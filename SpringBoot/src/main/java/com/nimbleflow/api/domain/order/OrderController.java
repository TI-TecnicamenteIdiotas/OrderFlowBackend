package com.nimbleflow.api.domain.order;

import com.nimbleflow.api.exception.response.example.ExceptionResponseExample;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@RestController
@RequiredArgsConstructor
@Tag(name = "Order Controller")
@SecurityRequirement(name = "Bearer Authorization")
@ApiResponse(
    responseCode = "401", 
    description = "Unauthorized", 
    content = @Content(schema = @Schema(implementation = ExceptionResponseExample.UnauthorizedException.class))
)
@RequestMapping(value = "api/v1/order", produces = MediaType.APPLICATION_JSON_VALUE)
public class OrderController {

    private final OrderService purchaseService;

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE) 
    @Operation(description = "Save order informations")
    @ApiResponses({
        @ApiResponse(responseCode = "201", description = "Created"),
        @ApiResponse(
            responseCode = "400", 
            description = "Bad Request (thrown when dto has invalid (null, empty) parameters)", 
            content = @Content(schema = @Schema(implementation = ExceptionResponseExample.BadRequestException.class))
        )
    })
    public ResponseEntity<OrderDTO> saveOrder(@RequestBody @Validated OrderDTO dto) {
        OrderDTO body = purchaseService.saveOrder(dto);
        return new ResponseEntity<>(body, HttpStatus.CREATED);
    }

    @GetMapping("{tableId}")
    @Operation(description = "Find orders by tableId")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<OrderDTO>> findOrdersByInterval(
        @PathVariable UUID tableId,
        @RequestParam(value = "inactive", required = false) boolean inactive
    ) {
        List<OrderDTO> body = purchaseService.findOrdersByTableId(tableId, inactive);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @DeleteMapping("{tableId}")
    @Operation(description = "Delete orders by tableId (logical exclusion)")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<OrderDTO>> deleteOrdersByTableId(@PathVariable UUID tableId) {
        List<OrderDTO> body = purchaseService.deleteOrderByTableId(tableId);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @GetMapping("/month-report")
    @Operation(description = "Get orders month report")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<OrderDTO>> findOrdersByInterval(
            @RequestParam(value = "getInactivePurchases", required = false) boolean getInactivePurchases
    ) {
        List<OrderDTO> body = purchaseService.getOrdersMonthReport(getInactivePurchases);
        HttpStatus httpStatus = body != null && !body.isEmpty() ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @GetMapping("/interval")
    @Operation(description = "Find orders by interval")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<OrderDTO>> findOrdersByInterval(
            @RequestParam(value = "getInactivePurchases", required = false) boolean getInactivePurchases,
            @RequestParam(value = "starDate") ZonedDateTime startDate,
            @RequestParam(value = "endDate") ZonedDateTime endDate
    ) {
        List<OrderDTO> body = purchaseService.findOrdersByInterval(startDate, endDate, getInactivePurchases);
        HttpStatus httpStatus = body != null && !body.isEmpty() ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

}

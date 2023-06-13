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
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@Slf4j
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

    private final OrderService orderService;

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE)
    @Operation(description = "Save order information")
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Created"),
            @ApiResponse(
                    responseCode = "400",
                    description = "Bad Request (thrown when dto has invalid (null, empty) parameters)",
                    content = @Content(schema = @Schema(implementation = ExceptionResponseExample.BadRequestException.class))
            )
    })
    public ResponseEntity<OrderDTO> saveOrder(@RequestBody @Validated OrderDTO dto) {
        log.info(String.format("Saving order: %s", dto));
        OrderDTO body = orderService.saveOrder(dto);
        return new ResponseEntity<>(body, HttpStatus.CREATED);
    }

    @PutMapping(consumes = MediaType.APPLICATION_JSON_VALUE)
    @Operation(description = "Update order information")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "OK"),
            @ApiResponse(
                    responseCode = "400",
                    description = "Bad Request (thrown when dto has invalid (null, empty) parameters)",
                    content = @Content(schema = @Schema(implementation = ExceptionResponseExample.BadRequestException.class))
            )
    })
    public ResponseEntity<OrderDTO> updateOrder(@RequestBody @Validated OrderDTO dto) {
        log.info(String.format("Updating order: %s", dto));
        OrderDTO body = orderService.updateOrderById(dto);
        return new ResponseEntity<>(body, HttpStatus.OK);
    }

    @GetMapping("{tableId}")
    @Operation(description = "Find orders by tableId")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content")
    })
    public ResponseEntity<List<OrderDTO>> findOrdersByTableId(
            @PathVariable UUID tableId,
            @RequestParam(value = "getDeletedOrders", required = false) boolean getDeletedOrders
    ) {
        log.info(String.format("Find orders by tableId: %s; getDeletedOrders: %s", tableId, getDeletedOrders));
        List<OrderDTO> body = orderService.findOrdersByTableId(tableId, getDeletedOrders);
        HttpStatus httpStatus = body.isEmpty() ? HttpStatus.NO_CONTENT : HttpStatus.OK;
        return new ResponseEntity<>(body, httpStatus);
    }

    @DeleteMapping("{tableId}")
    @Operation(description = "Delete orders by tableId (logical exclusion)")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content")
    })
    public ResponseEntity<List<OrderDTO>> deleteOrdersByTableId(@PathVariable UUID tableId) {
        log.info(String.format("Delete orders by tableId: %s", tableId));
        List<OrderDTO> body = orderService.deleteOrdersByTableId(tableId);
        HttpStatus httpStatus = body.isEmpty() ? HttpStatus.NO_CONTENT : HttpStatus.OK;
        return new ResponseEntity<>(body, httpStatus);
    }

    @DeleteMapping("{id}")
    @Operation(description = "Delete orders by tableId (logical exclusion)")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content")
    })
    public ResponseEntity<OrderDTO> deleteOrderById(@PathVariable UUID id) {
        log.info(String.format("Delete order by id: %s", id));
        OrderDTO body = orderService.deleteOrderById(id);
        HttpStatus httpStatus = body != null ? HttpStatus.NO_CONTENT : HttpStatus.OK;
        return new ResponseEntity<>(body, httpStatus);
    }

    @GetMapping("/interval")
    @Operation(description = "Find orders by interval")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content")
    })
    public ResponseEntity<List<OrderDTO>> findOrdersByInterval(
            @RequestParam(value = "getDeletedOrders", required = false) boolean getDeletedOrders,
            @RequestParam(value = "starDate") ZonedDateTime startDate,
            @RequestParam(value = "endDate") ZonedDateTime endDate
    ) {
        log.info(String.format("Find orders by interval: %s, %s; getDeletedOrders: %s", startDate, endDate, getDeletedOrders));
        List<OrderDTO> body = orderService.findOrdersByInterval(startDate, endDate, getDeletedOrders);
        HttpStatus httpStatus = body.isEmpty() ? HttpStatus.NO_CONTENT : HttpStatus.OK;
        return new ResponseEntity<>(body, httpStatus);
    }
}
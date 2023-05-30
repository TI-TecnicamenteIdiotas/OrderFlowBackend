package com.nimbleflow.api.domain.purchase;

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
@Tag(name = "Purchase Controller")
@SecurityRequirement(name = "Bearer Authorization")
@ApiResponse(
    responseCode = "401", 
    description = "Unauthorized", 
    content = @Content(schema = @Schema(implementation = ExceptionResponseExample.UnauthorizedException.class))
)
@RequestMapping(value = "api/v1/purchase", produces = MediaType.APPLICATION_JSON_VALUE)
public class PurchaseController {

    private final PurchaseService purchaseService;

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE) 
    @Operation(description = "Save purchase informations")
    @ApiResponses({
        @ApiResponse(responseCode = "201", description = "Created"),
        @ApiResponse(
            responseCode = "400", 
            description = "Bad Request (thrown when dto has invalid (null, empty) parameters)", 
            content = @Content(schema = @Schema(implementation = ExceptionResponseExample.BadRequestException.class))
        )
    })
    public ResponseEntity<PurchaseDTO> savePurchase(@RequestBody @Validated PurchaseDTO dto) {
        PurchaseDTO body = purchaseService.savePurchase(dto);
        return new ResponseEntity<>(body, HttpStatus.CREATED);
    }

    @GetMapping("{orderId}")
    @Operation(description = "Find purchase by it's orderId")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<PurchaseDTO>> getPurchaseByOrderId(
        @PathVariable UUID orderId,
        @RequestParam(value = "inactive", required = false) boolean inactive
    ) {
        List<PurchaseDTO> body = purchaseService.findPurchaseByOrderId(orderId, inactive);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @DeleteMapping("{orderId}")
    @Operation(description = "Delete purchase by it's orderId (logical exclusion)")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<PurchaseDTO>> deletePurchaseByOrderId(@PathVariable UUID orderId) {
        List<PurchaseDTO> body = purchaseService.deletePurchaseByOrderId(orderId);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @GetMapping("/month-report")
    @Operation(description = "Get purchases month report")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<PurchaseDTO>> getPurchaseByOrderId(
            @RequestParam(value = "getInactivePurchases", required = false) boolean getInactivePurchases
    ) {
        List<PurchaseDTO> body = purchaseService.getPurchaseMonthReport(getInactivePurchases);
        HttpStatus httpStatus = body != null && !body.isEmpty() ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    @GetMapping("/")
    @Operation(description = "Get purchases by interval")
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Ok"),
            @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<List<PurchaseDTO>> getPurchaseByOrderId(
            @RequestParam(value = "getInactivePurchases", required = false) boolean getInactivePurchases,
            @RequestParam(value = "starDate") ZonedDateTime startDate,
            @RequestParam(value = "endDate") ZonedDateTime endDate
    ) {
        List<PurchaseDTO> body = purchaseService.getPurchasesByInterval(startDate, endDate, getInactivePurchases);
        HttpStatus httpStatus = body != null && !body.isEmpty() ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(body, httpStatus);
    }

    // TODO: GetTopSoldProducts

}

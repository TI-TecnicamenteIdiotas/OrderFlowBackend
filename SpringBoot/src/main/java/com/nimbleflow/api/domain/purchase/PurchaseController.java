package com.nimbleflow.api.domain.purchase;

import java.util.List;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import com.nimbleflow.api.domain.shared.BaseResponse;
import com.nimbleflow.api.exception.response.example.ExceptionResponseExample;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;

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
    public ResponseEntity<BaseResponse<PurchaseDTO>> savePurchase(@RequestBody @Validated PurchaseDTO dto) {
        PurchaseDTO body = purchaseService.savePurchase(dto);
        return new ResponseEntity<>(new BaseResponse<>(body, HttpStatus.CREATED), HttpStatus.CREATED);
    }

    @GetMapping("{orderId}")
    @Operation(description = "Find purchase by it's orderId")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<BaseResponse<List<PurchaseDTO>>> getPurchaseByOrderId(
        @PathVariable Long orderId, 
        @RequestParam(value = "inactive", required = false) boolean inactive
    ) {
        List<PurchaseDTO> body = purchaseService.findPurchaseByOrderId(orderId, inactive);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(new BaseResponse<>(body, httpStatus), httpStatus);
    }

    @DeleteMapping("{orderId}")
    @Operation(description = "Delete purchase by it's orderId (logical exclusion)")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(responseCode = "204", description = "No Content", content = @Content)
    })
    public ResponseEntity<BaseResponse<List<PurchaseDTO>>> deletePurchaseByOrderId(@PathVariable Long orderId) {
        List<PurchaseDTO> body = purchaseService.deletePurchaseByOrderId(orderId);
        HttpStatus httpStatus = body != null ? HttpStatus.OK : HttpStatus.NO_CONTENT;
        return new ResponseEntity<>(new BaseResponse<>(body, httpStatus), httpStatus);
    }

    //TODO: GetMonthReport, GetReportByInterval, GetTopSoldProducts

}

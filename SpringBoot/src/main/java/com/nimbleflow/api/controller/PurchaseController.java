package com.nimbleflow.api.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import com.nimbleflow.api.controller.response.example.exception.ExceptionResponseExample;
import com.nimbleflow.api.dto.PurchaseDTO;
import com.nimbleflow.api.service.PurchaseService;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
@Tag(name = "Purchase Controller")
@RequestMapping(value = "api/purchase", produces = MediaType.APPLICATION_JSON_VALUE)
public class PurchaseController {

    private final PurchaseService purchaseService;

    @PostMapping
    @Operation(description = "Save purchase informations")
    @ApiResponses({
        @ApiResponse(responseCode = "201", description = "Created"),
        @ApiResponse(
            responseCode = "400", 
            description = "Bad Request (throw when dto has invalid (null, empty) parameters)", 
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
    public ResponseEntity<PurchaseDTO> getPurchaseByOrderId(@PathVariable Long orderId) {
        PurchaseDTO body = purchaseService.findPurchaseByOrderId(orderId);
        return new ResponseEntity<>(body, HttpStatus.OK);
    }

    @DeleteMapping("{orderId}")
    @Operation(description = "Delete purchase by it's orderId (logical exclusion)")
    @ApiResponses({
        @ApiResponse(responseCode = "200", description = "Ok"),
        @ApiResponse(
            responseCode = "400", 
            description = "Bad Request (throw when no data was found to be deleted)", 
            content = @Content(schema = @Schema(implementation = ExceptionResponseExample.BadRequestException.class))
        )
    })
    public ResponseEntity<PurchaseDTO> deletePurchaseByOrderId(@PathVariable Long orderId) {
        PurchaseDTO body = purchaseService.deletePurchaseByOrderId(orderId);
        return new ResponseEntity<>(body, HttpStatus.OK);
    }

    //TODO: GetMonthReport, GetReportByInterval, GetTopSoldProducts

}

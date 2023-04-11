package com.nimbleflow.api.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import com.nimbleflow.api.controller.response.BaseItemResponse;
import com.nimbleflow.api.dto.PurchaseDTO;

@RestController
@RequestMapping(name = "purchase", produces = MediaType.APPLICATION_JSON_VALUE, consumes = MediaType.APPLICATION_JSON_VALUE)
public class PurchaseController {

    //Post, Get e Delete
    //GetMonthReport
    //GetReportByInterval
    //GetTopSoldProducts
    @PostMapping
    public ResponseEntity<BaseItemResponse<PurchaseDTO>> savePurchase(@RequestBody PurchaseDTO dto) {
        BaseItemResponse<PurchaseDTO> body = new BaseItemResponse<PurchaseDTO>(dto);
        return new ResponseEntity<>(body, HttpStatus.CREATED);
    }

    @GetMapping("{orderId}")
    public ResponseEntity<BaseItemResponse<PurchaseDTO>> getPurchaseByOrderId(@RequestParam Long orderId) {
        BaseItemResponse<PurchaseDTO> body = new BaseItemResponse<PurchaseDTO>(new PurchaseDTO());
        return new ResponseEntity<>(body, HttpStatus.OK);
    }

}

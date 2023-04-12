package com.nimbleflow.api.service;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import com.nimbleflow.api.exception.BadRequestException;
import com.nimbleflow.api.dto.PurchaseDTO;
import com.nimbleflow.api.model.Purchase;
import com.nimbleflow.api.repository.PurchaseRepository;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class PurchaseService {

    private final PurchaseRepository purchaseRepository;
    private final ModelMapper modelMapper;

    public PurchaseDTO savePurchase(PurchaseDTO purchaseDTO) {
        Purchase purchase = modelMapper.map(purchaseDTO, Purchase.class);
        purchase = purchaseRepository.save(purchase);
        purchaseDTO = modelMapper.map(purchase, PurchaseDTO.class);
        return purchaseDTO;
    }

    public PurchaseDTO findPurchaseByOrderId(Long orderId) {
        Purchase purchase = purchaseRepository.findByOrderId(orderId).orElse(null);

        if (purchase == null) return null;

        return modelMapper.map(purchase, PurchaseDTO.class);
    }

    public PurchaseDTO deletePurchaseByOrderId(Long orderId) {
        Purchase purchase = purchaseRepository.findByOrderId(orderId)
            .orElseThrow(() -> new BadRequestException(String.format("There's no purchase with orderId %s to be deleted", orderId)));

        purchase.setActive(false);
        purchase = purchaseRepository.save(purchase);
        return modelMapper.map(purchase, PurchaseDTO.class);
    }

}

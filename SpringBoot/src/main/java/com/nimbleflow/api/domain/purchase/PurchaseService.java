package com.nimbleflow.api.domain.purchase;

import java.util.ArrayList;
import java.util.List;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import com.nimbleflow.api.exception.BadRequestException;

import lombok.RequiredArgsConstructor;

@Service
@RequiredArgsConstructor
public class PurchaseService {

    private final PurchaseRepository purchaseRepository;
    private final ModelMapper modelMapper;

    public PurchaseDTO savePurchase(PurchaseDTO purchaseDTO) {
        List<PurchaseDTO> purchaseExists = findPurchaseByOrderId(purchaseDTO.getOrderId());
        if (purchaseExists != null && !purchaseExists.isEmpty()) {
            throw new BadRequestException("Purchase already registered");
        }

        Purchase purchase = modelMapper.map(purchaseDTO, Purchase.class);
        purchase = purchaseRepository.save(purchase);
        purchaseDTO = modelMapper.map(purchase, PurchaseDTO.class);
        return purchaseDTO;
    }

    public List<PurchaseDTO> findPurchaseByOrderId(Long orderId) {
        return findPurchaseByOrderId(orderId, false);
    }

    public List<PurchaseDTO> findPurchaseByOrderId(Long orderId, boolean inactive) {
        List<Purchase> purchases = purchaseRepository.findByOrderIdAndActive(orderId, !inactive);
        List<PurchaseDTO> purchasesDTOs = new ArrayList<PurchaseDTO>();

        if (purchases.isEmpty()) return null;

        purchases.forEach(purchase -> {
            purchasesDTOs.add(modelMapper.map(purchase, PurchaseDTO.class));
        });

        return purchasesDTOs;
    }

    public List<PurchaseDTO> deletePurchaseByOrderId(Long orderId) {
        List<Purchase> purchases = purchaseRepository.findByOrderId(orderId);
        List<PurchaseDTO> purchasesDTOs = new ArrayList<PurchaseDTO>();
        
        if (purchases.isEmpty()) {
            return null;
        }

        purchases.forEach(purchase -> {
            purchase.setActive(false);
            purchase = purchaseRepository.save(purchase);
            purchasesDTOs.add(modelMapper.map(purchase, PurchaseDTO.class));
        });
        
        return purchasesDTOs;
    }

}

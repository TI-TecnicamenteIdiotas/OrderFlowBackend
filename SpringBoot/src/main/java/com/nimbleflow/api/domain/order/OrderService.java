package com.nimbleflow.api.domain.purchase;

import com.nimbleflow.api.exception.BadRequestException;
import lombok.RequiredArgsConstructor;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.time.ZonedDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

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

    public List<PurchaseDTO> findPurchaseByOrderId(UUID orderId) {
        return findPurchaseByOrderId(orderId, false);
    }

    public List<PurchaseDTO> findPurchaseByOrderId(UUID orderId, boolean inactive) {
        List<Purchase> purchases = purchaseRepository.findByOrderIdAndActive(orderId, !inactive);
        List<PurchaseDTO> purchasesDTOs = new ArrayList<PurchaseDTO>();

        if (purchases.isEmpty()) return null;

        purchases.forEach(purchase -> {
            purchasesDTOs.add(modelMapper.map(purchase, PurchaseDTO.class));
        });

        return purchasesDTOs;
    }

    public List<PurchaseDTO> deletePurchaseByOrderId(UUID orderId) {
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

    public List<PurchaseDTO> getPurchaseMonthReport(boolean getInactivePurchases) {
        int dayOfMonth = ZonedDateTime.now().getDayOfMonth();
        int daysToSubtract = (dayOfMonth + 1) - dayOfMonth;
        ZonedDateTime startDate = ZonedDateTime.now().minusDays(daysToSubtract);

        return getPurchasesByInterval(startDate, ZonedDateTime.now(), getInactivePurchases);
    }

    public List<PurchaseDTO> getPurchasesByInterval(ZonedDateTime startDate, ZonedDateTime endDate, boolean getInactivePurchases) {
        List<Purchase> purchases;

        if (getInactivePurchases) {
            purchases = purchaseRepository.findPurchasesByPurchaseDateBetween(startDate, endDate);
        } else {
            purchases = purchaseRepository.findPurchasesByPurchaseDateBetweenAndActiveTrue(startDate, endDate);
        }

        return purchases.stream()
                .map(this::mapPurchaseToPurchaseDTO)
                .toList();
    }

    private PurchaseDTO mapPurchaseToPurchaseDTO(Purchase purchase) {
        return modelMapper.map(purchase, PurchaseDTO.class);
    }

}

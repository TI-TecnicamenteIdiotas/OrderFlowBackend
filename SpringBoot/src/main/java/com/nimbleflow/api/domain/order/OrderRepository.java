package com.nimbleflow.api.domain.purchase;

import org.socialsignin.spring.data.dynamodb.repository.EnableScan;
import org.springframework.data.repository.CrudRepository;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@EnableScan
public interface PurchaseRepository extends CrudRepository<Purchase, String> {

    List<Purchase> findAll();
    List<Purchase> findByOrderId(UUID orderId);
    List<Purchase> findByOrderIdAndActive(UUID orderId, boolean active);

    List<Purchase> findPurchasesByPurchaseDateBetween(ZonedDateTime startDate, ZonedDateTime endDate);

    List<Purchase> findPurchasesByPurchaseDateBetweenAndActiveTrue(ZonedDateTime startDate, ZonedDateTime endDate);

}

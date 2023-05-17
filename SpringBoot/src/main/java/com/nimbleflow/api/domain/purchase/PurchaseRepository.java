package com.nimbleflow.api.domain.purchase;

import java.util.List;
import java.util.UUID;

import org.socialsignin.spring.data.dynamodb.repository.EnableScan;
import org.springframework.data.repository.CrudRepository;

@EnableScan
public interface PurchaseRepository extends CrudRepository<Purchase, String> {

    List<Purchase> findAll();
    List<Purchase> findByOrderId(UUID orderId);
    List<Purchase> findByOrderIdAndActive(UUID orderId, boolean active);

}

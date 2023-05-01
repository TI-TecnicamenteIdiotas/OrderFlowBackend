package com.nimbleflow.api.domain.purchase;

import java.util.List;

import org.socialsignin.spring.data.dynamodb.repository.EnableScan;
import org.springframework.data.repository.CrudRepository;

@EnableScan
public interface PurchaseRepository extends CrudRepository<Purchase, String> {

    List<Purchase> findAll();
    List<Purchase> findByOrderId(Long orderId);
    List<Purchase> findByOrderIdAndActive(Long orderId, boolean active);

}

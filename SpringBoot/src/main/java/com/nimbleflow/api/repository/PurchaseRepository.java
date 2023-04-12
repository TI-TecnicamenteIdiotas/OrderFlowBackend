package com.nimbleflow.api.repository;

import java.util.List;
import java.util.Optional;

import org.socialsignin.spring.data.dynamodb.repository.EnableScan;
import org.springframework.data.repository.CrudRepository;

import com.nimbleflow.api.model.Purchase;

@EnableScan
public interface PurchaseRepository extends CrudRepository<Purchase, String> {

    List<Purchase> findAll();
    Optional<Purchase> findByOrderId(Long orderId);

}

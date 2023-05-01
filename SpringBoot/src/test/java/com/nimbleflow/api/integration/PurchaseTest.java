package com.nimbleflow.api.integration;

import static org.junit.jupiter.api.Assertions.assertNotNull;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import com.nimbleflow.api.ApiApplication;
import com.nimbleflow.api.domain.purchase.Purchase;
import com.nimbleflow.api.domain.purchase.PurchaseRepository;

@ActiveProfiles("production")
@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = ApiApplication.class)
public class PurchaseTest {

    @Autowired
    private PurchaseRepository purchaseRepository;

    @Test
    void save() {
        Purchase purchase = Purchase.builder()
            .orderId(1L)
            .tableId(1L)
            .build();

        purchase = purchaseRepository.save(purchase);

        assertNotNull(purchase);
    }
    
}

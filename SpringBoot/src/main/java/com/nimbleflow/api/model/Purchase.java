package com.nimbleflow.api.model;

import com.nimbleflow.api.enums.PaymentMethod;
import lombok.Data;

@Data
public class Purchase {

    private Long orderId;
    private Long tableId;
    private PaymentMethod paymentMethod;

}
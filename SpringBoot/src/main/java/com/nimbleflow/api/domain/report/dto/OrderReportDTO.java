package com.nimbleflow.api.domain.report.dto;

import com.nimbleflow.api.domain.order.enums.PaymentMethod;
import lombok.*;

import java.time.ZonedDateTime;
import java.util.UUID;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class OrderReportDTO {

    private UUID id;
    private UUID tableId;
    private ZonedDateTime createdAt;
    private PaymentMethod paymentMethod;
    private String productsIdsAndAmount;
    private boolean active;

}

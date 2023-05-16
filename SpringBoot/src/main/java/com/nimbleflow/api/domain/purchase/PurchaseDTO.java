package com.nimbleflow.api.domain.purchase;

import java.time.ZonedDateTime;
import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.nimbleflow.api.domain.purchase.enums.PaymentMethod;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class PurchaseDTO {
    
    @JsonProperty(access = JsonProperty.Access.READ_ONLY)
    private UUID id;

    @NotNull
    private UUID orderId;

    @NotNull
    private UUID tableId;

    @NotNull
    private ZonedDateTime purchaseDate;

    @NotNull
    private PaymentMethod paymentMethod;

    private Boolean active;

}

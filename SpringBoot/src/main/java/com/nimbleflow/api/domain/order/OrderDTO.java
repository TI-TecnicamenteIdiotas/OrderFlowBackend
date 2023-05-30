package com.nimbleflow.api.domain.purchase;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.nimbleflow.api.domain.purchase.enums.PaymentMethod;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.time.ZonedDateTime;
import java.util.UUID;

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

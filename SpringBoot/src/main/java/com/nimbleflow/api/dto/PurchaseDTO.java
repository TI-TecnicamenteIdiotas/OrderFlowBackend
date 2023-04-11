package com.nimbleflow.api.dto;

import java.time.ZonedDateTime;

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
    
    private String id;

    @NotNull
    private Long orderId;

    @NotNull
    private Long tableId;

    @NotNull
    private ZonedDateTime purchaseDate;

    private Boolean active;

}

package com.nimbleflow.api.domain.order;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.nimbleflow.api.domain.order.enums.PaymentMethod;
import com.nimbleflow.api.domain.product.ProductDTO;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class OrderDTO {

    private UUID id;

    @NotNull
    private UUID tableId;

    @NotNull
    private ZonedDateTime orderDate;

    @NotNull
    private PaymentMethod paymentMethod;

    @NotNull
    private ProductDTO productDTO;

    @NotNull
    private List<ProductDTO> products;

    private Boolean active;

}

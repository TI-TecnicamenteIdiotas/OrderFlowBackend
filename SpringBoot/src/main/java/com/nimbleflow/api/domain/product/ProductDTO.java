package com.nimbleflow.api.domain.product;

import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.util.UUID;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class ProductDTO {
    @NotNull
    private UUID id;

    @NotNull
    private Integer amount;
}
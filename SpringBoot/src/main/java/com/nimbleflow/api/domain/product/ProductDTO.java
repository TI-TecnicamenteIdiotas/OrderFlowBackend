package com.nimbleflow.api.domain.product;

import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBAttribute;
import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBTypeConverted;
import com.nimbleflow.api.utils.dynamodb.converters.UUIDConverter;
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
    @DynamoDBAttribute
    @DynamoDBTypeConverted(converter = UUIDConverter.class)
    private UUID id;

    @NotNull
    @DynamoDBAttribute
    private Integer amount;

}
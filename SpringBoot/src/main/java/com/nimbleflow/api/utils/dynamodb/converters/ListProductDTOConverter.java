package com.nimbleflow.api.utils.dynamodb.converters;

import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBTypeConverter;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.nimbleflow.api.domain.product.ProductDTO;

import java.io.IOException;
import java.util.List;

public class ListProductDTOConverter implements DynamoDBTypeConverter<String, List<ProductDTO>> {

    private final ObjectMapper objectMapper = new ObjectMapper();

    @Override
    public String convert(List<ProductDTO> productDTOs) {
        try {
            return objectMapper.writeValueAsString(productDTOs);
        } catch (JsonProcessingException e) {
            throw new IllegalArgumentException("Failed to convert MyObject to JSON string", e);
        }
    }

    @Override
    public List<ProductDTO> unconvert(String json) {
        try {
            return objectMapper.readValue(json, new TypeReference<List<ProductDTO>>() {});
        } catch (IOException e) {
            throw new IllegalArgumentException("Failed to convert JSON string to MyObject", e);
        }
    }
}

package com.nimbleflow.api.utils.dynamodb.converters;

import com.amazonaws.services.dynamodbv2.datamodeling.DynamoDBTypeConverter;

import java.util.UUID;

public class UUIDConverter implements DynamoDBTypeConverter<String, UUID> {

    @Override
    public String convert(UUID uuid) {
        return uuid != null ? uuid.toString() : null;
    }

    @Override
    public UUID unconvert(String s) {
        return s != null ? UUID.fromString(s) : null;
    }
}

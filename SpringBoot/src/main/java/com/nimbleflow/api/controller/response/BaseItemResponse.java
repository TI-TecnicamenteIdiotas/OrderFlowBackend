package com.nimbleflow.api.controller.response;

import java.time.ZonedDateTime;

import lombok.Data;

@Data
public class BaseItemResponse<T> {
    
    private boolean success;
    private ZonedDateTime time;
    private T item;

    public BaseItemResponse(T item) {
        this.success = true;
        this.time = ZonedDateTime.now();
        this.item = item;
    }

}

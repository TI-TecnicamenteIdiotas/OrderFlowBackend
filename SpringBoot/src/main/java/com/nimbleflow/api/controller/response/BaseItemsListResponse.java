package com.nimbleflow.api.controller.response;

import java.time.ZonedDateTime;

import lombok.Data;

@Data
public class BaseItemsListResponse<T> {
    
    private boolean success;
    private ZonedDateTime time;
    private T listOfItems;

}

package com.nimbleflow.api.exception.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.ZonedDateTime;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class BaseExceptionResponse {

    private ZonedDateTime timestamp;
    private int status;
    private String error;
    private String message;
    private String path;
    
}

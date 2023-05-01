package com.nimbleflow.api.domain.shared;

import java.time.ZonedDateTime;

import org.springframework.http.HttpStatusCode;
import org.springframework.stereotype.Component;

import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Component
@NoArgsConstructor
public class BaseResponse<T> {

    private ZonedDateTime timestamp;
    private int status;
    private String path;
    private T content;

    public BaseResponse(T content, HttpStatusCode status) {
        this.timestamp = ZonedDateTime.now();
        this.status = status.value();
        this.content = content;
    }
    
}

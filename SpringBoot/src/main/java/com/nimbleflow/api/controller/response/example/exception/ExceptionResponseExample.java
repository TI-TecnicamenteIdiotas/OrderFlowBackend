package com.nimbleflow.api.controller.response.example.exception;

import java.time.ZonedDateTime;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Getter;

public interface ExceptionResponseExample {

    @Getter
    public static final class BadRequestException extends BaseExceptionResponseExample {

        @Schema(example = "400")
        private int status;

        @Schema(example = "Bad Request")
        private String error;

    }

    @Getter
    public static abstract class BaseExceptionResponseExample {

        private ZonedDateTime timestamp;

        @Schema(example = "Exception message")
        private String message;

        @Schema(example = "request/path")
        private String path;

    }
    
}

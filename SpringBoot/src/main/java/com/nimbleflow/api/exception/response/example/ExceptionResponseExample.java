package com.nimbleflow.api.exception.response.example;

import java.time.ZonedDateTime;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Getter;

public interface ExceptionResponseExample {

    @Getter
    public static final class BadRequestException extends BaseExceptionResponseExample {

        @Schema(example = "2023-04-30T15:18:24.883+00:00")
        private ZonedDateTime timestamp;

        @Schema(example = "400")
        private int status;

        @Schema(example = "Bad Request")
        private String error;

    }

    @Getter
    public static final class UnauthorizedException extends BaseExceptionResponseExample {

        @Schema(example = "2023-04-30T15:18:24.883+00:00")
        private ZonedDateTime timestamp;

        @Schema(example = "401")
        private int status;

        @Schema(example = "Unauthorized")
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

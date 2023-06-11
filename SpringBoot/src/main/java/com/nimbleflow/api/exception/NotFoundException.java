package com.nimbleflow.api.exception;

import jakarta.servlet.ServletException;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.UNAUTHORIZED)
public class NotFoundException extends ServletException {
    public NotFoundException(String message) {
        super(message);
    }
}
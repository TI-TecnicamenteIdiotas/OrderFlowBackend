package com.nimbleflow.api.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

import jakarta.servlet.ServletException;

@ResponseStatus(value = HttpStatus.UNAUTHORIZED)
public class UnauthorizedException extends ServletException {

    public static final String MESSAGE = "Empty or invalid Authorization header";

    public UnauthorizedException() {
        super(MESSAGE);
    }
    
}

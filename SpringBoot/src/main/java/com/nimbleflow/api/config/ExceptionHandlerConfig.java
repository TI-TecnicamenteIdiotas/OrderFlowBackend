package com.nimbleflow.api.config;

import java.time.ZonedDateTime;
import java.util.ArrayList;
import java.util.List;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.InsufficientAuthenticationException;
import org.springframework.validation.FieldError;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

import com.nimbleflow.api.exception.BadRequestException;
import com.nimbleflow.api.exception.UnauthorizedException;
import com.nimbleflow.api.exception.response.BaseExceptionResponse;

import io.jsonwebtoken.security.SignatureException;
import jakarta.servlet.http.HttpServletRequest;

@ControllerAdvice
public class ExceptionHandlerConfig {

    @ExceptionHandler({BadRequestException.class, BadCredentialsException.class})
    @ResponseBody
    public ResponseEntity<BaseExceptionResponse> badRequestHandler(HttpServletRequest request, Exception exception) {
        BaseExceptionResponse responseBody = BaseExceptionResponse.builder()
            .timestamp(ZonedDateTime.now())
            .status(400)
            .error("Bad Request")
            .message(exception.getMessage())
            .path(request.getServletPath())
            .build();

        ResponseEntity<BaseExceptionResponse> response = new ResponseEntity<>(responseBody, HttpStatus.BAD_REQUEST);
        return response;
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    @ResponseBody
    public ResponseEntity<BaseExceptionResponse> methodArgumentNotValidHandler(HttpServletRequest request, MethodArgumentNotValidException exception) {
        List<FieldError> errors = exception.getBindingResult().getFieldErrors();

        List<String> errorList = new ArrayList<String>();

        for (FieldError error : errors) {
            errorList.add(String.format("%s %s", error.getField(), error.getDefaultMessage()));
        };

        BaseExceptionResponse responseBody = BaseExceptionResponse.builder()
            .timestamp(ZonedDateTime.now())
            .status(400)
            .error("Bad Request")
            .message(errorList.toString())
            .path(request.getServletPath())
            .build();

        ResponseEntity<BaseExceptionResponse> response = new ResponseEntity<>(responseBody, HttpStatus.BAD_REQUEST);
        return response;
    }

    @ExceptionHandler({UnauthorizedException.class, InsufficientAuthenticationException.class, SignatureException.class})
    @ResponseBody
    public ResponseEntity<BaseExceptionResponse> unauthorizedHandler(HttpServletRequest request, Exception exception) {
        BaseExceptionResponse responseBody = BaseExceptionResponse.builder()
            .timestamp(ZonedDateTime.now())
            .status(401)
            .error("Unauthorized")
            .message(UnauthorizedException.MESSAGE)
            .path(request.getServletPath())
            .build();

        ResponseEntity<BaseExceptionResponse> response = new ResponseEntity<>(responseBody, HttpStatus.UNAUTHORIZED);
        return response;
    }

    @ExceptionHandler(Exception.class)
    @ResponseBody
    public ResponseEntity<BaseExceptionResponse> defaultHandler(HttpServletRequest request, Exception exception) {
        BaseExceptionResponse responseBody = BaseExceptionResponse.builder()
            .timestamp(ZonedDateTime.now())
            .status(500)
            .error("Internal Server Error")
            .message(exception.getMessage())
            .path(request.getServletPath())
            .build();

        ResponseEntity<BaseExceptionResponse> response = new ResponseEntity<>(responseBody, HttpStatus.INTERNAL_SERVER_ERROR);
        return response;
    }

}

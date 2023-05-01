package com.nimbleflow.api.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.MethodParameter;
import org.springframework.http.MediaType;
import org.springframework.http.converter.HttpMessageConverter;
import org.springframework.http.server.ServerHttpRequest;
import org.springframework.http.server.ServerHttpResponse;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.servlet.mvc.method.annotation.ResponseBodyAdvice;

import com.nimbleflow.api.domain.shared.BaseResponse;

@ControllerAdvice
public class ResponseFilterConfig<T> implements ResponseBodyAdvice<T> {

    @Value("${server.servlet.context-path}")
    private String contextPath;

    @Override
    public boolean supports(MethodParameter returnType, Class<? extends HttpMessageConverter<?>> converterType) {
        return true;
    }

    @Override
    public T beforeBodyWrite(T body, MethodParameter returnType, MediaType selectedContentType,
            Class<? extends HttpMessageConverter<?>> selectedConverterType, ServerHttpRequest request, ServerHttpResponse response) {
        if (body.getClass() == BaseResponse.class) {
            BaseResponse<?> finalResponse = (BaseResponse<?>) body;
            finalResponse.setPath(request.getURI().getPath().replace(contextPath, ""));
            return (T) finalResponse;
        }

        return body;
    }
    
}

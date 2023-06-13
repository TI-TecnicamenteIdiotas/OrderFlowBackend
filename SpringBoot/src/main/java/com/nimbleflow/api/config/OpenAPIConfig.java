package com.nimbleflow.api.config;

import io.swagger.v3.oas.models.ExternalDocumentation;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenAPIConfig {
    @Bean
    public OpenAPI openApi() {
        return new OpenAPI()
                .info(new Info()
                        .title("NimbleFlow - Products Reports API")
                        .description("NimbleFlow Spring-Boot API to generate products reports")
                        .version("v1.0.0")
                        .license(new License().name("MIT")))
                .externalDocs(new ExternalDocumentation()
                        .description("NimbleFlow Documentation")
                        .url("https://github.com/TI-TecnicamenteIdiotas/order-flow-backend"));
    }
}
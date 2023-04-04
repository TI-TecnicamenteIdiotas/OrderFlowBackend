package com.nimbleflow.api.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import io.swagger.v3.oas.models.ExternalDocumentation;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;

@Configuration
public class OpenAPIConfig {
    
    @Bean
    public OpenAPI openApi() {
        return new OpenAPI()
                .info(new Info().title("NimbleFlow - Spring Boot API")
                .description("NimbleFlow BackEnd Application")
                .version("v0.0.1")
                .license(new License().name("MIT")))
                .externalDocs(new ExternalDocumentation()
                    .description("NimbleFlow Documentation")
                    .url("https://github.com/TI-TecnicamenteIdiotas/order-flow-backend"));
    }
  

}

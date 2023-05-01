package com.nimbleflow.api.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import io.swagger.v3.oas.annotations.enums.SecuritySchemeType;
import io.swagger.v3.oas.annotations.security.SecurityScheme;
import io.swagger.v3.oas.models.ExternalDocumentation;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;

@Configuration
@SecurityScheme(
    name = "Bearer Authorization",
    type = SecuritySchemeType.HTTP,
    bearerFormat = "JWT",
    scheme = "bearer"
)
public class OpenAPIConfig {
    
    @Bean
    public OpenAPI openApi() {
        return new OpenAPI()
                .info(new Info().title("NimbleFlow - Prodcuts Reports API")
                .description("NimbleFlow Spring-Boot API to generate products reports")
                .version("v1.0.0")
                .license(new License().name("MIT")))
                .externalDocs(new ExternalDocumentation()
                    .description("NimbleFlow Documentation")
                    .url("https://github.com/TI-TecnicamenteIdiotas/order-flow-backend"));
    }
  

}

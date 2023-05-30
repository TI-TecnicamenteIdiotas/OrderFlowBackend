package com.nimbleflow.api.domain.product;

import com.nimbleflow.api.exception.response.example.ExceptionResponseExample;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
@Tag(name = "Purchase Controller")
@SecurityRequirement(name = "Bearer Authorization")
@ApiResponse(
        responseCode = "401",
        description = "Unauthorized",
        content = @Content(schema = @Schema(implementation = ExceptionResponseExample.UnauthorizedException.class))
)
@RequestMapping(value = "api/v1/product", produces = MediaType.APPLICATION_JSON_VALUE)
public class ProductController {

    // TODO: GetTopSoldProducts

}

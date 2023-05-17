package com.nimbleflow.api.domain.auth;

import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.v3.oas.annotations.Hidden;
import lombok.RequiredArgsConstructor;

@Hidden
@RestController
@RequiredArgsConstructor
@RequestMapping(value = "api/v1/auth", produces = MediaType.APPLICATION_JSON_VALUE)
public class AuthController {

    private final AuthService authService;

    @PostMapping(value = "web", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<AuthDTO> webRegister(@RequestBody @Validated AuthDTO dto) {
        return new ResponseEntity<>(authService.webAuthenticate(dto), HttpStatus.OK);
    }

    @GetMapping(value = "mobile")
    public ResponseEntity<AuthDTO> mobileRegister(@RequestParam("integrationToken") String integrationToken) {
        return new ResponseEntity<>(authService.mobileAuthenticate(integrationToken), HttpStatus.OK);
    }
}

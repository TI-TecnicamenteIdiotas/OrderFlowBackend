package com.nimbleflow.api.domain.auth;

import io.swagger.v3.oas.annotations.Hidden;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

@Slf4j
@Hidden
@RestController
@RequiredArgsConstructor
@RequestMapping(value = "api/v1/auth", produces = MediaType.APPLICATION_JSON_VALUE)
public class AuthController {

    private final AuthService authService;

    @PostMapping(value = "web", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<AuthDTO> webRegister(@RequestBody @Validated AuthDTO dto) {
        log.warn(String.format("Web authentication: %s", dto));
        return new ResponseEntity<>(authService.webAuthenticate(dto), HttpStatus.OK);
    }

    @GetMapping(value = "mobile")
    public ResponseEntity<AuthDTO> mobileRegister(@RequestParam("integrationToken") String integrationToken) {
        log.warn(String.format("Mobile authentication: %s", integrationToken));
        return new ResponseEntity<>(authService.mobileAuthenticate(integrationToken), HttpStatus.OK);
    }
}

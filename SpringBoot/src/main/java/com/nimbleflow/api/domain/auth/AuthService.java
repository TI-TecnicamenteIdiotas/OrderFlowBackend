package com.nimbleflow.api.domain.auth;

import com.nimbleflow.api.config.security.JwtService;
import com.nimbleflow.api.exception.UnauthorizedException;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.stereotype.Service;

import java.util.HashMap;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final AuthenticationManager authenticationManager;
    private final UserDetailsService userDetailsService;
    private final JwtService jwtService;

    @Value("${nimbleflow.username}")
    private String API_INTEGRATION_TOKEN;

    @Value("${nimbleflow.username}")
    private String API_USERNAME;

    public AuthDTO webAuthenticate(AuthDTO dto) {
        authenticationManager.authenticate(
            new UsernamePasswordAuthenticationToken(dto.getUsername(), dto.getPassword())
        );

        String jwtToken = jwtService.generateToken(
            new HashMap<>(), 
            userDetailsService.loadUserByUsername(dto.getUsername())
        );

        dto.setToken(jwtToken);
        return dto;
    }

    @SneakyThrows
    public AuthDTO mobileAuthenticate(String integrationToken) {
        if (!API_INTEGRATION_TOKEN.equals(integrationToken)) {
            throw new UnauthorizedException("Invalid integrationToken");
        }

        String jwtToken = jwtService.generateToken(
            new HashMap<>(), 
            userDetailsService.loadUserByUsername(API_USERNAME)
        );

        return AuthDTO.builder().token(jwtToken).build();
    }
    
}

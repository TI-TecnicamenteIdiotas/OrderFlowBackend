package com.nimbleflow.api.config;

import java.util.Collections;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import com.nimbleflow.api.config.security.UserDetailsImpl;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;

@Service
@RequiredArgsConstructor
public class ApplicationConfig {

    @Value("${nimbleflow.username}")
    private String API_USERNAME;

    @Value("${nimbleflow.password}")
    private String API_PASSWORD;
    
    @Bean
    public UserDetailsService userDetailsService() {
        return username -> findApiUser(username)
            .orElseThrow(() -> new UsernameNotFoundException("User not found."));
    }

    @Bean 
    public AuthenticationProvider authenticationProvider() {
        DaoAuthenticationProvider daoAuthenticationProvider = new DaoAuthenticationProvider();
        daoAuthenticationProvider.setUserDetailsService(userDetailsService());
        daoAuthenticationProvider.setPasswordEncoder(passwordEncoder());
        return daoAuthenticationProvider;
    }

    @Bean
    @SneakyThrows
    public AuthenticationManager authenticationManager(AuthenticationConfiguration configuration) {
        return configuration.getAuthenticationManager();
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder(10);
    }

    private Optional<UserDetails> findApiUser(String username) {
        if (API_USERNAME.equals(username)) {
            return Optional.of(
                UserDetailsImpl.builder()
                    .authorities(Collections.emptyList())
                    .username(API_USERNAME)
                    .password(passwordEncoder().encode(API_PASSWORD))
                    .isAccountNonExpired(true)
                    .isAccountNonLocked(true)
                    .isCredentialsNonExpired(true)
                    .isEnabled(true)
                    .build()
            );
        }

        return Optional.empty();
    }

}

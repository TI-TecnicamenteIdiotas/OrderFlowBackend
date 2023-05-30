package com.nimbleflow.api.config.security;

import com.nimbleflow.api.config.CustomAuthenticationEntryPointConfig;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.web.AuthenticationEntryPoint;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class SecurityConfig {
    
    private final JwtAuthenticationFilter jwtAuthFilter;
    private final AuthenticationProvider authenticationProvider;

    @Bean
    @SneakyThrows
    public SecurityFilterChain filterChain(HttpSecurity http) {
        http.csrf().disable()
            .authorizeHttpRequests((authorization) -> authorization
                .requestMatchers(
                    "/api/v1/auth/**",
                    "/v3/api-docs/**",
                    "/swagger-ui/**",
                    "/v2/api-docs/**",
                    "/swagger-resources/**"
                ).permitAll()
                .anyRequest()
                .authenticated()
            )
            .sessionManagement()
            .sessionCreationPolicy(SessionCreationPolicy.STATELESS).and()
            .authenticationProvider(authenticationProvider)
            .exceptionHandling().authenticationEntryPoint(authenticationEntryPoint()).and()
            .addFilterBefore(jwtAuthFilter, UsernamePasswordAuthenticationFilter.class);

        return http.build();
    }

    @Bean
    public AuthenticationEntryPoint authenticationEntryPoint(){
        return new CustomAuthenticationEntryPointConfig();
    }

}

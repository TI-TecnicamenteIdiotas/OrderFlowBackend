package com.nimbleflow.api.config.security;

import java.security.Key;
import java.time.ZonedDateTime;
import java.util.Date;
import java.util.Map;
import java.util.function.Function;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;

@Service
public class JwtService {

    @Value("${nimbleflow.jwt.signing-key}")
    private String SIGNING_KEY;
    
    public String getUsername(String token) {
        return extractJwtClaim(token, Claims::getSubject);
    }

    public String generateToken(Map<String, Object> extractJwtClaims, UserDetails userDetails) {
        return Jwts
            .builder()
            .setClaims(extractJwtClaims)
            .setSubject(userDetails.getUsername())
            .setIssuedAt(Date.from(ZonedDateTime.now().toInstant()))
            .setExpiration(Date.from(ZonedDateTime.now().plusDays(1).toInstant()))
            .signWith(getSigningKey(), SignatureAlgorithm.HS256)
            .compact();
    }

    public boolean isJwtTokenValid(String token, UserDetails userDetails) {
        final String username = getUsername(token);
        return (username.equals(userDetails.getUsername())) && !isTokenExpired(token);
    }

    private boolean isTokenExpired(String token) {
        Date tokenExpirationDate = extractJwtClaim(token, Claims::getExpiration);
        return tokenExpirationDate.before(new Date());
    }

    public <T> T extractJwtClaim(String token, Function<Claims, T> claimsResolver) {
        final Claims claims = extractJwtClaims(token);
        return claimsResolver.apply(claims);
    }

    private Claims extractJwtClaims(String token) {
        return Jwts
            .parserBuilder()
            .setSigningKey(getSigningKey())
            .build()
            .parseClaimsJws(token)
            .getBody();
    }

    private Key getSigningKey() {
        byte[] signingKeyBytes = Decoders.BASE64.decode(SIGNING_KEY);
        return Keys.hmacShaKeyFor(signingKeyBytes);
    }

}

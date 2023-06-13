package com.nimbleflow.api.domain.order;

import jakarta.validation.constraints.NotNull;
import org.socialsignin.spring.data.dynamodb.repository.EnableScan;
import org.springframework.data.repository.CrudRepository;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@EnableScan
public interface OrderRepository extends CrudRepository<Order, String> {
    @NotNull
    List<Order> findAll();

    Optional<Order> findById(UUID id);

    List<Order> findByDeletedAtIsNullOrDeletedAtIsEmpty();

    List<Order> findByTableId(UUID tableId);

    List<Order> findByTableIdAndDeletedAtIsNullOrDeletedAtIsEmpty(UUID orderId);

    List<Order> findByCreatedAtBetween(ZonedDateTime startDate, ZonedDateTime endDate);

    List<Order> findByCreatedAtBetweenAndDeletedAtIsNullOrDeletedAtIsEmpty(ZonedDateTime startDate, ZonedDateTime endDate);
}
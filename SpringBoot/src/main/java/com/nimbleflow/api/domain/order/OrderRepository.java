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
    List<Order> findByActiveTrue();
    List<Order> findByTableId(UUID tableId);
    List<Order> findByTableIdAndActive(UUID orderId, boolean active);
    List<Order> findByOrderDateBetween(ZonedDateTime startDate, ZonedDateTime endDate);
    List<Order> findByOrderDateBetweenAndActiveTrue(ZonedDateTime startDate, ZonedDateTime endDate);

}

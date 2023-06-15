package com.nimbleflow.api.domain.order;

import com.nimbleflow.api.exception.BadRequestException;
import com.nimbleflow.api.exception.NotFoundException;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Lazy;
import org.springframework.stereotype.Service;

import java.time.ZonedDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Slf4j
@Service
@RequiredArgsConstructor
public class OrderService {
    private final OrderRepository orderRepository;
    private ModelMapper modelMapper;

    @Autowired
    public void setModelMapper(@Lazy ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

    public OrderDTO saveOrder(OrderDTO orderDTO) {
        if (orderDTO.getId() != null) {
            orderDTO.setId(null);
        }

        if (orderDTO.getActive() == null) {
            orderDTO.setActive(true);
        }

        Order order = modelMapper.map(orderDTO, Order.class);
        order = orderRepository.save(order);
        log.info(String.format("Order saved successfully: %s", order));

        orderDTO = modelMapper.map(order, OrderDTO.class);
        return orderDTO;
    }

    @SneakyThrows
    public OrderDTO updateOrderById(OrderDTO orderDTO) {
        if (orderDTO.getId() == null)
            throw new BadRequestException("Please, inform the id of the order you want to update");

        Order order = findOrderById(orderDTO.getId())
                .orElseThrow(() -> new NotFoundException(String.format("The order with id %s was not found", orderDTO.getId())));

        modelMapper.map(orderDTO, order);
        order = orderRepository.save(order);
        log.info(String.format("Order updated successfully: %s", order));

        return modelMapper.map(order, OrderDTO.class);
    }

    public List<OrderDTO> findOrdersByTableId(UUID tableId, boolean getDeletedOrders) {
        List<Order> orders;

        if (getDeletedOrders) {
            orders = orderRepository.findByTableId(tableId);
        } else {
            orders = orderRepository.findByTableIdAndActiveIsTrue(tableId);
        }

        if (orders.isEmpty()) return new ArrayList<>();

        List<OrderDTO> orderDTOS = new ArrayList<>();

        orders.forEach(order -> {
            orderDTOS.add(modelMapper.map(order, OrderDTO.class));
        });

        return orderDTOS;
    }

    public List<OrderDTO> deleteOrdersByTableId(UUID orderId) {
        List<Order> orders = orderRepository.findByTableId(orderId);
        List<OrderDTO> orderDTOS = new ArrayList<>();

        if (orders.isEmpty()) {
            return new ArrayList<>();
        }

        orders.forEach(order -> {
            order.setActive(false);
            order = orderRepository.save(order);
            log.info(String.format("Order deleted successfully: %s", order));
            orderDTOS.add(modelMapper.map(order, OrderDTO.class));
        });

        return orderDTOS;
    }

    public OrderDTO deleteOrderById(UUID orderId) {
        Order order = orderRepository.findById(orderId).orElse(null);

        if (order == null) return null;

        order.setActive(false);
        order = orderRepository.save(order);
        log.info(String.format("Order deleted successfully: %s", order));

        return modelMapper.map(order, OrderDTO.class);
    }

    public List<OrderDTO> getAllMothOrders(boolean getDeletedOrders) {
        int dayOfMonth = ZonedDateTime.now().getDayOfMonth();
        int daysToSubtract = (dayOfMonth + 1) - dayOfMonth;
        ZonedDateTime startDate = ZonedDateTime.now().minusDays(daysToSubtract);

        return findOrdersByInterval(startDate, ZonedDateTime.now(), getDeletedOrders);
    }

    public List<OrderDTO> findOrdersByInterval(ZonedDateTime startDate, ZonedDateTime endDate, boolean getDeletedOrders) {
        List<Order> orders;

        if (getDeletedOrders) {
            orders = orderRepository.findByCreatedAtBetween(startDate, endDate);
        } else {
            orders = orderRepository.findByCreatedAtBetweenAndActiveIsTrue(startDate, endDate);
        }

        return orders.stream()
                .map(order -> modelMapper.map(order, OrderDTO.class))
                .toList();
    }

    public List<OrderDTO> findAllOrders(boolean getDeletedOrders) {
        List<Order> orders;

        if (getDeletedOrders) {
            orders = orderRepository.findAll();
        } else {
            orders = orderRepository.findByActiveIsTrue();
        }

        return orders.stream()
                .map(order -> modelMapper.map(order, OrderDTO.class))
                .toList();
    }

    public Optional<Order> findOrderById(UUID id) {
        return orderRepository.findById(id);
    }

}
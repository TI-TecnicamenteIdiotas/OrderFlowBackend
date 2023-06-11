package com.nimbleflow.api.domain.order;

import com.nimbleflow.api.exception.BadRequestException;
import com.nimbleflow.api.exception.NotFoundException;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.modelmapper.ModelMapper;
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
    private final ModelMapper modelMapper = new ModelMapper();

    public OrderDTO saveOrder(OrderDTO orderDTO) {
        if (orderDTO.getId() != null) {
            orderDTO.setId(null);
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

        order = orderRepository.save(order);
        log.info(String.format("Order updated successfully: %s", order));

        return modelMapper.map(order, OrderDTO.class);
    }

    public List<OrderDTO> findOrdersByTableId(UUID orderId, boolean getInactivePurchases) {
        List<Order> orders = orderRepository.findByTableIdAndActive(orderId, !getInactivePurchases);
        List<OrderDTO> purchasesDTOs = new ArrayList<>();

        if (orders.isEmpty()) return new ArrayList<>();

        orders.forEach(order -> {
            purchasesDTOs.add(modelMapper.map(order, OrderDTO.class));
        });

        return purchasesDTOs;
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

    public List<OrderDTO> getAllMothOrders(boolean getInactiveOrders) {
        int dayOfMonth = ZonedDateTime.now().getDayOfMonth();
        int daysToSubtract = (dayOfMonth + 1) - dayOfMonth;
        ZonedDateTime startDate = ZonedDateTime.now().minusDays(daysToSubtract);

        return findOrdersByInterval(startDate, ZonedDateTime.now(), getInactiveOrders);
    }

    public List<OrderDTO> findOrdersByInterval(ZonedDateTime startDate, ZonedDateTime endDate, boolean getInactiveOrders) {
        List<Order> orders;

        if (getInactiveOrders) {
            orders = orderRepository.findByOrderDateBetween(startDate, endDate);
        } else {
            orders = orderRepository.findByOrderDateBetweenAndActiveTrue(startDate, endDate);
        }

        return orders.stream()
                .map(this::mapOrderToOrderDTO)
                .toList();
    }

    public List<OrderDTO> findAllOrders(boolean getInactiveOrders) {
        List<Order> orders;

        if (getInactiveOrders) {
            orders = orderRepository.findAll();
        } else {
            orders = orderRepository.findByActiveTrue();
        }

        return orders.stream()
                .map(this::mapOrderToOrderDTO)
                .toList();
    }

    public Optional<Order> findOrderById(UUID id) {
        return orderRepository.findById(id);
    }

    private OrderDTO mapOrderToOrderDTO(Order order) {
        return modelMapper.map(order, OrderDTO.class);
    }

}

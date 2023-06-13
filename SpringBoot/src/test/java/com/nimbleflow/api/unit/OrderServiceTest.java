package com.nimbleflow.api.unit;

import com.nimbleflow.api.domain.order.Order;
import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.OrderRepository;
import com.nimbleflow.api.domain.order.OrderService;
import com.nimbleflow.api.exception.BadRequestException;
import com.nimbleflow.api.exception.NotFoundException;
import com.nimbleflow.api.utils.ObjectBuilder;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@SpringBootTest
@DisplayName("OrderService")
public class OrderServiceTest {
    @InjectMocks
    private OrderService underTest;

    @Mock
    private OrderRepository orderRepository;

    @Test
    @DisplayName("Validate if saveOrder() saves the order correctly and returns the OrderDTO correctly")
    void saveOrderSuccess() {
        Order order = ObjectBuilder.buildOrder();

        Mockito.when(orderRepository.save(Mockito.any(Order.class)))
                .thenReturn(order);

        OrderDTO result = underTest.saveOrder(ObjectBuilder.buildOrderDTO());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(order.getId(), result.getId());
        Assertions.assertEquals(order.getTableId(), result.getTableId());
        Assertions.assertEquals(order.getCreatedAt(), result.getCreatedAt());
        Assertions.assertEquals(order.getProducts(), result.getProducts());
        Assertions.assertEquals(order.getPaymentMethod(), result.getPaymentMethod());
        Assertions.assertEquals(order.getDeletedAt(), result.getDeletedAt());
    }

    @Test
    @DisplayName("Validate if updateOrderById() updates the order correctly and returns the OrderDTO correctly")
    void updateOrderByIdSuccess() {
        Order order = ObjectBuilder.buildOrder();

        Mockito.when(orderRepository.save(Mockito.any(Order.class)))
                .thenReturn(order);

        Mockito.when(orderRepository.findById(Mockito.any(UUID.class)))
                .thenReturn(Optional.of(ObjectBuilder.buildOrder()));

        OrderDTO result = underTest.updateOrderById(ObjectBuilder.buildOrderDTO());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(order.getId(), result.getId());
        Assertions.assertEquals(order.getTableId(), result.getTableId());
        Assertions.assertEquals(order.getCreatedAt(), result.getCreatedAt());
        Assertions.assertEquals(order.getProducts(), result.getProducts());
        Assertions.assertEquals(order.getPaymentMethod(), result.getPaymentMethod());
        Assertions.assertEquals(order.getDeletedAt(), result.getDeletedAt());
    }

    @Test
    @DisplayName("Validate if updateOrderById() throws BadRequestException when id is null")
    void updateOrderByIdFailure1() {
        OrderDTO orderDTO = ObjectBuilder.buildOrderDTO();
        orderDTO.setId(null);

        Throwable throwable = Assertions.assertThrows(BadRequestException.class, () -> {
            underTest.updateOrderById(orderDTO);
        });

        Assertions.assertEquals("Please, inform the id of the order you want to update", throwable.getMessage());
    }

    @Test
    @DisplayName("Validate if updateOrderById() throws NotFoundException when no data is found")
    void updateOrderByIdFailure2() {
        OrderDTO orderDTO = ObjectBuilder.buildOrderDTO();

        Throwable throwable = Assertions.assertThrows(NotFoundException.class, () -> {
            underTest.updateOrderById(orderDTO);
        });

        Assertions.assertEquals(String.format("The order with id %s was not found", orderDTO.getId()), throwable.getMessage());
    }

    @Test
    @DisplayName("Validate if findOrdersByTableId() returns the expected values")
    void findOrdersByTableIdSuccess() {
        List<Order> orders = ObjectBuilder.buildListOfOrder();

        Mockito.when(orderRepository.findByTableIdAndDeletedAtIsNullOrDeletedAtIsEmpty(Mockito.any(UUID.class)))
                .thenReturn(orders);

        List<OrderDTO> result = underTest.findOrdersByTableId(ObjectBuilder.buildOrder().getTableId(), false);

        Assertions.assertNotNull(result);
        Assertions.assertEquals(orders.size(), result.size());
    }

    @Test
    @DisplayName("Validate if findOrdersByTableIdSuccess() deletes the orders as expected")
    void deleteOrdersByTableIdSuccess() {
        List<Order> orders = ObjectBuilder.buildListOfOrder();

        Mockito.when(orderRepository.findByTableId(Mockito.any(UUID.class)))
                .thenReturn(orders);

        Order order = ObjectBuilder.buildOrder();
        order.setDeletedAt(ZonedDateTime.now());

        Mockito.when(orderRepository.save(Mockito.any(Order.class)))
                .thenReturn(order);

        List<OrderDTO> result = underTest.deleteOrdersByTableId(ObjectBuilder.buildOrder().getTableId());

        Assertions.assertNotNull(result);
        Assertions.assertEquals(orders.size(), result.size());

        for (OrderDTO orderDTO : result) {
            Assertions.assertNotNull(orderDTO.getDeletedAt());
        }
    }

    @Test
    @DisplayName("Validate if findOrdersByIdSuccess() deletes the order as expected")
    void deleteOrdersByIdSuccess() {
        Mockito.when(orderRepository.findById(Mockito.any(UUID.class)))
                .thenReturn(Optional.of(ObjectBuilder.buildOrder()));

        Order order = ObjectBuilder.buildOrder();
        order.setDeletedAt(ZonedDateTime.now());

        Mockito.when(orderRepository.save(Mockito.any(Order.class)))
                .thenReturn(order);

        OrderDTO result = underTest.deleteOrderById(ObjectBuilder.buildOrder().getTableId());

        Assertions.assertNotNull(result);
        Assertions.assertNotNull(result.getDeletedAt());
    }
}
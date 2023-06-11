package com.nimbleflow.api.utils;

import com.nimbleflow.api.domain.order.Order;
import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.enums.PaymentMethod;
import com.nimbleflow.api.domain.product.ProductDTO;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

public class ObjectBuilder {
    public static OrderDTO buildOrderDTO() {
        return OrderDTO.builder()
                .orderDate(ZonedDateTime.now().minusMonths(1L))
                .active(true)
                .id(new UUID(5L, 15L))
                .tableId(new UUID(7L, 15L))
                .paymentMethod(PaymentMethod.CASH)
                .products(buildListOfProductDTO())
                .build();
    }

    public static ProductDTO buildProductDTO() {
        return ProductDTO.builder()
                .id(new UUID(5L, 15L))
                .amount(5)
                .build();
    }

    public static Order buildOrder() {
        return Order.builder()
                .id(new UUID(5L, 15L))
                .active(true)
                .tableId(new UUID(5L, 15L))
                .products(buildListOfProductDTO())
                .orderDate(ZonedDateTime.now().minusMonths(1L))
                .paymentMethod(PaymentMethod.CASH)
                .build();
    }

    public static List<Order> buildListOfOrder() {
        Order order = buildOrder();
        order.setId(new UUID(7L, 15L));

        return List.of(buildOrder(), order);
    }

    public static List<ProductDTO> buildListOfProductDTO() {
        ProductDTO product = buildProductDTO();
        product.setAmount(2);
        product.setId(new UUID(5L, 15L));

        return List.of(buildProductDTO(), product);
    }

    public static List<OrderDTO> buildListOfOrderDTO() {
        OrderDTO order = buildOrderDTO();
        order.setId(new UUID(5L, 15L));

        return List.of(buildOrderDTO(), order);
    }
}
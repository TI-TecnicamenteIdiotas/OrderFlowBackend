package com.nimbleflow.api.unit;

import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.OrderService;
import com.nimbleflow.api.domain.product.ProductDTO;
import com.nimbleflow.api.domain.product.ProductService;
import com.nimbleflow.api.utils.ObjectBuilder;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;
import java.util.Random;
import java.util.UUID;

@SpringBootTest
@DisplayName("ProductService")
public class ProductServiceTest {
    @InjectMocks
    private ProductService underTest;

    @Mock
    private OrderService orderService;

    @Test
    @DisplayName("Validate if getTopSoldProducts() returns 5 products when calling it with null maxProducts")
    void getTopSoldProductsMaxProductsNull() {
        List<OrderDTO> orderDTOs = new java.util.ArrayList<>(ObjectBuilder.buildListOfOrderDTO());

        for (int i = 0; i < 10; i++) {
            ProductDTO product = ObjectBuilder.buildProductDTO();
            product.setId(new UUID(new Random().nextLong(), 15L));

            OrderDTO orderDTO = ObjectBuilder.buildOrderDTO();
            orderDTO.setProducts(new java.util.ArrayList<>(orderDTO.getProducts()));
            orderDTO.getProducts().add(product);

            orderDTOs.add(orderDTO);
        }

        Mockito.when(orderService.findAllOrders(Mockito.anyBoolean()))
                .thenReturn(orderDTOs);

        List<ProductDTO> result = underTest.getTopSoldProducts(null, false);

        Assertions.assertNotNull(result);
        Assertions.assertFalse(result.isEmpty());
        Assertions.assertEquals(5, result.size());
    }

    @Test
    @DisplayName("Validate if getTopSoldProducts() returns 2 products when calling it with maxProducts = 2")
    void getTopSoldProductsMaxProductsEqual2() {
        final int MAX_PRODUCTS = 2;

        List<OrderDTO> orderDTOs = new java.util.ArrayList<>(ObjectBuilder.buildListOfOrderDTO());

        for (int i = 0; i < 10; i++) {
            ProductDTO product = ObjectBuilder.buildProductDTO();
            product.setId(new UUID(new Random().nextLong(), 15L));

            OrderDTO orderDTO = ObjectBuilder.buildOrderDTO();
            orderDTO.setProducts(new java.util.ArrayList<>(orderDTO.getProducts()));
            orderDTO.getProducts().add(product);

            orderDTOs.add(orderDTO);
        }

        Mockito.when(orderService.findAllOrders(Mockito.anyBoolean()))
                .thenReturn(orderDTOs);

        List<ProductDTO> result = underTest.getTopSoldProducts(MAX_PRODUCTS, false);

        Assertions.assertNotNull(result);
        Assertions.assertFalse(result.isEmpty());
        Assertions.assertEquals(MAX_PRODUCTS, result.size());
    }

}
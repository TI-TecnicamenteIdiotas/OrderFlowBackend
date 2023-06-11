package com.nimbleflow.api.domain.product;

import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.OrderService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import java.util.*;

@Slf4j
@Service
@RequiredArgsConstructor
public class ProductService {

    private final OrderService orderService;

    public List<ProductDTO> getTopSoldProducts(Integer maxProducts, boolean getInactiveOrders) {
        log.info(String.format("Get top sold products (maxProducts: %s, getInactiveOrders: %s)", maxProducts, getInactiveOrders));

        List<OrderDTO> orders = orderService.findAllOrders(getInactiveOrders);

        Map<UUID, Integer> productsIdsCount = new HashMap<>();

        if (orders.isEmpty()) {
            return new ArrayList<>();
        }

        orders.forEach(orderDTO -> {
            orderDTO.getProducts().forEach(productDTO -> {
                Integer productAmount = productsIdsCount.get(productDTO.getId());

                if (productAmount == null) {
                    productsIdsCount.put(productDTO.getId(), productDTO.getAmount());
                } else {
                    productAmount += productDTO.getAmount();
                    productsIdsCount.put(productDTO.getId(), productAmount);
                }
            });
        });

        List<ProductDTO> sortedProductsByAmount = new ArrayList<>();

        for (Map.Entry<UUID, Integer> entrySet : productsIdsCount.entrySet()) {
            sortedProductsByAmount.add(ProductDTO.builder()
                    .id(entrySet.getKey())
                    .amount(entrySet.getValue())
                    .build());
        };

        sortedProductsByAmount.sort(Comparator.comparingInt(ProductDTO::getAmount));

        sortedProductsByAmount = getFilteredProductsByMaxOfProductsArray(sortedProductsByAmount, maxProducts);

        return sortedProductsByAmount;
    }

    private List<ProductDTO> getFilteredProductsByMaxOfProductsArray(List<ProductDTO> products, Integer maxProducts) {
        if (maxProducts != null) {
            if (products.size() > maxProducts) {
                products = products.subList(0, maxProducts);
            }
        } else {
            if (products.size() > 5) {
                products = products.subList(0, 5);
            }
        }

        return products;
    }

}

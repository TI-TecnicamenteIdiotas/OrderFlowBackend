package com.nimbleflow.api.unit;

import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.OrderService;
import com.nimbleflow.api.domain.product.ProductDTO;
import com.nimbleflow.api.domain.product.ProductService;
import com.nimbleflow.api.domain.report.ReportService;
import com.nimbleflow.api.domain.report.dto.ReportDTO;
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

@SpringBootTest
@DisplayName("ReportService")
public class ReportServiceTest {
    @InjectMocks
    private ReportService underTest;

    @Mock
    private OrderService orderService;

    @Mock
    private ProductService productService;

    @Test
    @DisplayName("Validate if getOrdersMonthReportSuccess() returns the expected values when there's registered orders in the month")
    void getOrdersMonthReportSuccess() {
        Mockito.when(orderService.getAllMothOrders(Mockito.anyBoolean()))
                .thenReturn(ObjectBuilder.buildListOfOrderDTO());

        ReportDTO<OrderDTO> result = underTest.getOrdersMonthReport(false);

        Assertions.assertNotNull(result);
        Assertions.assertNotNull(result.getCsv());
        Assertions.assertNotNull(result.getItems());
        Assertions.assertEquals(2, result.getItems().size());
    }

    @Test
    @DisplayName("Validate if getOrdersReportByIntervalSuccess() returns the expected values when there's registered orders in the interval")
    void getOrdersReportByIntervalSuccess() {
        Mockito.when(orderService.findOrdersByInterval(Mockito.any(ZonedDateTime.class), Mockito.any(ZonedDateTime.class), Mockito.anyBoolean()))
                .thenReturn(ObjectBuilder.buildListOfOrderDTO());

        ReportDTO<OrderDTO> result = underTest.getOrdersReportByInterval(ZonedDateTime.now().minusMonths(1), ZonedDateTime.now(), false);

        Assertions.assertNotNull(result);
        Assertions.assertNotNull(result.getCsv());
        Assertions.assertNotNull(result.getItems());
        Assertions.assertEquals(2, result.getItems().size());
    }

    @Test
    @DisplayName("Validate if getTopSoldProductsReport() returns the expected values")
    void getTopSoldProductsReportSuccess() {
        List<ProductDTO> productDTOs = new java.util.ArrayList<>(ObjectBuilder.buildListOfProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());
        productDTOs.add(ObjectBuilder.buildProductDTO());

        Mockito.when(productService.getTopSoldProducts(Mockito.any(), Mockito.anyBoolean()))
                .thenReturn(productDTOs);

        ReportDTO<ProductDTO> result = underTest.getTopSoldProductsReport(null, false);

        Assertions.assertNotNull(result);
        Assertions.assertNotNull(result.getCsv());
        Assertions.assertNotNull(result.getItems());
    }
}
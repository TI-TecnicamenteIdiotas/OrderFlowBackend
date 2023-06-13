package com.nimbleflow.api.domain.report;

import com.nimbleflow.api.domain.order.OrderDTO;
import com.nimbleflow.api.domain.order.OrderService;
import com.nimbleflow.api.domain.product.ProductDTO;
import com.nimbleflow.api.domain.product.ProductService;
import com.nimbleflow.api.domain.report.dto.OrderReportDTO;
import com.nimbleflow.api.domain.report.dto.ReportDTO;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.csv.CSVFormat;
import org.apache.commons.csv.CSVPrinter;
import org.springframework.stereotype.Service;

import java.io.ByteArrayOutputStream;
import java.io.OutputStreamWriter;
import java.lang.reflect.Field;
import java.nio.charset.StandardCharsets;
import java.time.ZonedDateTime;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Slf4j
@Service
@RequiredArgsConstructor
public class ReportService {
    private final OrderService orderService;
    private final ProductService productService;

    public ReportDTO<OrderDTO> getOrdersMonthReport(boolean getInactiveOrders) {
        List<OrderDTO> orders = orderService.getAllMothOrders(getInactiveOrders);
        return getOrderDTOReportDTO(orders);
    }

    public ReportDTO<OrderDTO> getOrdersReportByInterval(ZonedDateTime startDate, ZonedDateTime endDate, boolean getInactiveOrders) {
        List<OrderDTO> orders = orderService.findOrdersByInterval(startDate, endDate, getInactiveOrders);
        return getOrderDTOReportDTO(orders);
    }

    public ReportDTO<ProductDTO> getTopSoldProductsReport(Integer maxProducts, boolean getInactiveOrders) {
        List<ProductDTO> products = productService.getTopSoldProducts(maxProducts, getInactiveOrders);

        if (products.isEmpty()) {
            return ReportDTO.<ProductDTO>builder().build();
        }

        Map<String, String> headersAndRespectiveAttributes = new HashMap<>();
        headersAndRespectiveAttributes.put("Product id", "id");
        headersAndRespectiveAttributes.put("Total amount", "amount");

        return ReportDTO.<ProductDTO>builder()
                .items(products)
                .csv(convertListOfItemsToCSV(products, headersAndRespectiveAttributes))
                .build();
    }

    private ReportDTO<OrderDTO> getOrderDTOReportDTO(List<OrderDTO> orders) {
        if (orders.isEmpty()) {
            return ReportDTO.<OrderDTO>builder().build();
        }

        Map<String, String> headersAndRespectiveAttributes = new HashMap<>();
        headersAndRespectiveAttributes.put("Order id", "id");
        headersAndRespectiveAttributes.put("Table id", "tableId");
        headersAndRespectiveAttributes.put("Creation date", "createdAt");
        headersAndRespectiveAttributes.put("Payment method", "paymentMethod");
        headersAndRespectiveAttributes.put("Products ids and amount", "productsIdsAndAmount");
        headersAndRespectiveAttributes.put("Deletion date", "deletedAt");

        List<OrderReportDTO> reports = new ArrayList<>();

        orders.forEach(orderDTO -> {
            reports.add(buildOrderReportDTO(orderDTO));
        });

        return ReportDTO.<OrderDTO>builder()
                .items(orders)
                .csv(convertListOfItemsToCSV(reports, headersAndRespectiveAttributes))
                .build();
    }

    private OrderReportDTO buildOrderReportDTO(OrderDTO orderDTO) {
        OrderReportDTO reportDTO = OrderReportDTO.builder()
                .createdAt(orderDTO.getCreatedAt())
                .id(orderDTO.getId())
                .deletedAt(orderDTO.getDeletedAt())
                .paymentMethod(orderDTO.getPaymentMethod())
                .tableId(orderDTO.getTableId())
                .build();

        for (int i = 0; i < orderDTO.getProducts().size(); i++) {
            ProductDTO productDTO = orderDTO.getProducts().get(i);

            if (orderDTO.getProducts().size() > 1 && (i + 1) < orderDTO.getProducts().size()) {
                reportDTO.setProductsIdsAndAmount(String.format("%s: %s\n", productDTO.getId(), productDTO.getAmount()));
            } else {
                reportDTO.setProductsIdsAndAmount(String.format("%s: %s", productDTO.getId(), productDTO.getAmount()));
            }
        }

        return reportDTO;
    }

    @SneakyThrows
    private String convertListOfItemsToCSV(List<?> items, Map<String, String> headersAndRespectiveAttributes) {
        log.info(String.format("Converting list of items to CSV (items: %s, headersAndRespectiveAttributes: %s)", items, headersAndRespectiveAttributes));

        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        OutputStreamWriter writer = new OutputStreamWriter(outputStream, StandardCharsets.UTF_8);

        CSVFormat csvFormat = CSVFormat.Builder.create().setHeader(headersAndRespectiveAttributes.keySet().toArray(String[]::new)).build();
        CSVPrinter csvPrinter = new CSVPrinter(writer, csvFormat);
        List<Map<String, Object>> fieldsAndValues = new ArrayList<>();

        for (Object item : items) {
            Class<?> itemClass = item.getClass();

            Field[] fields = itemClass.getDeclaredFields();
            HashMap<String, Object> fieldAndValue = new HashMap<>();
            for (Field field : fields) {
                field.setAccessible(true);
                String fieldName = field.getName();
                Object fieldValue;
                try {
                    fieldValue = field.get(item);
                    if (fieldValue == null) fieldValue = "";
                    fieldAndValue.put(fieldName, fieldValue);
                } catch (IllegalAccessException e) {
                    e.printStackTrace();
                }
            }

            fieldsAndValues.add(fieldAndValue);
        }

        for (Map<String, Object> map : fieldsAndValues) {
            List<Object> valuesToSetOnReport = new ArrayList<>();

            for (Map.Entry<String, String> entrySet : headersAndRespectiveAttributes.entrySet()) {
                valuesToSetOnReport.add(map.get(entrySet.getValue()));
            }

            csvPrinter.printRecord(valuesToSetOnReport);
        }

        csvPrinter.flush();
        csvPrinter.close();

        return outputStream.toString();
    }
}
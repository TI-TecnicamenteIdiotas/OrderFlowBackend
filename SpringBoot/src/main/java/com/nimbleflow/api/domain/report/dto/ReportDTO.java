package com.nimbleflow.api.domain.report.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.*;

import java.util.ArrayList;
import java.util.List;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class ReportDTO<T> {

    @Schema(example = "Deletion date,Creation date,Order id,Table id,Payment method,Products ids and amount\n" +
            "\"\",2023-05-12T21:05:59.258452200-03:00[America/Sao_Paulo],00000000-0000-0005-0000-00000000000f,00000000-0000-0007-0000-00000000000f,CASH,00000000-0000-0005-0000-00000000000f: 2\n" +
            "\"\",2023-05-12T21:05:59.258452200-03:00[America/Sao_Paulo],00000000-0000-0005-0000-00000000000f,00000000-0000-0007-0000-00000000000f,CASH,00000000-0000-0005-0000-00000000000f: 2\n")
    private String csv;

    @Builder.Default
    private List<T> items = new ArrayList<>();

}

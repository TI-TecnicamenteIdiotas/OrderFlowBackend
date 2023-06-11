package com.nimbleflow.api.domain.report.dto;

import lombok.*;

import java.util.ArrayList;
import java.util.List;

@Data
@Builder
@EqualsAndHashCode
@NoArgsConstructor
@AllArgsConstructor
public class ReportDTO<T> {

    private String csv;

    @Builder.Default
    private List<T> items = new ArrayList<>();

}

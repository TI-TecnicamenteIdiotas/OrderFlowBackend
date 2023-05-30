package com.nimbleflow.api.integration;

import com.nimbleflow.api.ApiApplication;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.junit.jupiter.SpringExtension;

@ActiveProfiles("docker")
@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = ApiApplication.class)
public class PurchaseTest {


    
}

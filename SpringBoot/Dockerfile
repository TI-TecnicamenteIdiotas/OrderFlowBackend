FROM maven:3.9.2-eclipse-temurin-17-alpine AS build

WORKDIR /build
COPY . .

RUN mvn clean package -DskipTests

FROM maven:3.9.2-eclipse-temurin-17-alpine AS custom-jre

# required for strip-debug to work
RUN apk add --no-cache binutils

# Build small JRE image
RUN ${JAVA_HOME}/bin/jlink \
         --verbose \
         --add-modules ALL-MODULE-PATH \
         --strip-debug \
         --no-man-pages \
         --no-header-files \
         --compress=2 \
         --output /jre

FROM alpine:latest

ENV JAVA_HOME=/jre
ENV PATH="${JAVA_HOME}/bin:${PATH}"

# copy JRE from the custom-jre builder
COPY --from=custom-jre /jre/ $JAVA_HOME

WORKDIR /app

COPY --from=build /build/target/nimble-flow-backend-3.0.5.jar /app/nimble-flow-backend-3.0.5.jar
ENTRYPOINT ["java", "-jar", "nimble-flow-backend-3.0.5.jar", "--spring.profiles.active=container"]
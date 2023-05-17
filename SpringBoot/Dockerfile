FROM openjdk:17-alpine

# Instala o Maven
RUN apk update && apk add --no-cache maven

# Cria um diretório de trabalho na imagem
WORKDIR /app

# Copia o código-fonte da aplicação para a imagem
COPY . .

# Compila a aplicação e todas as suas dependências usando o Maven
RUN mvn clean package -DskipTests

EXPOSE 8080

# Executa a aplicação com o application.properties com o sufixo production na porta 8080 do container
CMD java -jar target/nimble-flow-backend-3.0.5.jar --spring.profiles.active=docker --server.port=8080

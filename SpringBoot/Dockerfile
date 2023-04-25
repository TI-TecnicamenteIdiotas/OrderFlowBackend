# Imagem base
FROM ubuntu:latest

# Atualiza o sistema e instala o Java e o Maven
RUN apt-get update && \
    apt-get install -y openjdk-17-jdk maven

# Cria e navega para o diretório de trabalho
WORKDIR /app

# Copia o código fonte para o container
COPY . .

# Baixa todas as dependências necessárias para executar a aplicação
RUN mvn clean package

# Define a variável de ambiente JASYPT_ENCRYPTION_KEY com o valor da variável de ambiente correspondente
ARG JASYPT_ENCRYPTION_KEY
ENV JASYPT_ENCRYPTION_KEY=${JASYPT_ENCRYPTION_KEY}

# Inicia a aplicação no container com o application.properties com o sufixo production na porta 8080
CMD ["java", "-jar", "/app/target/nimble-flow-backend.jar", "--spring.profiles.active=production", "--server.port=8080"]

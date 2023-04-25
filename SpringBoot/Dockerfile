# Define a imagem base a ser utilizada
FROM openjdk:17-jdk-slim

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia o arquivo pom.xml do diretório local para o diretório de trabalho do container
COPY pom.xml .

# Instala o Maven no container
RUN apt-get update && \
    apt-get install -y maven

# Baixa todas as dependências necessárias para executar a aplicação no container
RUN mvn clean install -Dskiptests
RUN mvn dependency:go-offline

# Copia todo o restante dos arquivos da aplicação para o diretório de trabalho do container
COPY . .

# Define a variável de ambiente JASYPT_ENCRYPTION_KEY
ENV JASYPT_ENCRYPTION_KEY=$JASYPT_ENCRYPTION_KEY

# Expõe a porta 8080 do container
EXPOSE 8080

# Inicia a aplicação com o sufixo production na porta 8080
CMD ["mvn", "spring-boot:run", "-Dspring-boot.run.profiles=production"]
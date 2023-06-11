FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

ENV PROTOBUF_PROTOC=/usr/bin/protoc
ENV GRPC_PROTOC_PLUGIN=/usr/bin/grpc_csharp_plugin
ENV gRPC_PluginFullPath=/usr/bin/grpc_csharp_plugin
RUN apk add protobuf protobuf-dev grpc grpc-plugins

WORKDIR /build

COPY *.sln ./
COPY ./Src ./Src

RUN dotnet restore "Src/NimbleFlowHub.Api/NimbleFlowHub.Api.csproj"
RUN dotnet publish "Src/NimbleFlowHub.Api/NimbleFlowHub.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NimbleFlowHub.Api.dll"]
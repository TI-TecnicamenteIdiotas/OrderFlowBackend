FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /build

COPY *.sln ./
COPY ./Src ./Src

RUN dotnet restore "Src/NimbleFlow.Api/NimbleFlow.Api.csproj"
RUN dotnet publish "Src/NimbleFlow.Api/NimbleFlow.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NimbleFlow.Api.dll"]
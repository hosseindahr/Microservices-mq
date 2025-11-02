#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Order/Presentation/Order.Api/Order.Api.csproj", "Services/Order/Presentation/Order.Api/"]
COPY ["Services/Order/Application/Models/Models.csproj", "Services/Order/Application/Models/"]
COPY ["Services/Order/Infrastructure/Messaging/Messaging/Messaging.csproj", "Services/Order/Infrastructure/Messaging/Messaging/"]
COPY ["Services/Order/Infrastructure/Messaging/RabbitMQService/RabbitMQService.csproj", "Services/Order/Infrastructure/Messaging/RabbitMQService/"]
RUN dotnet restore "Services/Order/Presentation/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/Services/Order/Presentation/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]
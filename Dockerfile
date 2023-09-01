#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PaymentsCoreApi/PaymentsCoreApi.csproj", "PaymentsCoreApi/"]
COPY ["PaymentsCoreApi.Data/PaymentsCoreApi.Data.csproj", "PaymentsCoreApi.Data/"]
COPY ["PaymentsCoreApi.Domain/PaymentsCoreApi.Domain.csproj", "PaymentsCoreApi.Domain/"]
COPY ["PaymentsCoreApi.Logic/PaymentsCoreApi.Logic.csproj", "PaymentsCoreApi.Logic/"]
RUN dotnet restore "PaymentsCoreApi/PaymentsCoreApi.csproj"
COPY . .
WORKDIR "/src/PaymentsCoreApi"
RUN dotnet build "PaymentsCoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsCoreApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsCoreApi.dll"]

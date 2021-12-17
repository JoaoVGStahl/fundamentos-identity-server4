#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SkyCommerce.Frete/SkyCommerce.Frete.csproj", "SkyCommerce.Frete/"]
RUN dotnet restore "SkyCommerce.Frete/SkyCommerce.Frete.csproj"
COPY . .
WORKDIR "/src/SkyCommerce.Frete"
RUN dotnet build "SkyCommerce.Frete.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkyCommerce.Frete.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkyCommerce.Frete.dll"]
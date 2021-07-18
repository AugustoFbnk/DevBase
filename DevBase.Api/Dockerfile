#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DevBase.Api/DevBase.Api.csproj", "DevBase.Api/"]
COPY ["DevBase.Domain/DevBase.Domain.csproj", "DevBase.Domain/"]
COPY ["DevBase.Services/DevBase.Services.csproj", "DevBase.Services/"]
COPY ["DevBase.Infra.Data/DevBase.Infra.Data.csproj", "DevBase.Infra.Data/"]
RUN dotnet restore "DevBase.Api/DevBase.Api.csproj"
COPY . .
WORKDIR "/src/DevBase.Api"
RUN dotnet build "DevBase.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevBase.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevBase.Api.dll"]

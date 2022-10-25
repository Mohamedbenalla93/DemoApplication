#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DemoApplication/DemoApplication.WebApplication1/DemoApplication.WebApplication1.csproj", "DemoApplication/DemoApplication.WebApplication1/"]
COPY ["DemoApplication/DemoAppliaction.Application/DemoApplication.Core.Application.csproj", "DemoApplication/DemoApplication.Application/"]
COPY ["DemoApplication/DemoApplication.Domain/DemoApplication.Core.Domain.csproj", "DemoApplication/DemoApplication.Domain/"]
COPY ["DemoApplication/DemoApplication.Infrastructure.Persistence/DemoApplication.Infrastructure.Persistence.csproj", "DemoApplication/DemoApplication.Infrastructure.Persistence/"]
COPY ["DemoApplication/Application.Core.DomainServices/Application.Core.DomainServices.csproj", "DemoApplication/DemoApplication.DomainServices/"]
RUN dotnet restore "DemoApplication/DemoApplication.WebApplication1/DemoApplication.WebApplication1.csproj"
COPY . .
WORKDIR "/src/DemoApplication/DemoApplication.WebApplication1"
RUN dotnet build "DemoApplication.WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoApplication.WebApplication1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoApplication.WebApplication1.dll"]
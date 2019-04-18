FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
FROM microsoft/dotnet:2.1-sdk AS build
ADD . /src
WORKDIR /src

COPY ["Fluendo.FluendoPlatform.Core.WebApi/Fluendo.FluendoPlatform.Core.WebApi.csproj", "Fluendo.FluendoPlatform.Core.WebApi/"]
COPY ["Fluendo.FluendoPlatform.Infrastructure.Common/Fluendo.FluendoPlatform.Infrastructure.Common.csproj", "Fluendo.FluendoPlatform.Infrastructure.Common/"]
COPY ["Fluendo.FluendoPlatform.Core.App/Fluendo.FluendoPlatform.Core.App.csproj", "Fluendo.FluendoPlatform.Core.App/"]
RUN dotnet restore "Fluendo.FluendoPlatform.Core.WebApi/Fluendo.FluendoPlatform.Core.WebApi.csproj"

COPY . .
WORKDIR "/src/Fluendo.FluendoPlatform.Core.WebApi"
RUN dotnet build "Fluendo.FluendoPlatform.Core.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fluendo.FluendoPlatform.Core.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fluendo.FluendoPlatform.Core.WebApi.dll"]
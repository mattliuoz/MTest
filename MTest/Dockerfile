FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 10660
EXPOSE 44342

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MTest/MTest.csproj MTest/
RUN dotnet restore MTest/MTest.csproj
COPY . .
WORKDIR /src/MTest
RUN dotnet build MTest.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MTest.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MTest.dll"]

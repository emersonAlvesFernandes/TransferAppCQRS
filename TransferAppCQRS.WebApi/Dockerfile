FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 55833
EXPOSE 44375

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TransferAppCQRS.WebApi/TransferAppCQRS.WebApi.csproj", "TransferAppCQRS.WebApi/"]
RUN dotnet restore "TransferAppCQRS.WebApi/TransferAppCQRS.WebApi.csproj"
COPY . .
WORKDIR "/src/TransferAppCQRS.WebApi"
RUN dotnet build "TransferAppCQRS.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TransferAppCQRS.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TransferAppCQRS.WebApi.dll"]
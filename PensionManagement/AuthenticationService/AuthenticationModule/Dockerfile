#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuthenticationService/AuthenticationModule/AuthenticationModule.csproj", "AuthenticationService/AuthenticationModule/"]
RUN dotnet restore "AuthenticationService/AuthenticationModule/AuthenticationModule.csproj"
COPY . .
WORKDIR "/src/AuthenticationService/AuthenticationModule"
RUN dotnet build "AuthenticationModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationModule.dll"]

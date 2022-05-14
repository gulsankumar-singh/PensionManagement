#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PensionerDetailService/PensionerDetailModule/PensionerDetailModule.csproj", "PensionerDetailService/PensionerDetailModule/"]
RUN dotnet restore "PensionerDetailService/PensionerDetailModule/PensionerDetailModule.csproj"
COPY . .
WORKDIR "/src/PensionerDetailService/PensionerDetailModule"
RUN dotnet build "PensionerDetailModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PensionerDetailModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PensionerDetailModule.dll"]

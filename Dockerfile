#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CustomerAPI.csproj", "."]
RUN dotnet restore "./CustomerAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CustomerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerAPI.csproj" -c Release -o /app/publish

FROM base AS final
RUN date
ENV TZ=Asia/Calcutta
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN date
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]

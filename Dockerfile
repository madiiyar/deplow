# Use Microsoft's official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use Microsoft's official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["deployWithoutDB/deployWithoutDB.csproj", "deployWithoutDB/"]
RUN dotnet restore "deployWithoutDB/deployWithoutDB.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/deployWithoutDB"
RUN dotnet publish "deployWithoutDB.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "deployWithoutDB.dll"]

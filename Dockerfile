FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY BusManagement.Presentation.API/BusManagement.Presentation.API.csproj . 
RUN dotnet restore

# Copy everything else and build app
COPY . .

# Start the application in watch mode
CMD ["dotnet", "watch", "run", "--project", "BusManagement.Presentation.API/BusManagement.Presentation.API.csproj", "--urls", "http://*:80"]
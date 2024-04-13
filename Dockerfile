FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything else and build app
COPY . .
RUN dotnet restore BusManagement.Presentation.API/BusManagement.Presentation.API.csproj

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
    
# Start the application in watch mode
CMD ["dotnet", "watch", "run", "--project", "BusManagement.Presentation.API/BusManagement.Presentation.API.csproj", "--urls", "http://*:80"]
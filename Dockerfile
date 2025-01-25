FROM mcr.microsoft.com/dotnet/sdk:8.0.101

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

EXPOSE 8080

ENTRYPOINT [ "dotnet", "backend.dll" ]
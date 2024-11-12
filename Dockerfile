# Estágio 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo de projeto e restaurar as dependências
COPY ["ControledeSalaFEMASS/ControledeSalaFEMASS.csproj", "ControledeSalaFEMASS/"]
RUN dotnet restore "ControledeSalaFEMASS/ControledeSalaFEMASS.csproj"

# Copiar todo o código e construir a aplicação
COPY . .
WORKDIR "/src/ControledeSalaFEMASS"
RUN dotnet build "ControledeSalaFEMASS.csproj" -c Release -o /app/build

# Publicar a aplicação
RUN dotnet publish "ControledeSalaFEMASS.csproj" -c Release -o /app/publish

# Estágio 2: Criar a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ControledeSalaFEMASS.dll"]

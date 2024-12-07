# Sobre:
O projeto consiste numa aplicação com interface gráfica no navegador web (front-end) com chamadas API para o back-end, ambos sendo executados de forma local.

Projeto requisitado pelo professor de Desenvolvimento de Sistemas I como requisito para conclusão de disciplina.


# Tecnologias utiliazdas no back-end:
<img src="https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff" width="100" />
<br><br>
<img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927" width="150" />
<br><br>
<img src="https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white" width="70" />
<br><br>
<img src="https://img.shields.io/badge/swagger-gray?logo=swagger" width="100" />
<br><br>

# Executando o projeto:
## Pré-requisitos

- **Docker**: Certifique-se de que Docker está instalado. [Download Docker](https://www.docker.com/products/docker-desktop)  
- **Docker Compose**: Certifique-se de que Docker Compose está instalado (geralmente incluído no pacote do Docker Desktop).

## Para configurar o banco e a API do projeto

1. **Clone o Repositório**  
   Clone o repositório do projeto:  
```bash 
git clone https://github.com/pimentahugo/ControledeSalaFEMASS
```
## Build e Execute os Containers

Na raiz do repositório clonado, execute os seguintes comandos para construir e iniciar os containers:

```bash 
cd ./ControledeSalaFEMASS
docker-compose build
docker-compose up
```

## Acessando a API

Uma vez que os containers estejam em execução, a API estará disponível em:  
http://localhost:5000/swagger/index.html

Mesmo sendo executado localmente, a API serve como intermediária para a comunicação entre o front-end (interface do usuário) e o back-end (servidor e banco de dados). Ela permite que o front-end envie solicitações (como buscar ou enviar dados) ao back-end, e que o back-end responda com os dados ou resultados esperados.

Nota: O acesso à API se destina a testes e não é necessário para executar a aplicação.

## Testar a API

Use ferramentas como Postman ou Insomnia para testar os end-points:

URL Base: http://localhost:5000/

## Parar o container

Para parar os containers em execução, pressione Ctrl + C no terminal ou use o comando abaixo:
```bash 
docker-compose down
```
## Possíveis problemas e soluções

- Porta Ocupada:  
Caso a porta 5000 esteja sendo usada por outro serviço, edite o arquivo docker-compose.yml e altere a porta para outra disponível.

- Erro ao construir containers:  
Verifique se o Docker está ativo e se os arquivos necessários foram clonados corretamente. Caso tenha outro problema, consulte a [documentação oficial do Docker](https://docs.docker.com/) .

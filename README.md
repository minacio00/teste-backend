# Projeto de Backend com .NET e Docker

Este é um projeto de backend desenvolvido em .NET 8. O projeto foi configurado para ser executado facilmente com Docker, garantindo que todas as dependências e configurações estejam prontas para uso.

## Requisitos

Antes de iniciar, certifique-se de ter os seguintes softwares instalados:

- **Docker**: Para criar e gerenciar os containers.
- **Docker Compose**: Para orquestrar a aplicação e suas dependências (como o banco de dados PostgreSQL).

## configurando o envio de emails:
Este projeto inclui funcionalidade para envio de emails utilizando SMTP. Para que o envio de emails funcione corretamente, você precisará configurar as credenciais SMTP no arquivo appsettings.json.
Abra o arquivo appsettings.json na raiz do projeto e edite a seção "Smtp" com as suas credenciais

## Como Rodar o Projeto

### 1. Clonar o Repositório

Primeiro, clone o repositório para a sua máquina local:

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio

### 2 - Executasndo o projeto com docker:
```bash
docker-compose up --build

### 3 - Acessando a API
```bash
http://localhost:5000/swagger

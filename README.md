
# Processo Seletivo - 2025

Este repositório contém a implementação do desafio técnico para a vaga FullStack no processo seletivo da SEALS de 2025. O desafio abrange o desenvolvimento de uma aplicação FullStack utilizando .NET para o back-end e React com Vite para o front-end, com foco na gestão de DUVs (Documentos Únicos Virtuais), navios e passageiros.

## Estrutura do Repositório

O repositório é dividido em duas principais seções:
1. **Back-End**: API desenvolvida em .NET Core, utilizando Entity Framework para manipulação de dados.
2. **Front-End**: Interface de usuário desenvolvida em React com Vite para exibição das DUVs, navios e passageiros.

---

## Back-End

### Tecnologias Utilizadas
- **.NET Core**: Para criação da API.
- **Entity Framework Core**: Para manipulação de dados e mapeamento objeto-relacional.
- **PostgreSQL**: Banco de dados utilizado para armazenar as informações das DUVs, navios e passageiros.
- **Swagger**: Para documentação da API e facilitar o teste dos endpoints.

### Funcionalidades Implementadas
- **CRUD das entidades**:
  - **DUV**: Implementação de operações CRUD para gerenciar DUVs.
  - **Navio**: Implementação de operações CRUD para gerenciar navios.
  - **Passageiro**: Implementação de operações CRUD para gerenciar passageiros.

- **Endpoints principais**:
  - `GET /api/duvs`: Lista todas as DUVs.
  - `GET /api/duvs/{guid}`: Exibe os detalhes de uma DUV específica.
  - `POST /api/duvs`: Cria uma nova DUV.
  - `PUT /api/duvs/{guid}`: Atualiza uma DUV existente.
  - `DELETE /api/duvs/{guid}`: Exclui uma DUV.

  - **Navio**:
    - `GET /api/navios`: Lista todos os navios.
    - `POST /api/navios`: Cria um novo navio.
    - `PUT /api/navios/{guid}`: Atualiza informações de um navio.
    - `DELETE /api/navios/{guid}`: Exclui um navio.

  - **Passageiro**:
    - `GET /api/passageiros`: Lista todos os passageiros.
    - `GET /api/passageiros/{guid}`: Exibe os detalhes de um passageiro específico.
    - `POST /api/passageiros`: Cria um novo passageiro.
    - `PUT /api/passageiros/{guid}`: Atualiza informações de um passageiro.
    - `DELETE /api/passageiros/{guid}`: Exclui um passageiro.

- **Validação e Erros**: Adicionada validação customizada para as entidades **DUV**, **Navio** e **Passageiro**, com tratamento adequado de erros de validação no back-end.

- **Mudança de Identificadores**: Alteração nas entidades para usar **Guid** como identificador público (substituindo o uso de **Id**).

- **Swagger**: A API pode ser acessada via Swagger para facilitar o teste dos endpoints na URL: `https://localhost:7204/swagger/index.html`.

---

## Front-End

### Tecnologias Utilizadas
- **React**: Para a construção da interface de usuário.
- **Vite**: Para o bundling e desenvolvimento rápido.
- **Material-UI**: Para componentes de UI estilizados.
- **Axios**: Para realizar requisições HTTP à API.
- **React Router**: Para navegação entre as páginas da aplicação.

### Funcionalidades Implementadas
- **Listagem de DUVs**: Exibição de todas as DUVs cadastradas com informações como número da DUV e navio.
- **Detalhes da DUV**: Exibição das informações detalhadas de uma DUV selecionada, incluindo navio e passageiros.
- **Separação de Passageiros e Tripulantes**: Visualmente, passageiros e tripulantes são exibidos separadamente na interface.
- **Integração com a API**: A interface front-end consome a API desenvolvida no back-end para exibir e interagir com as DUVs, navios e passageiros.

- **Layout Institucional**: A interface foi desenvolvida com base no estilo institucional da SEALS, com um layout limpo e funcional.

- **Acessibilidade de UI**: Adição de botões para criar, editar e excluir passageiros, navios e DUVs.

- **Vite**: O front-end utiliza Vite para um desenvolvimento mais rápido e eficiente. Para rodar o front-end, use o comando:
  ```bash
  npm run dev
  ```

- **Acesso ao Front-End**: O front-end estará disponível em `http://localhost:5173`.

---

## Como Rodar o Projeto

### Back-End
1. Clone o repositório e navegue até a pasta do back-end.
2. Execute o comando para restaurar as dependências do projeto:
   ```bash
   dotnet restore
   ```
3. Crie o banco de dados e execute a migração:
   ```bash
   dotnet ef database update
   ```
4. Inicie a API:
   ```bash
   dotnet run
   ```
5. A API estará disponível em `https://localhost:7204`.

### Front-End
1. Clone o repositório e navegue até a pasta do front-end.
2. Instale as dependências:
   ```bash
   npm install
   ```
3. Inicie o servidor de desenvolvimento:
   ```bash
   npm run dev
   ```
4. O front-end estará disponível em `http://localhost:5173`.

---

## Demonstrações

### Front-End
![Front-end demonstration](https://github.com/user-attachments/assets/b904f50c-0c18-4be9-8947-0e1bb6e95641)

### Back-End
![back-end demonstration](https://github.com/user-attachments/assets/9b24915e-9fdf-4919-ada3-781c4b34b33f)

---

## Conclusão

Este repositório contém a implementação do desafio técnico do processo seletivo, abrangendo tanto o back-end quanto o front-end. Ambas as partes foram estruturadas de acordo com as melhores práticas de desenvolvimento e focando em uma arquitetura limpa e escalável.

---

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

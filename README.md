# Sobre o Projeto


Este projeto foi concebido para servir como um gerenciador de dados pessoais, incluindo números de telefone para contato, e o status de cadastro correspondente. Através desse sistema, os dados são armazenados de forma segura e acessível, facilitando as operações diárias de empresas que precisam manter contato com seus clientes.

# API InfoManager

Esta iniciativa consiste em uma API dedicada a um gerenciador de informações, projetada especificamente para aprimorar a gestão de clientes e promover uma otimização eficaz das operações diárias em empresas. Ao permitir a criação de usuários, o sistema facilita o acesso a várias rotas após a autenticação, fornecendo funcionalidades abrangentes para a manipulação dos dados das pessoas, incluindo operações de criação, consulta, edição e exclusão (CRUD).


https://github.com/TrizCes/infoManager-Lar/assets/115851354/43376e5c-971c-4edc-b698-360b84697911


### Banco de dados

Para gerenciar os dados, optei por utilizar o SQL Server e desenvolvi um banco de dados denominado InfoManagerDb, empregando o método Code First para assegurar um controle mais preciso sobre sua estrutura. Abaixo, você pode conferir a modelagem lógica do banco de dados:

![Database](https://github.com/TrizCes/infoManager-Lar/assets/115851354/c81d1827-cea1-434d-b282-c2185f96c1eb)

**As migrations estão disponíveis no projeto.**

### Segurança

A API adota um robusto método de criptografia para as senhas dos usuários, utilizando o algoritmo de hash SHA-256 (Secure Hash Algorithm 256 bits). As senhas são armazenadas de forma criptografada no banco de dados, o que torna extremamente difícil para terceiros obtê-las. A criptografia SHA-256 é reconhecida pela sua segurança e complexidade, proporcionando uma camada adicional de proteção aos dados sensíveis dos usuários.

Além disso, a segurança é reforçada com o uso de Tokens de autenticação para proteger as rotas e os dados da API. Cada funcionalidade da API verifica o token de autenticação do usuário logado, que deve ser enviado no cabeçalho da requisição no formato Bearer Token. Essa abordagem garante que somente usuários autorizados tenham acesso às funcionalidades da API, protegendo assim a integridade e confidencialidade dos dados.

#### Token de Autenticação:

Toda requisição feita à API precisa ser autenticada por meio de um token de autenticação, o qual é gerado no momento do login e deve ser incluído no cabeçalho de todas as requisições. A biblioteca escolhida para lidar com tokens de autenticação foi a Microsoft.IdentityModel.Tokens. Esta biblioteca provê classes e métodos para gerar e validar tokens JWT (JSON Web Tokens), além de oferecer suporte para a criação de credenciais de assinatura de token.

#### Guia de Status Codes ultilizados:

```javascript
// 200 (OK) = requisição bem sucedida
// 201 (Created) = requisição bem sucedida e algo foi criado
// 204 (No Content) = requisição bem sucedida, sem conteúdo no corpo da resposta
// 400 (Bad Request) = o servidor não entendeu a requisição pois está com uma sintaxe/formato inválido
// 401 (Unauthorized) = o usuário não está autenticado (logado)
// 403 (Forbidden) = o usuário não tem permissão de acessar o recurso solicitado
// 404 (Not Found) = o servidor não pode encontrar o recurso solicitado
// 500 (Internal Server Error) = erro inesperado do servidor
```

## Tecnologias

`C#` `SQLServer`

# Para ultilizar na sua máquina

### Instações recomendadas:

- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/);
- [SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads);

## Passo a passo :

- Faça o download do repositório para o seu computador;
- Abra o Visual Studio 2022;
- Abra a solução do projeto dentro do Visual Studio;
- Certifique-se de que o SQL Server está instalado e em execução em sua máquina;
- Execute as migrações para criar o banco de dados InfoManagerDb. Isso pode ser feito usando o console do Gerenciador de Pacotes NuGet com o comando Update-Database;
- Inicie o projeto API InfoManager no Visual Studio;
- Após o projeto ser iniciado, você pode acessar a API usando um cliente HTTP, como Postman ou Insomnia, ou até mesmo o Swagger incorporado, que pode ser acessado através da URL da API seguida de /swagger/index.html, onde você pode testar todas as rotas disponíveis;
    - `No Swagger`:
    - Você verá a interface do Swagger, onde poderá explorar e testar as rotas disponíveis na API.
    - Antes de fazer qualquer requisição protegida, certifique-se de estar autenticado. Para isso, vá até a rota de autenticação e forneça as credenciais necessárias para obter o token JWT.
    - Com o token JWT obtido, clique no botão "Authorize" na interface do Swagger e insira o token no formato Bearer {seu-token}.
- Certifique-se de incluir o token de autenticação no cabeçalho de todas as requisições, no formato Bearer [seu-token];
- Utilize os endpoints da API conforme necessário, lembrando de seguir as boas práticas de segurança e respeitar os status codes fornecidos.

# Autor: Leonardo Cesar Thebald Coutinho

## Tecnologias

- Linguagem C# e .Net 7
- Entity Framework Core 7
- Swagger (Open API)
- SignalR
- JWT Web Token
- Banco de dados SqLite

## Cenário

A ideia do projeto é simular um sistema de um blog onde os usuários possam se cadastrar, autenticar (login) e fazer postagens no blog.
Este blog pode ser acessado por qualquer cliente que não seja cadastrado no blog, desta forma ele poderá observar as postagens enviadas pelos usuários de forma simultânea (Real-time).
O Usuário logado pode gerenciar suas postagens criando novas, atualizando existentes dele e excluir as que ele criou.
Esta aplicação foi criada em uma Solution com dois projetos, sendo um projeto web API Rest para prover serviços do gerenciamento dos posts feito pelo usuário, e servir como hub para o websocket atualizar as notificações de postagens para o cliente. 
O outro projeto é um Console Application para servir de client-side para ter as mensagens sendo impressas no "Blog". Foram utilizadas tecnologias atuais do .Net e boas práticas como SOLID, Clean Code, Autenticação JWT para a API e WebSocket para fazer a comunicação server x client.

## Uso

- Após executar o projeto em modo desenvolvimento (localhost), irá abrir o browser com o Swagger expondo os endpoints;
- O visitante pode se tornar usuário se registrando no endpoint /register;
- Após virar usuário do blog o usuário pode fazer autenticação no endpoint /login;
- Ao se autenticar o usuário recebe um token como retorno da API;
- Com esse token em mãos, faço a autenticação na API utilizando o seguinte o comando 'Bearer' seguido do token recebido pela API ex: Bearer {xxxx-token-xxxxxx};
- Após a API autorizar o usuário ele está apto para gerenciar suas postagens utilizando os endpoins (/posts);
- Ao enviar um comentário no endpoint (/posts/[POST]) a tela dos clientes serão atualizadas em tempo real.

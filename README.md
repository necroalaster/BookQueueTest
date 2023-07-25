# BookQueueTest

O projeto foi desenvolvido no .Net Core 3.1.
Foi utilizado o banco de dados SQL Server com EntityFramework e Migrations para versionamento de banco.
O nome do banco é BookQueue.

Todos os Endpoints foram implementados com exceção do PATCH, nunca havia trabalhado ou desenvolvido este, tive várias dificuldades e para não deixar de entregar não o implementei.

Adicionei um Swagger para facilitar o teste do projeto, mesmo que não tenha sido requisitado.

Coisas que faltaram na implementação:

- Log
- Validação de entradas, apenas POST tem alguma
- Tratamento de excessões no âmbito geral.
- Testes dos controllers.
- Docker (nunca trabalhei)
- Upload de imagens de capa
- Sem linguagens funcionais (nunca trabalhei com alguma)

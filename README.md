Instruções para teste:
1. Abrir a solution com Visual Studio 2019;
2. Alterar o projeto de inicialização para o docker-compose;
3. Executar o projeto (F5);

O arquivo de banco de dados do MSSql está configurado para ser armazenado no diretório: C:\dados\volumes\sqlserver. Caso necessário, alterar essa configuração no arquivo docker-compose.yml;
A api será executada na porta 8080, conforme configurado no Dockerfile. Em caso de conflito, altere a configuração no Dockerfile e no front;

Estrutura do projeto:
```
DevBase
  |-- DevBase.Api
  |-- DevBase.Domain
  |-- DevBase.Infra.Data
  |-- DevBase.Services
  |-- DevBase.Services.Util
  |-- Devbase.Util
  |-- UnitTest 
     |-- DevBase.Api.Test
     |-- DevBase.Services.Test 
```
	 
O projeto foi estruturado para ser inteligível, evolutivo, testável e escalável;
A camada de API apenas expõe os endpoints da aplicação e realiza tratamento da entrada de dados;
A camada de Domain possui a conexão com o mundo real, nela são definidas as entidades do sistema. Nesse modelo foi utilizado um domínio pobre, portanto essa camada não deve executar regras de negócio;
A camada de Services realiza a interação com a infra-estrutura e acesso a dados, coordena as tarefas e realiza chamadas as regras de negócio;
Services Utils possui utilidades relacionadas a camada de serviços;
Util possui utilidades gerais do sistema;
UnitTest: Testes unitários do sistema.

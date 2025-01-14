# Emissor Digital de Ordem de Serviço

Neste repositório, contém o código fonte do projeto Emissor digital de Ordem de Serviço

# Visão geral

Este projeto tem como objetivo auxiliar pessoas a criarem orçamentos/ordens de serviço de forma fácil e prática, sendo via web ou aplicativo mobile

Site: [Emisor Digital de O.S.](https://osdigital.vintech.dev.br/)

Ele foi desenvolvido utilizando as linguagem de programação e ferramentas abaixo listadas:
|Descrição|Usado em|
|-|-|
|.NET|Backend
|Xamarin Forms|FrontEnd
|JavaScript com TypeScript|FrontEnd
|MongoDB|Persistencia de dados
|Firebase|Autenticação, Push Notification e Remote Config

## Debug

Para fazer a execução do mesmo localmente, algumas configurações precisam ser feitas anteriormente. Segue:

### Solicitar dados sensiveis

Algumas configurações, exigem dados sensiveis que não estão no controle de versão. Sendo assim, solicite-as para mim. Elas são:
|Descrição|Usado em|
|-|-|
|Usuário GitHub|nuget.config
|Token GitHub|nuget.config

### Declarar váriaveis para nuget.config (.NET)

Algumas das bibliotecas utilizadas, são privadas e precisam de autenticação para seguir.

Sendo assim, crie as váriaveis de ambiente em sua máquina local denominadas `GITHUB_USERNAME` e `GITHUB_TOKEN`

**OBS: Os valores devem ser solicitados a mim**

### Alterar valores no appsettings.json (.NET)

| Nome                        | Descrição                              | Arquivo                                                                  | Observação                    |
| --------------------------- | -------------------------------------- | ------------------------------------------------------------------------ | ----------------------------- |
| <CONNECTION_STRING_MONGODB> | String de conexão com o MongoDb        | [src/api/Api/appsettings.Development.json](appsettings.Development.json) | Ambiente de Desenvolvimento   |
| <CONNECTION_STRING_MONGODB> | String de conexão com o MongoDb        | [src/api/Api/appsettings.Production.json](appsettings.Production.json)   | Ambiente de Produção          |
| <FIREBASE_PRIVATE_KEY_ID>   | Credenciais Firebase                   | [src/api/Api/appsettings.json](appsettings.json)                         | Solicitar a mim se necessário |
| <FIREBASE_CLIENT_EMAIL>     | Credenciais Firebase                   | [src/api/Api/appsettings.json](appsettings.json)                         | Solicitar a mim se necessário |
| <FIREBASE_CLIENT_ID>        | Credenciais Firebase                   | [src/api/Api/appsettings.json](appsettings.json)                         | Solicitar a mim se necessário |
| <URL_DOWNLOAD_APK>          | Link para download do .apk (android)   | [src/api/Api/appsettings.json](appsettings.json)                         | ----------------------------- |
| <URL_USER_PICTURE>          | URL base para buscar a foto do usuario | [src/api/Api/appsettings.json](appsettings.json)                         | ----------------------------- |
| <URL_ORDER_PDF>             | URL base para buscar o pdf da O.S.     | [src/api/Api/appsettings.json](appsettings.json)                         | ----------------------------- |
| <URL_TERM_OF_SERVICE>       | Link para os termos de serviço         | [src/api/Api/appsettings.json](appsettings.json)                         | ----------------------------- |

**OBS: Os valores referentes ao Firebase, podem ser obtidas através de outro projeto, não necessáriamente seja o meu (no arquivo, tem os placeolders de como devem ser preenchidas)**

### Alterar valores no contants.ts

Caso optar por criar um novo projeto do Firebase, precisa reconfigurar as credencias no arquivo [src/web/src/constants.ts](constants.ts)

**OBS: Caso decida usar o já existente, solicite a mim a apiKey (ensinarei como configura-la abaixo)**

### Criar arquivo .env com as váriaveis de ambiente projeto web

Crie um arquivo novo em [src/web/src/](src/web/src/) denominado **.env** seguindo o exemplo abaixo:

```
PUBLIC_URL=
REACT_APP_API_URL=
REACT_APP_FIREBASE_API_KEY=
```

| Nome                       | Descrição                |
| -------------------------- | ------------------------ |
| PUBLIC_URL                 | URL do projeto publicado |
| REACT_APP_API_URL          | URL da api               |
| REACT_APP_FIREBASE_API_KEY | ApiKey do Firebase       |

## Compilar projeto usando Docker

Antes de fazer a compilação do projeto e posteriormente deploy, antes algumas configurações precisam ser feitas

### Criar arquivo .env com as váriaveis de ambiente

Crie um arquivo novo em [/](/) denominado **.env** seguindo o exemplo abaixo:

```
GITHUB_USERNAME=
GITHUB_TOKEN=
CONNECTION_STRING_MONGODB=
FIREBASE_PRIVATE_KEY_ID=
FIREBASE_CLIENT_EMAIL=
FIREBASE_CLIENT_ID=
URL_DOWNLOAD_APK=
URL_BASE_GET_USER_PICTURE=
URL_BASE_GET_PDF=
URL_TERM_OF_SERVICE=
```

| Nome                      | Descrição                                                    |
| ------------------------- | ------------------------------------------------------------ |
| GITHUB_USERNAME           |                                                              |
| GITHUB_TOKEN              |                                                              |
| CONNECTION_STRING_MONGODB | String de conexão com o MongoDb                              |
| FIREBASE_PRIVATE_KEY_ID   | Chave privada id do Firebase (Solicitar a mim se necessário) |
| FIREBASE_CLIENT_EMAIL     | Email do client Firebase (Solicitar a mim se necessário)     |
| FIREBASE_CLIENT_ID        | ClientId do Firebase (Solicitar a mim se necessário)         |
| URL_DOWNLOAD_APK          | Link para download do .apk (android)                         |
| URL_BASE_GET_USER_PICTURE | URL base para buscar a foto do usuario                       |
| URL_BASE_GET_PDF          | URL base para buscar o pdf da O.S.                           |
| URL_TERM_OF_SERVICE       | Link para os termos de serviço                               |

### Compilar projeto e subir os containers

Execute o comando a seguir na pasta raíz do repositorio:

```bash
docker compose up --build
```

### Aplicativo Móvel

Foi desenvolvido também um aplicativo usando Xamarin Forms. Para o funcionamento do mesmo, é necessário alterar o arquivo [src/app/OrderService.Android/google-services.json](google-services.json).

| Nome               | Descrição                                                      |
| ------------------ | -------------------------------------------------------------- |
| <CERTIFICATE_HASH> | Hash do certificado Firebase ((Solicitar a mim se necessário)) |
| <CURRENT_API_KEY>  | ApiKey Firebase (Solicitar a mim se necessário)                |

**OBS: Os valores referentes ao Firebase, podem ser obtidas através de outro projeto, não necessáriamente seja o meu (no arquivo, tem os placeolders de como devem ser preenchidas)**

## NSwag Studio

Usado para criar os client's de api, usando nos projetos front-end (web e mobile)

Mais informações sobre: [Nswag](https://github.com/RicoSuter/NSwag)

## Considerações Finais

Este passo a passo tem como objetivo auxiliar na configurações do projeto. Caso enfrente alguma dificuldade, estou a disposição.

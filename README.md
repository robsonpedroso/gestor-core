# Gestor

Projeto modelo para novos projetos

## Introdu��o

Essas instru��es fornecer�o uma c�pia do projeto em execu��o na sua m�quina local para fins de desenvolvimento e teste.
Consulte implanta��o para obter notas sobre como implantar o projeto em um sistema ativo.

### Prerequisitos

O que voc� precisa para baixar, rodar e disponibilizar.

* Visual Studio com o dotnet core 3.1 instalado
* SQL Server
* Criar o banco de dados local ex.: **DBGestorCore**
* Alterar a connection string para o banco - caso haja necessidade

### Instala��o

Ap�s a execu��o do pre requisitos, segue um passo a passo de como rodar localmente.

Clonar o reposit�rio

```
git clone git@github.com:robsonpedroso/gestor-core.git
```

Abra a solu��o com o Visual Studio e compile.
Sete o Projeto default como a API e execute (F5).


Chame a URL abaixo pelo navegador para verificar se esta ok.

```
http://localhost:4201/api/v1/ping
```

Se ele retornar ok (conforme exemplo abaixo)

```
{
    "content": "pong",
    "status": "OK",
    "messages": []
}
```
## Diret�rios

1. `_docs` - Contem o arquivo Readme.md e caso necess�rio outras documenta��es para suporte a execu��o e manuten��o da aplica��o.
2. `api` - Projeto da API
3. `core` - Estrutura padr�o do DDD contendo os projetos `Application`, `Domain` e `Infra`
4. `tools` - Ferramentas para ajudar no desenvolvimento, no caso foi usado algumas extensions para facilitar a implementa��o da API e dos retornos.

### Padr�o de Tecnologia utilizado

Utilizamos o padr�o do DDD mais simplificado para trabalhar com os projetos.

Os contratos n�o s�o utilizados no projeto de `Application` devido ser 1 por 1, caso haja algum caso que seja necess�rio fazer uma diferencia��o de aplica��o, ai sim criamos a interface para essa diferencia��o

J� no `Domain` e no `Infra` utilizamos normalmente os contratos (interfaces), pois sabemos que muitas das vezes precisamos modificar os servi�os, seja por causa de alguma integra��o ou ferramenta utilizada que foi necess�rio mudar o padr�o de conex�es e chamadas entre elas.

O mapeamento das interfaces s�o feito automaticamente com reflection para evitar o trabalho e poss�veis erros de esquecimento.
Esse reflection se encontra numa extension no projeto `tools/WebApi` e � chamado no startup da aplica��o.
Caso seja necess�rio passar alguma interface externa ou manualmente mesmo, esse metodo aceita um action ficando mais f�cil utilizar.

Exemplo da utiliza��o se encontra no Statup (veja abaixo):
```
	services.AddServiceMappingsFromAssemblies<BaseApplication, IBaseService, InfraServices>(srv =>
    {
        srv.AddSingleton<Config>();
    });
```

O retorno da API foi modificado atrav�s de um wrapper e filtro no startup da API.
O padr�o de convers�o do json � `SnakeCaseNamingStrategy`.
Para facilitar a visualiza��o do json de resultado utilizei o [Json Viewer Online](http://jsonviewer.stack.hu/)

## Execu��o dos testes

N�o foi gerado

## Publica��o

N�o foi gerado

## Versionamento

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

```
Given a version number MAJOR.MINOR.PATCH, increment the:

MAJOR version when you make incompatible API changes,
MINOR version when you add functionality in a backwards compatible manner, and
PATCH version when you make backwards compatible bug fixes.
Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.
```

## Autores

* **Robson Pedroso** - *Projeto inicial* - [RobsonPedroso](https://github.com/robsonpedroso)

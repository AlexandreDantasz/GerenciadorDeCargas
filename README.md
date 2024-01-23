# Sistema Gerenciador de Cargas :file_folder:
<p>O sistema usa um banco de dados para incluir, remover, buscar, listar informações de cargas e transferir essas informações para um arquivo de Excel. Criado com o intuito de facilitar a documentação e o gerenciamento de cargas de um funcionário da Gollog.</p>

## Tecnologias Utilizadas :pushpin:
- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/): Linguagem de programação<br>
- [.NET](https://dotnet.microsoft.com/en-us/download/dotnet)<br>
- [SQLite 3](https://www.sqlite.org/download.html)<br>
- [Microsoft Excel](https://www.microsoft.com/pt-br/microsoft-365/microsoft-office)

## Dependências e Versões :calendar:
- **.NET | Versão: 8.0**
- **SQLite 3 | Versão: 3.42.0**
- **System.Data.SQLite | Versão: 1.0.118**
- **Microsoft.Office.Interop.Excel | Versão: 15.0.4795.1001**

## Como rodar o projeto :white_check_mark:
1. Instale o **.NET 8.0** através desse [link](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Instale o **SQLite 3** através desse [link](https://www.sqlite.org/download.html)
3. Instale o **Microsoft Excel** através desse [link](https://www.microsoft.com/pt-br/microsoft-365/microsoft-office)
4. Faça o clone desse [repositório](https://github.com/AlexandreDantasz/Gollog) na sua máquina:
    - Crie uma pasta no seu computador para esse programa.
    - Abra o `git bash` ou `terminal` dentro dessa pasta.
    - Digite `git clone https://github.com/AlexandreDantasz/Gollog` e pressione `enter`
5. Adicione as dependências necessárias, dentro dessa pasta criada.
    - **System.Data.SQLite:** `dotnet add package System.Data.SQLite --version 1.0.118`
    - **Microsoft.Office.Interop.Excel:** Caso o programa aponte erro pela falta dessa dependência, tente `dotnet add package Microsoft.Office.Interop.Excel --version 15.0.4795.1001`
6. Execute o programa
    - Entre na pasta localizaCargas com `cd localizaCargas`
    - Digite `dotnet run` e pressione `enter`

## Configurando o banco de dados :hammer:

### Localização :round_pushpin:
O banco de dados está localizado na pasta **localizaCargas**, dentro das pastas **bin/Debug/net8.0** e **bin/Release/net8.0**, além disso, o banco também está presente em **bin/Debug/net8.0/win-x64** e **bin/Release/net8.0/publish**

### Lista de comandos (terminal) :page_with_curl:
```
sqlite3
.open Gollog.db
CREATE TABLE IF NOT EXISTS Cargas(codRastreio TEXT PRIMARY KEY NOT NULL, nomeCliente TEXT, rua TEXT, bairro TEXT, volPeso TEXT, descricao TEXT, desembarque TEXT);
```

## Problemas encontrados :warning:
- Ao adicionar a dependência **Microsoft.Office.Interop.Excel** encontrei erros que indicavam a falha de busca das informações necessárias para incluir a dependência, em razão disso, adicionei a referência COM **Microsoft Excel 16.0 Object Librabry** versão **1.9** no [Visual Studio](https://visualstudio.microsoft.com). Para mais informações acesse esse [link](https://learn.microsoft.com/en-us/answers/questions/1496033/microsoft-office-interop-excel-reference-cannot-be) para informações no fórum da Microsoft (EN-US)
- Ao publicar o projeto como [single-file](https://learn.microsoft.com/en-us/dotnet/core/deploying/single-file/overview?tabs=cli) ocorria um erro com a dependência **System.Data.SQLite** uma vez que a função `Path.GetDirectoryName()`, que estava sendo usada dentro da dependência, retornava uma string nula em versões iguais ou mais recentes a .NET 5, para resolver isso, decidi abdicar da publicação single-file. Um trade-off para a garantia do funcionamento do projeto.
# SistemaEleicao

Sistema de eleição em console desenvolvido em **.NET 8** (C#) como exercício da disciplina de **Garantia da Qualidade de Software**, aplicando **Test-Driven Development (TDD)** com **xUnit** e histórico de commits no padrão **Conventional Commits**.

## Funcionalidades

1. **Cadastro de candidatos** — nome e número de urna, com validação que impede números duplicados;
2. **Registro de eleitores** — número do título/CPF (simulado) para controle de quem já votou;
3. **Votação** — o eleitor vota digitando o número do candidato escolhido;
   - `0` registra **voto em branco**;
   - número de candidato **inexistente** registra **voto nulo**;
   - eleitor **não registrado** ou que **já votou** é bloqueado com mensagem de erro;
4. **Apuração** — relatório final com votos por candidato (ordenados), votos nulos, brancos, total geral e o candidato vencedor (com detecção de empate).

## Estrutura da Solução

```
SistemaEleicao/
├── SistemaEleicao.sln
├── SistemaEleicao.Console/        # Aplicação de console
│   ├── Program.cs                 # Menu interativo
│   └── Core/                      # Regras de negócio
│       ├── Candidato.cs           # Entidade candidato (com validações)
│       ├── EleicaoManager.cs      # Gerência de candidatos, eleitores, votos e apuração
│       └── ResultadoEleicao.cs    # Resultado consolidado + geração do relatório
└── SistemaEleicao.Tests/          # Testes unitários (xUnit)
    ├── CandidatoTests.cs          # Regras de cadastro de candidatos
    ├── VotacaoTests.cs            # Eleitores, voto duplicado, nulo e branco
    └── ApuracaoTests.cs           # Totais, vencedor, empate e relatório
```

## Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior

Verifique a instalação:

```bash
dotnet --version
```

## Como Executar

Clone o repositório e entre na pasta:

```bash
git clone https://github.com/PedroHenriqueM2005/SistemaEleicao.git
cd SistemaEleicao
```

Restaure as dependências e execute a aplicação de console:

```bash
dotnet restore
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

O menu interativo será exibido:

```
MENU PRINCIPAL
  [1] Cadastrar candidato
  [2] Registrar eleitor
  [3] Votar
  [4] Apuração de votos
  [0] Sair
```

Fluxo sugerido de uso: cadastre os candidatos, registre os eleitores, realize os votos e, por fim, exiba a apuração.

## Como Executar os Testes

```bash
dotnet test
```

Saída esperada:

```
Passed!  - Failed: 0, Passed: 28, Skipped: 0, Total: 28
```

## Padrão de Commits

O histórico segue o [Conventional Commits](https://www.conventionalcommits.org/pt-br/), com ciclos de TDD (teste antes da implementação):

| Tipo       | Uso                                              |
|------------|--------------------------------------------------|
| `chore`    | Estrutura inicial da solução e dos projetos      |
| `test`     | Testes unitários (fase vermelha do TDD)          |
| `feat`     | Implementação das regras de negócio (fase verde) |
| `refactor` | Reorganização de namespaces e legibilidade       |
| `docs`     | Documentação (este README)                       |

Visualize o histórico com:

```bash
git log --oneline
```

## Licença

Projeto acadêmico de uso educacional.

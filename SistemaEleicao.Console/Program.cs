using SistemaEleicao.Core;

const string Titulo = "SISTEMA DE ELEIÇÃO";

var eleicao = new EleicaoManager();
var executando = true;

Console.Clear();
EscreverCabecalho();

while (executando)
{
    MostrarMenu();
    var opcao = Console.ReadLine()?.Trim();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarCandidato();
                break;
            case "2":
                RegistrarEleitor();
                break;
            case "3":
                RegistrarVoto();
                break;
            case "4":
                ExibirApuracao();
                break;
            case "0":
                executando = false;
                Console.WriteLine("\nEncerrando o sistema. Obrigado por utilizar!");
                break;
            default:
                Console.WriteLine("\n[!] Opção inválida. Tente novamente.");
                break;
        }
    }
    catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
    {
        Console.WriteLine($"\n[ERRO] {ex.Message}");
    }

    if (executando)
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
        EscreverCabecalho();
    }
}

void EscreverCabecalho()
{
    Console.WriteLine("==================================================");
    Console.WriteLine(Titulo.PadLeft(25 + Titulo.Length / 2));
    Console.WriteLine("==================================================");
}

void MostrarMenu()
{
    Console.WriteLine();
    Console.WriteLine("MENU PRINCIPAL");
    Console.WriteLine("  [1] Cadastrar candidato");
    Console.WriteLine("  [2] Registrar eleitor");
    Console.WriteLine("  [3] Votar");
    Console.WriteLine("  [4] Apuração de votos");
    Console.WriteLine("  [0] Sair");
    Console.Write("\nEscolha uma opção: ");
}

void CadastrarCandidato()
{
    Console.WriteLine("\n--- CADASTRO DE CANDIDATO ---");
    Console.Write("Nome do candidato: ");
    var nome = Console.ReadLine() ?? string.Empty;
    Console.Write("Número de urna: ");

    if (!int.TryParse(Console.ReadLine(), out var numero))
    {
        throw new ArgumentException("O número do candidato deve ser um valor inteiro.");
    }

    eleicao.CadastrarCandidato(nome, numero);
    Console.WriteLine($"\n[OK] Candidato '{nome}' cadastrado com o número {numero}.");
}

void RegistrarEleitor()
{
    Console.WriteLine("\n--- REGISTRO DE ELEITOR ---");
    Console.Write("Número do título/CPF do eleitor: ");
    var titulo = Console.ReadLine() ?? string.Empty;

    eleicao.RegistrarEleitor(titulo);
    Console.WriteLine($"\n[OK] Eleitor {titulo.Trim()} registrado e apto a votar.");
}

void RegistrarVoto()
{
    Console.WriteLine("\n--- VOTAÇÃO ---");

    var candidatos = eleicao.ListarCandidatos();
    if (candidatos.Count == 0)
    {
        Console.WriteLine("[!] Nenhum candidato cadastrado. Cadastre candidatos antes de votar.");
        return;
    }

    Console.WriteLine("Candidatos disponíveis:");
    foreach (var candidato in candidatos)
    {
        Console.WriteLine($"  {candidato.Numero,3} - {candidato.Nome}");
    }

    Console.Write("\nTítulo/CPF do eleitor: ");
    var titulo = Console.ReadLine() ?? string.Empty;

    Console.Write("Número do candidato (0 = branco, outro nº inexistente = nulo): ");
    if (!int.TryParse(Console.ReadLine(), out var numero))
    {
        throw new ArgumentException("O número digitado deve ser um valor inteiro.");
    }

    eleicao.Votar(titulo, numero);

    var candidatoVotado = candidatos.FirstOrDefault(c => c.Numero == numero);
    var descricao = numero == EleicaoManager.NumeroVotoBranco
        ? "em branco"
        : candidatoVotado is not null
            ? $"no candidato {candidatoVotado.Nome}"
            : "nulo (candidato inexistente)";
    Console.WriteLine($"\n[OK] Voto {descricao} computado com sucesso!");
}

void ExibirApuracao()
{
    Console.WriteLine();
    Console.Write(eleicao.Apurar().GerarRelatorio());
}

namespace SistemaEleicao;

/// <summary>
/// Classe de gerência da eleição: candidatos, eleitores, votos e apuração.
/// </summary>
public class EleicaoManager
{
    /// <summary>Número de urna que representa o voto em branco.</summary>
    public const int NumeroVotoBranco = 0;

    private readonly Dictionary<int, Candidato> _candidatos = new();
    private readonly Dictionary<int, int> _votosPorCandidato = new();
    private readonly HashSet<string> _eleitoresRegistrados = new();
    private readonly HashSet<string> _eleitoresQueVotaram = new();

    private int _votosNulos;
    private int _votosBrancos;

    /// <summary>Total de votos nulos computados até o momento.</summary>
    public int TotalVotosNulos => _votosNulos;

    /// <summary>Total de votos em branco computados até o momento.</summary>
    public int TotalVotosBrancos => _votosBrancos;

    /// <summary>Cadastra um candidato na eleição.</summary>
    /// <exception cref="InvalidOperationException">Quando já existe candidato com o mesmo número.</exception>
    /// <exception cref="ArgumentException">Quando os dados do candidato são inválidos.</exception>
    public void CadastrarCandidato(string nome, int numero)
    {
        var candidato = new Candidato(nome, numero);

        if (_candidatos.ContainsKey(candidato.Numero))
        {
            throw new InvalidOperationException(
                $"Já existe um candidato cadastrado com o número {candidato.Numero}.");
        }

        _candidatos.Add(candidato.Numero, candidato);
        _votosPorCandidato[candidato.Numero] = 0;
    }

    /// <summary>Lista os candidatos cadastrados.</summary>
    public IReadOnlyList<Candidato> ListarCandidatos() => _candidatos.Values.ToList();

    /// <summary>Registra um eleitor (número do título/CPF simulado) apto a votar.</summary>
    /// <exception cref="ArgumentException">Quando o título é vazio.</exception>
    /// <exception cref="InvalidOperationException">Quando o eleitor já está registrado.</exception>
    public void RegistrarEleitor(string tituloEleitor)
    {
        ValidarTituloEleitor(tituloEleitor);

        if (!_eleitoresRegistrados.Add(tituloEleitor.Trim()))
        {
            throw new InvalidOperationException(
                $"O eleitor {tituloEleitor.Trim()} já está registrado.");
        }
    }

    /// <summary>Indica se o eleitor informado já exerceu o seu voto.</summary>
    public bool EleitorJaVotou(string tituloEleitor)
    {
        ValidarTituloEleitor(tituloEleitor);
        return _eleitoresQueVotaram.Contains(tituloEleitor.Trim());
    }

    /// <summary>
    /// Registra o voto de um eleitor. Número 0 = voto em branco;
    /// número de candidato inexistente = voto nulo.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Quando o eleitor não está registrado ou já votou.
    /// </exception>
    public void Votar(string tituloEleitor, int numeroCandidato)
    {
        ValidarTituloEleitor(tituloEleitor);
        var titulo = tituloEleitor.Trim();

        if (!_eleitoresRegistrados.Contains(titulo))
        {
            throw new InvalidOperationException(
                $"O eleitor {titulo} não está registrado para votar.");
        }

        if (!_eleitoresQueVotaram.Add(titulo))
        {
            throw new InvalidOperationException(
                $"O eleitor {titulo} já votou nesta eleição.");
        }

        if (numeroCandidato == NumeroVotoBranco)
        {
            _votosBrancos++;
        }
        else if (_votosPorCandidato.TryGetValue(numeroCandidato, out var total))
        {
            _votosPorCandidato[numeroCandidato] = total + 1;
        }
        else
        {
            _votosNulos++;
        }
    }

    /// <summary>Obtém o total de votos de um candidato cadastrado.</summary>
    /// <exception cref="ArgumentException">Quando o candidato não existe.</exception>
    public int ObterTotalVotosCandidato(int numeroCandidato)
    {
        if (!_votosPorCandidato.TryGetValue(numeroCandidato, out var total))
        {
            throw new ArgumentException(
                $"Não existe candidato cadastrado com o número {numeroCandidato}.",
                nameof(numeroCandidato));
        }

        return total;
    }

    private static void ValidarTituloEleitor(string tituloEleitor)
    {
        if (string.IsNullOrWhiteSpace(tituloEleitor))
        {
            throw new ArgumentException(
                "O número do título do eleitor é obrigatório.", nameof(tituloEleitor));
        }
    }
}

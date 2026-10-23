namespace SistemaEleicao;

/// <summary>
/// Classe de gerência da eleição: candidatos, eleitores, votos e apuração.
/// </summary>
public class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = new();

    /// <summary>Cadastra um candidato na eleição.</summary>
    /// <exception cref="InvalidOperationException">Quando já existe candidato com o mesmo número.</exception>
    /// <exception cref="ArgumentException">Quando os dados do candidato são inválidos.</exception>
    public void CadastrarCandidato(string nome, int numero)
    {
        if (_candidatos.ContainsKey(numero))
        {
            throw new InvalidOperationException(
                $"Já existe um candidato cadastrado com o número {numero}.");
        }

        var candidato = new Candidato(nome, numero);
        _candidatos.Add(candidato.Numero, candidato);
    }

    /// <summary>Lista os candidatos cadastrados.</summary>
    public IReadOnlyList<Candidato> ListarCandidatos() => _candidatos.Values.ToList();

    /// <summary>Registra um eleitor (número do título/CPF simulado) apto a votar.</summary>
    public void RegistrarEleitor(string tituloEleitor) => throw new NotImplementedException();

    /// <summary>Indica se o eleitor informado já exerceu o seu voto.</summary>
    public bool EleitorJaVotou(string tituloEleitor) => throw new NotImplementedException();

    /// <summary>
    /// Registra o voto de um eleitor. Número 0 = voto em branco;
    /// número de candidato inexistente = voto nulo.
    /// </summary>
    public void Votar(string tituloEleitor, int numeroCandidato) => throw new NotImplementedException();

    /// <summary>Total de votos nulos computados até o momento.</summary>
    public int TotalVotosNulos => throw new NotImplementedException();

    /// <summary>Total de votos em branco computados até o momento.</summary>
    public int TotalVotosBrancos => throw new NotImplementedException();

    /// <summary>Obtém o total de votos de um candidato cadastrado.</summary>
    public int ObterTotalVotosCandidato(int numeroCandidato) => throw new NotImplementedException();
}

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
}

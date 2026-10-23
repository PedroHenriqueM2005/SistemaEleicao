namespace SistemaEleicao;

/// <summary>
/// Classe de gerência da eleição: candidatos, eleitores, votos e apuração.
/// </summary>
public class EleicaoManager
{
    /// <summary>Cadastra um candidato na eleição.</summary>
    /// <exception cref="InvalidOperationException">Quando já existe candidato com o mesmo número.</exception>
    /// <exception cref="ArgumentException">Quando os dados do candidato são inválidos.</exception>
    public void CadastrarCandidato(string nome, int numero) => throw new NotImplementedException();

    /// <summary>Lista os candidatos cadastrados.</summary>
    public IReadOnlyList<Candidato> ListarCandidatos() => throw new NotImplementedException();
}

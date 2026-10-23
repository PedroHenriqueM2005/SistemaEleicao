namespace SistemaEleicao;

/// <summary>Total de votos recebidos por um candidato.</summary>
public record ResultadoCandidato(Candidato Candidato, int TotalVotos);

/// <summary>
/// Resultado consolidado da apuração da eleição.
/// </summary>
public class ResultadoEleicao
{
    public ResultadoEleicao(
        IReadOnlyList<ResultadoCandidato> votosPorCandidato,
        int votosNulos,
        int votosBrancos,
        Candidato? vencedor)
    {
        VotosPorCandidato = votosPorCandidato;
        VotosNulos = votosNulos;
        VotosBrancos = votosBrancos;
        Vencedor = vencedor;
    }

    /// <summary>Votos por candidato, ordenados do mais votado para o menos votado.</summary>
    public IReadOnlyList<ResultadoCandidato> VotosPorCandidato { get; }

    /// <summary>Total de votos nulos.</summary>
    public int VotosNulos { get; }

    /// <summary>Total de votos em branco.</summary>
    public int VotosBrancos { get; }

    /// <summary>Total geral de votos computados (candidatos + nulos + brancos).</summary>
    public int TotalVotos => VotosPorCandidato.Sum(v => v.TotalVotos) + VotosNulos + VotosBrancos;

    /// <summary>Candidato vencedor; nulo em caso de empate ou ausência de votos.</summary>
    public Candidato? Vencedor { get; }

    /// <summary>Gera o relatório textual da apuração.</summary>
    public string GerarRelatorio() => string.Empty;
}

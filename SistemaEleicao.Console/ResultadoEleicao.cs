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
    public string GerarRelatorio()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("================ RELATÓRIO FINAL DA ELEIÇÃO ================");
        sb.AppendLine();
        sb.AppendLine("Votos por candidato:");

        if (VotosPorCandidato.Count == 0)
        {
            sb.AppendLine("  Nenhum candidato cadastrado.");
        }

        foreach (var item in VotosPorCandidato)
        {
            sb.AppendLine($"  {item.Candidato.Nome,-25} (nº {item.Candidato.Numero,3}) : {item.TotalVotos} voto(s)");
        }

        sb.AppendLine();
        sb.AppendLine($"  Votos Nulos   : {VotosNulos}");
        sb.AppendLine($"  Votos Brancos : {VotosBrancos}");
        sb.AppendLine($"  Total de Votos: {TotalVotos}");
        sb.AppendLine();

        if (Vencedor is not null)
        {
            sb.AppendLine($"VENCEDOR: {Vencedor.Nome} (nº {Vencedor.Numero})");
        }
        else if (TotalVotos == 0)
        {
            sb.AppendLine("RESULTADO: nenhum voto computado até o momento.");
        }
        else
        {
            sb.AppendLine("RESULTADO: EMPATE técnico entre os candidatos mais votados.");
        }

        sb.AppendLine("============================================================");
        return sb.ToString();
    }
}

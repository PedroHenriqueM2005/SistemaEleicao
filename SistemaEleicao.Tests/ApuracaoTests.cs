namespace SistemaEleicao.Tests;

public class ApuracaoTests
{
    private static EleicaoManager CriarEleicaoVotada()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);
        manager.CadastrarCandidato("Bruno Costa", 20);

        manager.RegistrarEleitor("111.111.111-11");
        manager.RegistrarEleitor("222.222.222-22");
        manager.RegistrarEleitor("333.333.333-33");

        manager.Votar("111.111.111-11", 10);
        manager.Votar("222.222.222-22", 10);
        manager.Votar("333.333.333-33", 20);

        return manager;
    }

    [Fact]
    public void Apurar_ComVotos_RetornaTotaisPorCandidato()
    {
        var manager = CriarEleicaoVotada();

        var resultado = manager.Apurar();

        Assert.Equal(2, resultado.VotosPorCandidato.Single(v => v.Candidato.Numero == 10).TotalVotos);
        Assert.Equal(1, resultado.VotosPorCandidato.Single(v => v.Candidato.Numero == 20).TotalVotos);
        Assert.Equal(3, resultado.TotalVotos);
    }

    [Fact]
    public void Apurar_ComNulosEBrancos_IncluiNoResultado()
    {
        var manager = CriarEleicaoVotada();
        manager.RegistrarEleitor("444.444.444-44");
        manager.RegistrarEleitor("555.555.555-55");
        manager.Votar("444.444.444-44", 999);
        manager.Votar("555.555.555-55", 0);

        var resultado = manager.Apurar();

        Assert.Equal(1, resultado.VotosNulos);
        Assert.Equal(1, resultado.VotosBrancos);
        Assert.Equal(5, resultado.TotalVotos);
    }

    [Fact]
    public void Apurar_CandidatoMaisVotado_EhOVencedor()
    {
        var manager = CriarEleicaoVotada();

        var resultado = manager.Apurar();

        Assert.NotNull(resultado.Vencedor);
        Assert.Equal("Ana Silva", resultado.Vencedor!.Nome);
        Assert.Equal(10, resultado.Vencedor.Numero);
    }

    [Fact]
    public void Apurar_Empate_VencedorNulo()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);
        manager.CadastrarCandidato("Bruno Costa", 20);
        manager.RegistrarEleitor("111.111.111-11");
        manager.RegistrarEleitor("222.222.222-22");
        manager.Votar("111.111.111-11", 10);
        manager.Votar("222.222.222-22", 20);

        Assert.Null(manager.Apurar().Vencedor);
    }

    [Fact]
    public void Apurar_SemVotos_VencedorNulo()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);

        var resultado = manager.Apurar();

        Assert.Null(resultado.Vencedor);
        Assert.Equal(0, resultado.TotalVotos);
    }

    [Fact]
    public void Apurar_OrdenaCandidatosDoMaisVotadoParaOMenosVotado()
    {
        var manager = CriarEleicaoVotada();

        var resultado = manager.Apurar();

        Assert.Equal(10, resultado.VotosPorCandidato[0].Candidato.Numero);
        Assert.Equal(20, resultado.VotosPorCandidato[1].Candidato.Numero);
    }

    [Fact]
    public void GerarRelatorio_ComVencedor_ContemDadosDaEleicao()
    {
        var manager = CriarEleicaoVotada();

        var relatorio = manager.Apurar().GerarRelatorio();

        Assert.Contains("Ana Silva", relatorio);
        Assert.Contains("Bruno Costa", relatorio);
        Assert.Contains("Nulos", relatorio, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Brancos", relatorio, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("VENCEDOR", relatorio, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GerarRelatorio_Empate_IndicaEmpate()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);
        manager.CadastrarCandidato("Bruno Costa", 20);
        manager.RegistrarEleitor("111.111.111-11");
        manager.RegistrarEleitor("222.222.222-22");
        manager.Votar("111.111.111-11", 10);
        manager.Votar("222.222.222-22", 20);

        var relatorio = manager.Apurar().GerarRelatorio();

        Assert.Contains("EMPATE", relatorio, StringComparison.OrdinalIgnoreCase);
    }
}

using SistemaEleicao.Core;

namespace SistemaEleicao.Tests;

public class VotacaoTests
{
    private static EleicaoManager CriarEleicaoComCandidatos()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);
        manager.CadastrarCandidato("Bruno Costa", 20);
        return manager;
    }

    [Fact]
    public void RegistrarEleitor_Novo_RegistraComSucesso()
    {
        var manager = CriarEleicaoComCandidatos();

        manager.RegistrarEleitor("123.456.789-00");

        Assert.False(manager.EleitorJaVotou("123.456.789-00"));
    }

    [Fact]
    public void RegistrarEleitor_Duplicado_LancaInvalidOperationException()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("123.456.789-00");

        Assert.Throws<InvalidOperationException>(
            () => manager.RegistrarEleitor("123.456.789-00"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RegistrarEleitor_TituloInvalido_LancaArgumentException(string? titulo)
    {
        var manager = CriarEleicaoComCandidatos();

        Assert.Throws<ArgumentException>(() => manager.RegistrarEleitor(titulo!));
    }

    [Fact]
    public void Votar_EleitorNaoRegistrado_LancaInvalidOperationException()
    {
        var manager = CriarEleicaoComCandidatos();

        Assert.Throws<InvalidOperationException>(() => manager.Votar("999.999.999-99", 10));
    }

    [Fact]
    public void Votar_DuasVezes_LancaInvalidOperationException()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("123.456.789-00");
        manager.Votar("123.456.789-00", 10);

        var ex = Assert.Throws<InvalidOperationException>(
            () => manager.Votar("123.456.789-00", 20));

        Assert.Contains("já votou", ex.Message);
    }

    [Fact]
    public void Votar_CandidatoExistente_IncrementaVotoDoCandidato()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("111.111.111-11");
        manager.RegistrarEleitor("222.222.222-22");

        manager.Votar("111.111.111-11", 10);
        manager.Votar("222.222.222-22", 10);

        Assert.Equal(2, manager.ObterTotalVotosCandidato(10));
        Assert.Equal(0, manager.ObterTotalVotosCandidato(20));
    }

    [Fact]
    public void Votar_CandidatoInexistente_ContaComoVotoNulo()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("111.111.111-11");

        manager.Votar("111.111.111-11", 999);

        Assert.Equal(1, manager.TotalVotosNulos);
        Assert.Equal(0, manager.ObterTotalVotosCandidato(10));
    }

    [Fact]
    public void Votar_NumeroZero_ContaComoVotoBranco()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("111.111.111-11");

        manager.Votar("111.111.111-11", 0);

        Assert.Equal(1, manager.TotalVotosBrancos);
    }

    [Fact]
    public void Votar_VotoNulo_MarcaEleitorComoJaVotou()
    {
        var manager = CriarEleicaoComCandidatos();
        manager.RegistrarEleitor("111.111.111-11");
        manager.Votar("111.111.111-11", 999);

        Assert.True(manager.EleitorJaVotou("111.111.111-11"));
        Assert.Throws<InvalidOperationException>(() => manager.Votar("111.111.111-11", 10));
    }

    [Fact]
    public void ObterTotalVotosCandidato_CandidatoInexistente_LancaArgumentException()
    {
        var manager = CriarEleicaoComCandidatos();

        Assert.Throws<ArgumentException>(() => manager.ObterTotalVotosCandidato(999));
    }
}

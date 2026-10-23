using SistemaEleicao.Core;

namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_DadosValidos_RegistraCandidato()
    {
        var manager = new EleicaoManager();

        manager.CadastrarCandidato("Ana Silva", 10);

        var candidatos = manager.ListarCandidatos();
        Assert.Single(candidatos);
        Assert.Equal("Ana Silva", candidatos[0].Nome);
        Assert.Equal(10, candidatos[0].Numero);
    }

    [Fact]
    public void CadastrarCandidato_VariosCandidatos_ListaTodos()
    {
        var manager = new EleicaoManager();

        manager.CadastrarCandidato("Ana Silva", 10);
        manager.CadastrarCandidato("Bruno Costa", 20);
        manager.CadastrarCandidato("Carla Souza", 30);

        Assert.Equal(3, manager.ListarCandidatos().Count);
    }

    [Fact]
    public void CadastrarCandidato_NumeroDuplicado_LancaInvalidOperationException()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Ana Silva", 10);

        var ex = Assert.Throws<InvalidOperationException>(
            () => manager.CadastrarCandidato("Outro Candidato", 10));

        Assert.Contains("10", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CadastrarCandidato_NomeInvalido_LancaArgumentException(string? nome)
    {
        var manager = new EleicaoManager();

        Assert.Throws<ArgumentException>(() => manager.CadastrarCandidato(nome!, 10));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CadastrarCandidato_NumeroInvalido_LancaArgumentException(int numero)
    {
        var manager = new EleicaoManager();

        Assert.Throws<ArgumentException>(() => manager.CadastrarCandidato("Ana Silva", numero));
    }
}

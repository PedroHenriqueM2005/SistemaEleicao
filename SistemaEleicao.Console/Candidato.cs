namespace SistemaEleicao;

/// <summary>
/// Representa um candidato que concorre à eleição.
/// </summary>
public class Candidato
{
    public Candidato(string nome, int numero)
    {
        Nome = nome;
        Numero = numero;
    }

    /// <summary>Nome do candidato.</summary>
    public string Nome { get; }

    /// <summary>Número de urna do candidato (identificador único).</summary>
    public int Numero { get; }
}

namespace SistemaEleicao;

/// <summary>
/// Representa um candidato que concorre à eleição.
/// </summary>
public class Candidato
{
    public Candidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do candidato é obrigatório.", nameof(nome));
        }

        if (numero <= 0)
        {
            throw new ArgumentException("O número do candidato deve ser um inteiro positivo.", nameof(numero));
        }

        Nome = nome.Trim();
        Numero = numero;
    }

    /// <summary>Nome do candidato.</summary>
    public string Nome { get; }

    /// <summary>Número de urna do candidato (identificador único).</summary>
    public int Numero { get; }
}

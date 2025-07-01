namespace Questao5.Application.Queries.Responses;

public class SaldoContaCorrenteResult
{
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataConsulta { get; set; }
    public decimal Saldo { get; set; }
    

    public SaldoContaCorrenteResult(dynamic numero, dynamic nome, DateTime now, decimal saldo)
    {
        Numero = numero;
        Nome = nome;
        DataConsulta = now;
        Saldo = saldo;
    }
}

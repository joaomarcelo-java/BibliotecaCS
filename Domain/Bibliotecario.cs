using System.Data.Common;

namespace BibliotecaApp.Domain;

public class Bibliotecario : Pessoa
{
    public string CodigoFunc{get; protected set;}

    protected Bibliotecario(){}
    public Bibliotecario(string nome, string codigoFunc)
    {
        Nome = nome;
        CodigoFunc = codigoFunc;
    }
    public override void ExibirDados()
    {
        System.Console.WriteLine($"Nome: {Nome}, CodigoFunc {CodigoFunc}");
    }

}

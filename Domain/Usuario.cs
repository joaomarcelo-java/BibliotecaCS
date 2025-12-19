namespace BibliotecaApp.Domain;

public class Usuario : Pessoa
{
    public string Matricula{get; private set;}

    protected Usuario(){}
    public Usuario(string nome, string matricula)
    {
        Nome = nome;
        Matricula = matricula;
    }
    public override void ExibirDados()
    {
        System.Console.WriteLine($"Nome: {Nome}, Matricula {Matricula}");
    }
}
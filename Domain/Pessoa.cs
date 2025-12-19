namespace BibliotecaApp.Domain;

public abstract class Pessoa
{
    public int Id{ get;protected set;}
    public string Nome{ get; protected set;}

    protected Pessoa(){}
    public Pessoa(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public abstract void ExibirDados();
}

namespace BibliotecaApp.Domain;

public class Emprestimo
{
    public int Id { get; private set;}
    public DateTime DataLocacao{get; private set;}
    public DateTime DataDevolucao{get; private set;}
    public int UsuarioId{get; private set;}
    public int LivroId{get; private set;}

    public virtual Usuario Usuario{get; private set;}
    public virtual Livro Livro{get; private set;}

    private Emprestimo(){}
    public Emprestimo(int livroId, int usuarioId)
    {
        DataLocacao = DateTime.Today;
        DataDevolucao = DataLocacao.AddDays(14);

        UsuarioId = usuarioId;
        LivroId = livroId;
    }
}

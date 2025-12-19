namespace BibliotecaApp.Domain;

public class Livro
{
    public int Id{ get; private set;}
    public string Titulo{ get; private set;}
    public string Autor{ get; private set;}
    public string Link{ get; private set;}
    public StatusLivro Status{ get; private set;}

    private Livro(){}
    public Livro(string titulo, string autor, string link, StatusLivro status)
    {
        Titulo = titulo;
        Autor = autor;
        Link = link;
        Status = status;
    }

    public void MudarStatus(StatusLivro status)
    {
        this.Status = status;
    }
}

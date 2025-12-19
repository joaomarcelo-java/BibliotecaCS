namespace BibliotecaApp.Utils;

public class GerarCodigos
{
    private readonly Random _rand = new Random();
    public string GerarMatricula(string nome)
    {
        string iniciais = "";
        int num = _rand.Next(100000, 999999);
        string[] palavras = nome.Split(' ');
        foreach(var palavra in palavras)
        {
            iniciais += palavra[0];
        }
        return $"{iniciais.ToUpper()}{num}";
    }
    public string GerarCodFunc(string nome)
    {
        string iniciais = "";
        int num = _rand.Next(100, 999);
        string[] palavras = nome.Split(' ');
        foreach(var palavra in palavras)
        {
            iniciais += palavra[0];
        }
        return $"FCN{iniciais.ToUpper()}{num}";
    }
}
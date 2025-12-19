using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BibliotecaApp.Utils;

public class InputHelper
{
    public InputHelper() { }

    public int ReceberInt()
    {
        int num;
        while (true)
        {
            try
            {
                num = int.Parse(Console.ReadLine() ?? "0");
                break;
            }
            catch (Exception)
            {
                System.Console.WriteLine("[ ! ] -> Valor digitado incorreto. Utilize apenas números inteiros como indicado nos índices.");
            }
        }
        return num;
    }

    public int TransformarInt(string val)
    {
        string entrada = val;
        int resultado;
        while (true)
        {
            try
            {
                resultado = int.Parse(entrada);
                break;
            }
            catch (Exception)
            {
                System.Console.WriteLine("[ ! ] -> Valor digitado incorreto. Utilize apenas números inteiros.");
                entrada = System.Console.ReadLine() ?? "0";
            }
        }
        return resultado;
    }

    public string RetornaNomeInteiro()
    {
        while (true)
        {
            string entrada = System.Console.ReadLine() ?? "";
            string[] palavras = entrada.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (palavras.Length >= 2)
            {
                return Regex.Replace(entrada, @"[^a-zA-Z0-9 ]", "").ToUpper().Trim();
            }
            System.Console.WriteLine("[ ! ] -> Por favor, digite o nome completo (pelo menos nome e sobrenome):");
        }
    }

    public string ReceberStringSemEspeciais()
    {
        string texto;
        string padrao = @"[^a-zA-Z0-9]";
        while (true)
        {
            texto = System.Console.ReadLine()?.ToUpper() ?? "";
            if (string.IsNullOrWhiteSpace(texto))
            {
                System.Console.WriteLine("[ ! ] -> Campo de texto vazio. Digite novamente: ");
                continue;
            }
            break;
        }
        return Regex.Replace(texto, padrao, "");
    }
}
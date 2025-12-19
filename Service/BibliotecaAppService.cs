using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaApp.Data;
using BibliotecaApp.Domain;
using BibliotecaApp.Utils;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Service;

public class BibliotecaAppService
{
    private readonly AppDbContexto _db;
    private GerarCodigos gerarCodigos = new GerarCodigos();

    public BibliotecaAppService(AppDbContexto db)
    {
        _db = db;
    }

    public string RegistrarUsuario(string nome)
    {
        string matricula = gerarCodigos.GerarMatricula(nome);
        while (VerificaMatriculaExiste(matricula))
        {
            matricula = gerarCodigos.GerarMatricula(nome);
        }
        Usuario usuario = new Usuario(nome, matricula);
        _db.Usuarios.Add(usuario);
        _db.SaveChanges();
        return matricula;
    }

    public Usuario? RetornaUsuarioMatricula(string matricula)
    {
        return _db.Usuarios.FirstOrDefault(u => u.Matricula == matricula);
    }

    public bool VerificaMatriculaExiste(string matricula)
    {
        return _db.Usuarios.Any(u => u.Matricula == matricula);
    }

    public string RegistrarBibliotecario(string senhaAdmin, string nome)
    {
        string CodigoFuncionario;
        if (senhaAdmin == "123456")
        {
            CodigoFuncionario = gerarCodigos.GerarCodFunc(nome);
            while (VerificaCodigoFuncExiste(CodigoFuncionario))
            {
                CodigoFuncionario = gerarCodigos.GerarCodFunc(nome);
            }
            Bibliotecario bibliotecario = new Bibliotecario(nome, CodigoFuncionario);
            _db.Bibliotecarios.Add(bibliotecario);
            _db.SaveChanges();
            return CodigoFuncionario;
        }
        else
        {
            return null;
        }
    }

    public Bibliotecario? RetornaBibliotecarioCodigo(string codigoFunc)
    {
        return _db.Bibliotecarios.FirstOrDefault(b => b.CodigoFunc == codigoFunc);
    }

    public bool VerificaCodigoFuncExiste(string codigoFunc)
    {
        return _db.Bibliotecarios.Any(b => b.CodigoFunc == codigoFunc);
    }

    public List<Emprestimo> RetornaEmprestimos(int usuarioId)
    {
        return _db.Emprestimos
        .Include(e => e.Livro)
        .Where(e => e.UsuarioId == usuarioId)
        .ToList();
    }

    public string CriarEmprestimo(int livroId, int usuarioId)
    {
        if (VerificaDisponibilidadeLivro(livroId))
        {
            if (RetornaQuantidadeEmprestimosUsuario(usuarioId) < 3)
            {
                Emprestimo emprestimo = new Emprestimo(livroId, usuarioId);
                _db.Emprestimos.Add(emprestimo);
                var livro = _db.Livros.FirstOrDefault(l => l.Id == livroId);
                if (livro != null)
                {
                    livro.MudarStatus(StatusLivro.Indisponivel);
                    _db.SaveChanges();
                }
            }
            else
            {
                return "[ ! ] -> Você atingiu o número máximo de emprestimos!";
            }
        }
        else
        {
            return "[ ! ] -> Livro já alugado ou inexistente!";
        }
        return "[ ! ] -> Emprestimo realizado com sucesso. Consulte seus emprestimos para mais informações.";
    }

    public int RetornaQuantidadeEmprestimosUsuario(int usuarioId)
    {
        return _db.Emprestimos.Count(e => e.UsuarioId == usuarioId);
    }

    public bool VerificaDisponibilidadeLivro(int livroId)
    {
        var livro = _db.Livros.FirstOrDefault(l => l.Id == livroId);
        if (livro == null) return false;
        return livro.Status == StatusLivro.Disponivel;
    }

    public List<Livro> RetornaLivrosDisponiveis()
    {
        return _db.Livros
        .Where(l => l.Status == StatusLivro.Disponivel)
        .ToList();
    }

    public string DevolverLivro(int emprestimoId, int usuarioId)
    {
        var emprestimo = _db.Emprestimos.FirstOrDefault(e => e.Id == emprestimoId);
        if (emprestimo != null && emprestimo.UsuarioId == usuarioId)
        {
            var livro = _db.Livros.FirstOrDefault(l => l.Id == emprestimo.LivroId);
            if (livro != null) livro.MudarStatus(StatusLivro.Disponivel);
            _db.Remove(emprestimo);
            _db.SaveChanges();
            return "[ ! ] -> Emprestimo finalizado!";
        }
        else
        {
            return "[ ! ] -> Emprestimo não encontrado.";
        }
    }

    public bool VerificaCodigoFunc(string codigoFunc)
    {
        return _db.Bibliotecarios.Any(b => b.CodigoFunc == codigoFunc);
    }

    public List<Emprestimo>? RetornaEmpAtrasado(string codigoFunc)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            return _db.Emprestimos.Include(e => e.Livro).Include(e => e.Usuario).Where(e => e.DataDevolucao < DateTime.Today).ToList();
        }
        return null;
    }

    public List<Emprestimo>? RetornaEmps(string codigoFunc)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            return _db.Emprestimos.Include(e => e.Livro).Include(e => e.Usuario).ToList();
        }
        return null;
    }

    public bool EncerrarEmpUsuario(string codigoFunc, int idEmprestimo)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            var emprestimo = _db.Emprestimos.Include(e => e.Livro).FirstOrDefault(e => e.Id == idEmprestimo);
            if (emprestimo == null) return false;
            var livro = _db.Livros.FirstOrDefault(l => l.Id == emprestimo.Livro.Id);
            if (livro != null) livro.MudarStatus(StatusLivro.Disponivel);
            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public bool AddLivro(string codigoFunc, Livro livro)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            _db.Livros.Add(livro);
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public Usuario? BuscarUsuarioNome(string codigoFunc, string nomeUsuario)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            return _db.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
        }
        return null;
    }

    public Usuario? BuscarUsuarioMatricula(string codigoFunc, string matriculaUsuario)
    {
        if (VerificaCodigoFunc(codigoFunc))
        {
            return _db.Usuarios.FirstOrDefault(u => u.Matricula == matriculaUsuario);
        }
        return null;
    }
}
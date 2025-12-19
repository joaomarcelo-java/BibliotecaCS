using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BibliotecaApp.Domain;
using BibliotecaApp.Service;
using BibliotecaApp.Utils;

namespace BibliotecaApp;

public class InterfaceTextoService
{
    private readonly BibliotecaAppService _service;
    private InputHelper _utils;

    public InterfaceTextoService(BibliotecaAppService service)
    {
        _service = service;
        _utils = new InputHelper();
    }

    public void TipoUsuario()
    {
        while (true)
        {
            System.Console.WriteLine("-> Bem-Vindo <-");
            System.Console.WriteLine("-> Qual tipo de usuário você é?");
            System.Console.WriteLine("[ 1 ] -> Usuário Comum");
            System.Console.WriteLine("[ 2 ] -> Bibliotecario");
            int opc = _utils.ReceberInt();

            switch (opc)
            {
                case 1:
                    UsuarioEscolhaLogin();
                    break;
                case 2:
                    BibliotecarioEscolhaLogin();
                    break;
                default:
                    System.Console.WriteLine("[ ! ] -> Opção inválida.");
                    break;
            }
        }
    }

    public void UsuarioEscolhaLogin()
    {
        while (true)
        {
            System.Console.WriteLine("-> Usuário <-");
            System.Console.WriteLine("[ 1 ] -> Já tenho matricula.");
            System.Console.WriteLine("[ 2 ] -> Não tenho matricula.");
            int opc = _utils.ReceberInt();

            switch (opc)
            {
                case 1:
                    UsuarioLogin();
                    break;
                case 2:
                    UsuarioRegistro();
                    break;
                default:
                    System.Console.WriteLine("[ ! ] -> Opção inválida");
                    break;
            }
            break;
        }
    }

    public void UsuarioLogin()
    {
        System.Console.WriteLine("-> Login <-");
        System.Console.WriteLine("-> Digite sua matricula: ");
        string matricula = _utils.ReceberStringSemEspeciais();
        Usuario usuario = _service.RetornaUsuarioMatricula(matricula);

        if (usuario != null)
        {
            MenuUsuario(usuario);
        }
        else
        {
            System.Console.WriteLine("[ ! ] -> Usuário não encontrado");
            UsuarioLogin();
        }
    }

    public void UsuarioRegistro()
    {
        System.Console.WriteLine("-> Registro <-");
        System.Console.WriteLine("-> Vamos gerar sua matricula!");
        System.Console.WriteLine("-> Digite seu nome inteiro:");
        string nome = _utils.RetornaNomeInteiro();
        System.Console.WriteLine($"Usuário registrado! Guarde sua Matricula: {_service.RegistrarUsuario(nome)}");
        System.Console.WriteLine("[ ! ] -> Tecle Enter para voltar ao menu de login!");
        System.Console.ReadLine();
        UsuarioEscolhaLogin();
    }

    public void BibliotecarioEscolhaLogin()
    {
        while (true)
        {
            System.Console.WriteLine("-> Bibliotecário <-");
            System.Console.WriteLine("[ 1 ] -> Entrar com código de funcionario");
            System.Console.WriteLine("[ 2 ] -> (Administrador) Gerar um novo código de funcionário.");
            int opc = _utils.ReceberInt();

            switch (opc)
            {
                case 1:
                    BibliotecarioLogin();
                    break;
                case 2:
                    BibliotecarioRegistro();
                    break;
                default:
                    System.Console.WriteLine("[ ! ] -> Opção inválida");
                    break;
            }
            break;
        }
    }

    public void BibliotecarioLogin()
    {
        System.Console.WriteLine("-> Login <-");
        System.Console.WriteLine("-> Digite seu código de funcionário: ");
        string codigoFunc = _utils.ReceberStringSemEspeciais().ToUpper();
        Bibliotecario bibliotecario = _service.RetornaBibliotecarioCodigo(codigoFunc);
        if (bibliotecario != null)
        {
            MenuBibliotecario(bibliotecario);
        }
        else
        {
            System.Console.WriteLine("[ ! ] -> Usuário não encontrado.");
            BibliotecarioLogin();
        }
    }

    public void BibliotecarioRegistro()
    {
        System.Console.WriteLine("-> Admin <-");
        System.Console.WriteLine("-> Digite o nome do funcionario:");
        string nome = _utils.RetornaNomeInteiro();
        System.Console.WriteLine("-> Digite a senha do admin: ");
        string senhaAdmin = System.Console.ReadLine() ?? "";
        string codigoFuncResult = _service.RegistrarBibliotecario(senhaAdmin, nome);

        if (codigoFuncResult != null)
        {
            System.Console.WriteLine($"[ ! ] -> Funcionario criado, guarde o codigo de funcionario: {codigoFuncResult}");
        }
        else
        {
            System.Console.WriteLine("[ ! ] -> Senha de admin inválida.");
            System.Console.WriteLine("[ ! ] -> Tecle Enter para voltar ao menu anterior: ");
            BibliotecarioEscolhaLogin();
        }
        System.Console.WriteLine("[ ! ] -> Tecle Enter para voltar ao menu de login: ");
        System.Console.ReadLine();
        BibliotecarioLogin();
    }

    public void MostraInfosUsuario(Usuario usuario)
    {
        System.Console.WriteLine("-> Dados <-");
        System.Console.WriteLine($"Nome: {usuario.Nome}");
        System.Console.WriteLine($"Matricula: {usuario.Matricula}");
    }

    public void MostraEmprestimosUsuario(int usuarioId)
    {
        var emprestimos = _service.RetornaEmprestimos(usuarioId);
        if (emprestimos.Count == 0)
        {
            System.Console.WriteLine("[ ! ] -> Nenhum empréstimo encontrado.");
        }
        else
        {
            foreach (var emp in emprestimos)
            {
                System.Console.WriteLine($"- Id do Emprestimo: {emp.Id}");
                System.Console.WriteLine($"- Livro: {emp.Livro.Titulo}");
                System.Console.WriteLine($"- Autor: {emp.Livro.Autor}");
                System.Console.WriteLine($"- Data de Locação: {emp.DataLocacao}");
                System.Console.WriteLine($"- Data de Devolução: {emp.DataDevolucao}");
                System.Console.WriteLine($"- Link de Acesso: {emp.Livro.Link}");
                System.Console.WriteLine("----------------------------------");
            }
        }
    }

    public void AlugarLivro(Usuario usuario)
    {
        var livros = _service.RetornaLivrosDisponiveis();
        if (livros.Count() == 0)
        {
            System.Console.WriteLine("[ ! ] -> Infelizmente estamos sem livros disponíveis no momento.");
        }
        else
        {
            foreach (var livro in livros)
            {
                System.Console.WriteLine($"Id: {livro.Id}");
                System.Console.WriteLine($"Nome: {livro.Titulo}");
                System.Console.WriteLine($"Autor: {livro.Autor}");
                System.Console.WriteLine("----------------------------------");
            }
            System.Console.WriteLine("-> Digite o id do livro que deseja alugar: ");
            int opcAlugar = _utils.ReceberInt();
            System.Console.WriteLine(_service.CriarEmprestimo(opcAlugar, usuario.Id));
        }
    }

    public void DevolverLivro(int usuarioId)
    {
        MostraEmprestimosUsuario(usuarioId);
        System.Console.WriteLine("[ ! ] -> Digite o id do emprestimo que deseja devolver: ");
        int opcDevolver = _utils.ReceberInt();
        System.Console.WriteLine(_service.DevolverLivro(opcDevolver, usuarioId));
    }

    public void MenuUsuario(Usuario usuario)
    {
        while (true)
        {
            System.Console.WriteLine("-+= Biblioteca =+-");
            System.Console.WriteLine("[ 1 ] -> Verificar minhas informações.");
            System.Console.WriteLine("[ 2 ] -> Verificar meus empréstimos.");
            System.Console.WriteLine("[ 3 ] -> Alugar um livro.");
            System.Console.WriteLine("[ 4 ] -> Devolver um livro.");
            System.Console.WriteLine("[ 5 ] -> Logout.");

            int opc = _utils.ReceberInt();

            switch (opc)
            {
                case 1:
                    MostraInfosUsuario(usuario);
                    break;
                case 2:
                    MostraEmprestimosUsuario(usuario.Id);
                    break;
                case 3:
                    AlugarLivro(usuario);
                    break;
                case 4:
                    DevolverLivro(usuario.Id);
                    break;
                case 5:
                    System.Console.WriteLine("Saindo...");
                    return;
                default:
                    System.Console.WriteLine("[ ! ] -> Opção inválida");
                    break;
            }
        }
    }

    public void MenuVerificarEmpAtraso(string codigoFunc)
    {
        var emprestimosEmAtraso = _service.RetornaEmpAtrasado(codigoFunc);
        System.Console.WriteLine("-> Emprestimos em Atraso <-");
        if (emprestimosEmAtraso == null)
        {
            System.Console.WriteLine("!!! Acesso Negado !!!");
            TipoUsuario();
        }
        if (emprestimosEmAtraso.Count == 0)
        {
            System.Console.WriteLine("[ ! ] -> Nenhum emprestimo em atraso encontrado!");
            return;
        }
        foreach (var emp in emprestimosEmAtraso)
        {
            System.Console.WriteLine($"ID do emprestimo: {emp.Id}");
            System.Console.WriteLine($"Nome do Usuário: {emp.Usuario.Nome}");
            System.Console.WriteLine($"Matricula do Usuário: {emp.Usuario.Matricula}");
            System.Console.WriteLine($"Livro: {emp.Livro.Titulo}");
            System.Console.WriteLine($"Data de Locação: {emp.DataLocacao}");
            System.Console.WriteLine($"Data de Devolução: {emp.DataDevolucao}");
        }
    }

    public void MenuVerListaEmp(string codigoFunc)
    {
        var emprestimos = _service.RetornaEmps(codigoFunc);
        System.Console.WriteLine("-> Lista de Emprestimos <-");
        if (emprestimos == null)
        {
            System.Console.WriteLine("!!! Acesso Negado !!!");
            TipoUsuario();
        }
        if (emprestimos.Count == 0)
        {
            System.Console.WriteLine("[ ! ] -> Nenhum emprestimo encontrado!");
            return;
        }
        foreach (var emp in emprestimos)
        {
            System.Console.WriteLine($"ID do emprestimo: {emp.Id}");
            System.Console.WriteLine($"Nome do Usuário: {emp.Usuario.Nome}");
            System.Console.WriteLine($"Matricula do Usuário: {emp.Usuario.Matricula}");
            System.Console.WriteLine($"Livro: {emp.Livro.Titulo}");
            System.Console.WriteLine($"Autor: {emp.Livro.Autor}");
            System.Console.WriteLine($"Data de Locação: {emp.DataLocacao}");
            System.Console.WriteLine($"Data de Devolução: {emp.DataDevolucao}");
        }
    }

    public void MenuEncerrarEmpUsu(string codigoFunc)
    {
        var emprestimos = _service.RetornaEmps(codigoFunc);
        if (emprestimos == null)
        {
            System.Console.WriteLine("!!! Acesso Negado !!!");
            TipoUsuario();
        }
        if (emprestimos.Count == 0)
        {
            System.Console.WriteLine("[ ! ] -> Nenhum empréstimo encontrado!");
            return;
        }
        foreach (var emp in emprestimos)
        {
            System.Console.WriteLine($"ID do emprestimo: {emp.Id}");
            System.Console.WriteLine($"Nome do Usuário: {emp.Usuario.Nome}");
            System.Console.WriteLine($"Matricula do Usuário: {emp.Usuario.Matricula}");
            System.Console.WriteLine($"Livro: {emp.Livro.Titulo}");
            System.Console.WriteLine($"Data de Locação: {emp.DataLocacao}");
            System.Console.WriteLine($"Data de Devolução: {emp.DataDevolucao}");
        }
        System.Console.WriteLine("-> Digite o ID do empréstimo que deseja encerrar, ou SAIR para retornar ao menu anterior:");
        string opcSair = _utils.ReceberStringSemEspeciais();
        if (opcSair == "SAIR")
        {
            System.Console.WriteLine("Retornando ao menu anterior...");
            return;
        }
        int idEmprestimo = _utils.TransformarInt(opcSair);
        if (_service.EncerrarEmpUsuario(codigoFunc, idEmprestimo))
        {
            System.Console.WriteLine("[ ! ] -> Emprestimo removido com sucesso!");
        }
        else
        {
            System.Console.WriteLine("[ ! ] -> Emprestimo inexistente.");
        }
    }

    public void MenuAddLivro(string codigoFunc)
    {
        System.Console.WriteLine("-> Livro <-");
        System.Console.WriteLine("-> Primeiro, digite o nome do livro: ");
        string titulo = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("-> Agora, digite o autor do livro: ");
        string autor = System.Console.ReadLine() ?? "";
        System.Console.WriteLine("-> Insira o link de acesso ao livro: ");
        string link = System.Console.ReadLine() ?? "";
        Livro livro = new Livro(titulo, autor, link, StatusLivro.Disponivel);
        if (_service.AddLivro(codigoFunc, livro))
        {
            System.Console.WriteLine("[ ! ] -> Livro adicionado com sucesso!");
        }
        else
        {
            System.Console.WriteLine("!!! Acesso Negado !!!");
        }
    }

    public void MenuVerDadosUsuario(string codigoFunc)
    {
        System.Console.WriteLine("-> Buscar Usuário <-");
        System.Console.WriteLine("-> Como deseja buscar o usuário?");
        System.Console.WriteLine("[ 1 ] -> Pelo nome inteiro.");
        System.Console.WriteLine("[ 2 ] -> Pela matricula.");
        int opc = _utils.ReceberInt();

        switch (opc)
        {
            case 1:
                System.Console.WriteLine("-> Digite o nome inteiro do usuário:");
                string nomeBusca = _utils.RetornaNomeInteiro();
                var userPorNome = _service.BuscarUsuarioNome(codigoFunc, nomeBusca);
                if (userPorNome != null)
                {
                    System.Console.WriteLine("-> Usuário Encontrado <-");
                    System.Console.WriteLine($"-> Nome: {userPorNome.Nome}");
                    System.Console.WriteLine($"-> Matricula: {userPorNome.Matricula}");
                }
                else
                {
                    System.Console.WriteLine("[ ! ] -> Usuário não encontrado");
                }
                break;
            case 2:
                System.Console.WriteLine("-> Digite a matricula do usuário:");
                string matBusca = _utils.ReceberStringSemEspeciais();
                var userPorMat = _service.BuscarUsuarioMatricula(codigoFunc, matBusca);
                if (userPorMat != null)
                {
                    System.Console.WriteLine("-> Usuário Encontrado <-");
                    System.Console.WriteLine($"-> Nome: {userPorMat.Nome}");
                    System.Console.WriteLine($"-> Matricula: {userPorMat.Matricula}");
                }
                else
                {
                    System.Console.WriteLine("[ ! ] -> Usuário não encontrado");
                }
                break;
            default:
                System.Console.WriteLine("[ ! ] -> Opção inválida!");
                break;
        }
    }

    public void MenuBibliotecario(Bibliotecario bibliotecario)
    {
        while (true)
        {
            System.Console.WriteLine("-+= Bibliotecario =+-");
            System.Console.WriteLine("[ 1 ] -> Verificar emprestimos em atraso.");
            System.Console.WriteLine("[ 2 ] -> Encerrar emprestimo de usuário.");
            System.Console.WriteLine("[ 3 ] -> Verificar lista de emprestimos");
            System.Console.WriteLine("[ 4 ] -> Exibir dados de usuario.");
            System.Console.WriteLine("[ 5 ] -> Adicionar livro.");
            System.Console.WriteLine("[ 6 ] -> Logout");

            int opc = _utils.ReceberInt();

            switch (opc)
            {
                case 1:
                    MenuVerificarEmpAtraso(bibliotecario.CodigoFunc);
                    break;
                case 2:
                    MenuEncerrarEmpUsu(bibliotecario.CodigoFunc);
                    break;
                case 3:
                    MenuVerListaEmp(bibliotecario.CodigoFunc);
                    break;
                case 4:
                    MenuVerDadosUsuario(bibliotecario.CodigoFunc);
                    break;
                case 5:
                    MenuAddLivro(bibliotecario.CodigoFunc);
                    break;
                case 6:
                    System.Console.WriteLine("Saindo...");
                    return;
            }
        }
    }
}
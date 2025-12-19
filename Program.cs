using System;
using BibliotecaApp.Domain;
using BibliotecaApp.Data;
using BibliotecaApp.Service;

namespace BibliotecaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new AppDbContexto();
            var BibliotecaDb = new BibliotecaAppService(db);
            InterfaceTextoService menus = new InterfaceTextoService(BibliotecaDb);
            menus.TipoUsuario();

            
        }
    }
}
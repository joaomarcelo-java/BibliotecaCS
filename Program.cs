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
            using (var context = new AppDbContexto())
            {
                context.Database.EnsureCreated();
                var bibliotecaDb = new BibliotecaAppService(context);
                
                InterfaceTextoService menus = new InterfaceTextoService(bibliotecaDb);
                menus.TipoUsuario();
            }
        }
    }
}
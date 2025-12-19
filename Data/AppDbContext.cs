using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Domain;
using BibliotecaApp.Data;

namespace BibliotecaApp.Data;

public class AppDbContexto : DbContext
{
    public DbSet<Livro> Livros{set; get;}
    public DbSet<Emprestimo> Emprestimos{get; set;}
    public DbSet<Usuario> Usuarios{get; set;}
    public DbSet<Bibliotecario> Bibliotecarios{get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=biblioteca.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Livro>()
            .Property(l => l.Status)
            .HasConversion<int>();


        modelBuilder.Entity<Usuario>().HasBaseType<Pessoa>();
        modelBuilder.Entity<Bibliotecario>().HasBaseType<Pessoa>();
        
        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Usuario)
            .WithMany()
            .HasForeignKey(e => e.UsuarioId);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Livro)
            .WithMany()
            .HasForeignKey(e => e.LivroId);
    }
}
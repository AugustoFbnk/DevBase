using DevBase.Domain.Entidades.Cadastros;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevBase.Infra.Data.Contextos
{
    public class DevBaseContext : DbContext
    {
        public DevBaseContext(DbContextOptions<DevBaseContext> options) : base(options)
        {
        }
        public DbSet<Desenvolvedor> Desenvolvedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Desenvolvedor>().HasData(
               new Desenvolvedor
               {
                   Id = 1,
                   Nome = "Jane Doe",
                   DataNascimento = new DateTime(1990, 01, 05),
                   Hobby = "Assistir filmes",
                   Idade = 31,
                   Sexo = Sexo.Feminino
               });

            modelBuilder.Entity<Desenvolvedor>().HasData(
               new Desenvolvedor
               {
                   Id = 2,
                   Nome = "John Doe",
                   DataNascimento = new DateTime(1992, 01, 05),
                   Hobby = "Assistir filmes",
                   Idade = 29,
                   Sexo = Sexo.Masculino
               });
        }
    }
}

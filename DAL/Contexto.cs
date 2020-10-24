using Microsoft.EntityFrameworkCore;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroPublicaciones.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Generos> Generos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=DATA/Publicaciones.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Generos>().HasData(new Generos
            {
               GeneroId = 1,
               Gnero = "Trap"
            });

            modelBuilder.Entity<Generos>().HasData(new Generos
            {
               GeneroId = 2,
               Gnero = "Dubstep"
            });

            modelBuilder.Entity<Generos>().HasData(new Generos
            {
               GeneroId = 3,
               Gnero = "House"
            });

            modelBuilder.Entity<Generos>().HasData(new Generos
            {
               GeneroId = 4,
               Gnero = "Bass"
            });

             modelBuilder.Entity<Generos>().HasData(new Generos
            {
               GeneroId = 5,
               Gnero = "Chill"
            });


        }
    }
}

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=DATA/Publicaciones.db");
        }
    }
}

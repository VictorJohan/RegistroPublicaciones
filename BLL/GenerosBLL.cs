using Microsoft.EntityFrameworkCore;
using RegistroPublicaciones.DAL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistroPublicaciones.BLL
{
    public class GenerosBLL
    {
        public static bool Guardar(Generos genero)
        {
            if (!Existe(genero.GeneroId))
                return Insertar(genero);
            else
                return Modificar(genero);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Generos.Any(g => g.GeneroId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Insertar(Generos genero)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Generos.Add(genero);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Modificar(Generos genero)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(genero).State = EntityState.Modified;
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static Generos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Generos genero;

            try
            {
                genero = contexto.Generos.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return genero;
        }

        public static List<Generos> GetList()
        {
            Contexto contexto = new Contexto();
            List<Generos> lista = new List<Generos>();

            try
            {
                lista = contexto.Generos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RegistroPublicaciones.DAL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPublicaciones.BLL
{
    public class PublicacionesBLL
    {
        public static bool Guardar(Publicaciones publicacion)
        {
            if (!Existe(publicacion.PublicacionId))
                return Insertar(publicacion);
            else
                return Modificar(publicacion);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Publicaciones.Any(p => p.PublicacionId == id);
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

        private static bool Insertar(Publicaciones publicacion)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Publicaciones.Add(publicacion);
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

        private static bool Modificar(Publicaciones publicacion)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(publicacion).State = EntityState.Modified;
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

        public static Publicaciones Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Publicaciones publicacion;

            try
            {
                publicacion = contexto.Publicaciones.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return publicacion;
        }

        public static bool Eliminar(Publicaciones publicacion)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(publicacion).State = EntityState.Deleted;
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

        public static List<Publicaciones> GetListPublicaciones()
        {
            Contexto contexto = new Contexto();
            List<Publicaciones> lista = new List<Publicaciones>();

            try
            {
                lista = contexto.Publicaciones.ToList();
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

        public static List<Publicaciones> GetListPublicaciones(Expression<Func<Publicaciones, bool>> criterio) 
        {
            Contexto contexto = new Contexto();
            List<Publicaciones> lista = new List<Publicaciones>();

            try
            {
                lista = contexto.Publicaciones.Where(criterio).ToList();
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

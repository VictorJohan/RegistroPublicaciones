using Microsoft.EntityFrameworkCore;
using RegistroPublicaciones.DAL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Conexion conexion = new Conexion();
            SqlCommand command = new SqlCommand("GuardarPublicacion", conexion.Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            bool ok = false;

            try
            {
                conexion.AbrirConexion();
                command.Parameters.AddWithValue("@PublicacionId", publicacion.PublicacionId);
                command.Parameters.AddWithValue("@Descripcion", publicacion.Descripcion);
                command.Parameters.AddWithValue("@Nombre", publicacion.Nombre);
                command.Parameters.AddWithValue("@GeneroId", publicacion.GeneroId);
                command.Parameters.AddWithValue("@NombreCancion", publicacion.NombreCancion);
                command.Parameters.AddWithValue("@Link", publicacion.Link);
                command.Parameters.AddWithValue("@Fecha", publicacion.Fecha);
                command.Parameters.AddWithValue("@Wallpaper", publicacion.Wallpaper);
                
                if(command.ExecuteNonQuery() > 0) { ok = true; }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return ok;
        }

        /*public static bool Guardar(Publicaciones publicacion)
        {
            if (!Existe(publicacion.PublicacionId))
                return Insertar(publicacion);
            else
                return Modificar(publicacion);
        }*/

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

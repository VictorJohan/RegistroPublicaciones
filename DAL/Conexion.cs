using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace RegistroPublicaciones.DAL
{
    public class Conexion
    {
        public string cadena { get; set; } = "Data Source=bumixx.database.windows.net;Initial Catalog=Publicaciones;User ID=Bumixx;Password=Ci:84568003";
        public SqlConnection Connection { get; set; } = new SqlConnection();

        public Conexion()
        {
            Connection.ConnectionString = cadena;
        }

        public void AbrirConexion()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {

                MessageBox.Show($"No se logró acceder a la base de datos:\n{e.Message}", "Error.");
            }
        }

        public void CerrarConexion()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"No se logró cerrar a la base de datos:\n{e.Message}", "Error.");

            }
        }
    }
}

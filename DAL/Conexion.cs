using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace RegistroPublicaciones.DAL
{
    public class Conexion
    {
        string cadena = "Data Source=bumixx.database.windows.net;Initial Catalog=Publicaciones;User ID=Bumixx;Password=Ci:84568003";
        public SqlConnection connection = new SqlConnection();

        public Conexion()
        {
            connection.ConnectionString = cadena;
        }

        public void AbrirConexion()
        {
            try
            {
                connection.Open();
                MessageBox.Show("Abierta");
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
                connection.Close();
                MessageBox.Show("Cerrada");
            }
            catch (Exception e)
            {
                MessageBox.Show($"No se logró cerrar a la base de datos:\n{e.Message}", "Error.");

            }
        }
    }
}

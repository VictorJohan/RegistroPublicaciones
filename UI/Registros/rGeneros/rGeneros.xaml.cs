using RegistroPublicaciones.BLL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroPublicaciones.UI.Registros.rGeneros
{
    /// <summary>
    /// Interaction logic for rGeneros.xaml
    /// </summary>
    public partial class rGeneros : Window
    {
        private Generos Generos = new Generos();
        public rGeneros()
        {
            InitializeComponent();
            this.DataContext = Generos;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = GenerosBLL.Buscar(int.Parse(GeneroIdTextBox.Text));

            if(encontrado != null)
            {
                Generos = encontrado;
                this.DataContext = Generos;
            }
            else
            {
                MessageBox.Show("No se encontro ningún registro con este Id.", "No hay resultados.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenerosBLL.Guardar(Generos))
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("No se guardo");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Limpiar()
        {
            Generos = new Generos();
            this.DataContext = Generos;
        }
    }
}

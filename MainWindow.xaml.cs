using Microsoft.Win32;
using RegistroPublicaciones.BLL;
using RegistroPublicaciones.Entidades;
using RegistroPublicaciones.UI;
using RegistroPublicaciones.UI.Registros;
using RegistroPublicaciones.UI.Registros.rGeneros;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroPublicaciones
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //todo: Agregar Excepcion Handler
        //todo: Intenta poner el messageBox con material design
        //todo: Hacer menu
        //todo: Hacer presentacion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPublicacion rPublicacion = new rPublicacion();
            rPublicacion.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            cPublicacion cPublicacion = new cPublicacion();
            cPublicacion.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rGeneros rGeneros = new rGeneros();
            rGeneros.Show();
        }
    }
}

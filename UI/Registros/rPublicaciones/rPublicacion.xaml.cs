using Microsoft.Win32;
using MyToolkit.Multimedia;
using RegistroPublicaciones.BLL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroPublicaciones.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPublicacion.xaml
    /// </summary>
    public partial class rPublicacion : Window
    {
        Publicaciones Publicacion = new Publicaciones();
        
        public rPublicacion()
        {
            InitializeComponent();
            GeneroComboBox.ItemsSource = GenerosBLL.GetList();
            GeneroComboBox.SelectedValuePath = "GeneroId";
            GeneroComboBox.DisplayMemberPath = "Genero";
            this.DataContext = Publicacion;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IrButton_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl(LinkTextBox.Text);
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            InsertarButton.Content = "Cambiar";
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public void Cargar()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                WallpaperImage.Source = new BitmapImage(new Uri(op.FileName));
                //Publicacion.Wallpaper = BitmapSourceToByteArray((BitmapSource)photo.Source);
            }
        }

    }
}

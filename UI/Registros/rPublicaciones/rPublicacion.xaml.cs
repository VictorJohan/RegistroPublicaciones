using Microsoft.Win32;
using MyToolkit.Multimedia;
using RegistroPublicaciones.BLL;
using RegistroPublicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        byte[] wallpaper;
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
            var encontrado = PublicacionesBLL.Buscar(int.Parse(VideoIdTextBox.Text));

            if(encontrado != null)
            {
                Publicacion = encontrado;
                this.DataContext = Publicacion;
                WallpaperImage.Source = LoadImage(Publicacion.Wallpaper);
                EstadoBotonInsertar();
            }
            else
            {
                MessageBox.Show("No se encontro ningún registro con este Id.", "No hay resultados.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void IrButton_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl(LinkTextBox.Text);
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            Publicacion.Wallpaper = wallpaper;
            EstadoBotonInsertar();
            
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar()) { return; }

            if (PublicacionesBLL.Guardar(Publicacion))
            {
                Limpiar();
                MessageBox.Show("Registro Guardado.", "Exito.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo ha salido mal, no se pudo guardar el registro.", "Error.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarEliminar()) { return; }

            if (PublicacionesBLL.Eliminar(Publicacion))
            {
                Limpiar();
                MessageBox.Show("Se ha eliminado el registro.", "Registro Eliminado.", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo ha salido mal, no se pudo eliminar el registro.", "Error.",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                wallpaper = File.ReadAllBytes(op.FileName);
            }
        }

        public void Limpiar()
        {
            Publicacion = new Publicaciones();
            this.DataContext = Publicacion;
            WallpaperImage.Source = null;
            EstadoBotonInsertar();
        }

        public bool ValidarEliminar()
        {
            bool confirmar;

            var registro = PublicacionesBLL.Buscar(Publicacion.PublicacionId);
            if (registro.Link != Publicacion.Link)
            {
                MessageBox.Show("Busque el registro que desea eliminar.", "Advertencia.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            confirmar = MessageBox.Show("¿Seguro que deseas eliminar este registro?", "Eliminar.",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No;
            if (confirmar) { return false; }

            return true;
        }

        public bool Validar()
        {
            if(VideoIdTextBox.Text.Length == 0 || DescripcionTextBox.Text.Length == 0 || GeneroComboBox.SelectedIndex == -1
                || LinkTextBox.Text.Length == 0 || NombreCancionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurate de haber llenado todos los campos.", "Campos vacios.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(!Regex.IsMatch(LinkTextBox.Text, @"^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$"))
            {
                MessageBox.Show("Asegurate de haber introducido una URL valida, esta debe pertenecer a Youtube.", "URL no valida.",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public void EstadoBotonInsertar()
        {
            if(WallpaperImage.Source != null)
                InsertarButton.Content = "Cambiar";
            else
                InsertarButton.Content = "Insertar";
        }

    }
}

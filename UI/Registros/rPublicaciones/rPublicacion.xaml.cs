using Microsoft.Win32;
using MyToolkit.Multimedia;
using RegistroPublicaciones.BLL;
using RegistroPublicaciones.DAL;
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
        private int previousLineCount = 0;
        byte[] wallpaper;
        Conexion conexion = new Conexion();

        public rPublicacion()
        {
            InitializeComponent();
            //Rellenando y configurando GeneroComboBox
            //GeneroComboBox.ItemsSource = GenerosBLL.GetList();
            //GeneroComboBox.SelectedValuePath = "GeneroId";
            //GeneroComboBox.DisplayMemberPath = "Genero";
            this.DataContext = Publicacion;
        }

        //Busaca un registro en la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = PublicacionesBLL.Buscar(int.Parse(VideoIdTextBox.Text));

            if(encontrado != null)
            {
                Publicacion = encontrado;
                this.DataContext = Publicacion;
                WallpaperImage.Source = ConvertirArrayToImage(Publicacion.Wallpaper);//Convierte el array de Wallpaper en una imagen.
                EstadoBotonInsertar();
            }
            else
            {
                MessageBox.Show("No se encontro ningún registro con este Id.", "No hay resultados.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Muestra el video en youtube.
        private void IrButton_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl(LinkTextBox.Text);
        }

        //Insserta una imagen para guardar en la base de datos.
        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            Publicacion.Wallpaper = wallpaper;
            EstadoBotonInsertar();
            
        }

        //Convierte el array de byte a una imagen para mostrar.
        private static BitmapImage ConvertirArrayToImage(byte[] imageData)
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

        //Limpia el formulario para un nuevo registro.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Guarda un registro en la base de datos
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

        //Elimina un registro de la base de datos.
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

        //Abre el navegador con el Link del video.
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

        //Carga y muestra el wallpaper que se guardara en la base de datos.
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

        //Limpia el registro.
        public void Limpiar()
        {
            Publicacion = new Publicaciones();
            this.DataContext = Publicacion;
            WallpaperImage.Source = null;
            EstadoBotonInsertar();
        }

        //Valida el el evento eliminar.
        public bool ValidarEliminar()
        {
            bool confirmar;

            var registro = PublicacionesBLL.Buscar(Publicacion.PublicacionId);
            if (registro.Link != Publicacion.Link)//Obliga al usuario a buscar el registro para borrarlo
            {
                MessageBox.Show("Busque el registro que desea eliminar.", "Advertencia.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Pregunta si desea eliminar el registro en caso de que la respuesta sea "No" returna false.
            confirmar = MessageBox.Show("¿Seguro que deseas eliminar este registro?", "Eliminar.",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No;
            if (confirmar) { return false; }

            return true;
        }

        //Este metodo valida los campos del registro.
        public bool Validar()
        {
            //Valida que todos los campos esten llenos
            if(VideoIdTextBox.Text.Length == 0 || DescripcionTextBox.Text.Length == 0 || GeneroComboBox.SelectedIndex == -1
                || LinkTextBox.Text.Length == 0 || NombreCancionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurate de haber llenado todos los campos.", "Campos vacios.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que el link introducido sea un link de un video de Youtube
            if(!Regex.IsMatch(LinkTextBox.Text, @"^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$"))
            {
                MessageBox.Show("Asegurate de haber introducido una URL valida, esta debe pertenecer a Youtube.", "URL no valida.",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //Cambia el estado del boton de Insertar a Cambiar dependiendo de la circunstacias.
        public void EstadoBotonInsertar()
        {
            if(WallpaperImage.Source != null)
                InsertarButton.Content = "Cambiar";
            else
                InsertarButton.Content = "Insertar";
        }

        private void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DescripcionTextBox.LineCount > previousLineCount)
            {
                previousLineCount = DescripcionTextBox.LineCount;
            }
        }
    }
}

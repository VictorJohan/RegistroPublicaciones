﻿using Microsoft.Win32;
using RegistroPublicaciones.BLL;
using RegistroPublicaciones.Entidades;
using RegistroPublicaciones.UI;
using RegistroPublicaciones.UI.Registros;
using System;
using System.Collections.Generic;
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

        public void Cargar()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //photo.Source = new BitmapImage(new Uri(op.FileName));
                //Publicacion.Wallpaper = BitmapSourceToByteArray((BitmapSource)photo.Source);
            }
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

    }
}

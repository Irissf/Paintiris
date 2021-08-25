﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Paintiris
{

    /// <summary>
    /// Lógica de interacción para NuevoDoc.xaml
    /// </summary>
    public partial class NuevoDoc : Window
    {

        // ************************************************ PROPIEDADES *****************************************************

        private List<string> medidas = new List<string>() {
            "Píxeles",
            "Centímetros",
            "Pulgadas",
            "Milímetros"
        };
        private List<string> medidasDpi = new List<string>() {
            "Píxeles/pulgafa",
            "Píxeles/centímetro"
        };


        //Para construir el canvas
        public int anchoCanvas = 800;
        public int altoCanvas = 600;
        public Color colorCanvas = Color.FromRgb(255,255,255);
        public string nombreCanvas = "sin titulo-1";


        // ************************************************ CONTRUCTOR *****************************************************


        public NuevoDoc()
        {
            InitializeComponent();
            LlenarComponentes();

        }



        // ************************************************ MÉTODOS *****************************************************


        //TODO controlar que el valor introducido sea número entre 0-255

        private void LlenarComponentes()
        {
            foreach (string medida in medidas)
            {
                cbAlto.Items.Add(medida);
                cbAncho.Items.Add(medida);
            }
            cbAlto.SelectedIndex = 0;
            cbAncho.SelectedIndex = 0;

            foreach (string medidaDpi in medidasDpi)
            {
                cbResolucion.Items.Add(medidaDpi);
            }
            cbResolucion.SelectedIndex = 0;
        }


        // ************************************************ EVENTOS DE LOS COMPONENTES *****************************************************


        /// <summary>
        /// Para cambiar el color del canvas a medida que movemos los valores de rgb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slColorRojo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromRgb((byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            cvColor.Background = new SolidColorBrush(color);
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combito = (ComboBox)sender;
            //Hacemos que cambien a la vez los dos combobox de las medidas del canvas
            if (combito.Name == "cbAncho")
            {
                cbAlto.SelectedIndex = combito.SelectedIndex;
            }
            if (combito.Name == "cbAlto")
            {
                cbAncho.SelectedIndex = combito.SelectedIndex;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = false;
            //y cerramos la ventana modal
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = true;

            //cogemos los valores
            //TODO capturar excepciones
            if (chColor.IsChecked !=null && chColor.IsChecked == true)
            {
                colorCanvas = Color.FromRgb(Convert.ToByte(txtColorRojo.Text), Convert.ToByte(txtColorVerde.Text), Convert.ToByte(txtColorAzul.Text));
            }
            anchoCanvas = Convert.ToInt32(txtAncho.Text);
            altoCanvas = Convert.ToInt32(txtAlto.Text);
            nombreCanvas = txtNombre.Text;

            this.Close();
        }

        private void txtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox colores = (TextBox)sender;
            string numero = "";
            try
            {
                byte colorcito = Convert.ToByte(colores.Text);
            }
            catch (FormatException)
            {
                //si no lo mete, es que algo hizo mal el usuario
                colores.Text = "0";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            slColorAzul.IsEnabled = true;
            slColorRojo.IsEnabled = true;
            slColorVerde.IsEnabled = true;
            txtColorAzul.IsEnabled = true;
            txtColorRojo.IsEnabled = true;
            txtColorVerde.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            slColorAzul.IsEnabled = false;
            slColorRojo.IsEnabled = false;
            slColorVerde.IsEnabled = false;
            txtColorAzul.IsEnabled = false;
            txtColorRojo.IsEnabled = false;
            txtColorVerde.IsEnabled = false;
        }
    }
}

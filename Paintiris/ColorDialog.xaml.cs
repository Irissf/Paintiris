using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public Color color;
        public ColorDialog()
        {
            InitializeComponent();
            sltransparencia.Value = 255;
            txttransparencia.Text = "255";
        }

        private void txtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox colores = (TextBox)sender;
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

        private void slColorRojo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromArgb((byte)sltransparencia.Value,(byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            cv_colorMuetra.Background = new SolidColorBrush(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = true;
            color = Color.FromArgb((byte)sltransparencia.Value,(byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            this.Close();
        }
    }
}

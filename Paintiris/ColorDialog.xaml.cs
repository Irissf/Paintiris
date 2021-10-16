using Paintiris.Clases;
using System;
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
    /// Lógica de interacción para ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public Color color;
        Conversor conversor = new Conversor();

        //voy a generar un control, apra que solo se inicie cuando se hallan ejecutado todos por primera vez
        bool cambiarColor = false;
        int i = 0;  //para el control de los cuatro text
        public ColorDialog(Color color)
        {
            InitializeComponent();
            this.color = color;
            Trace.WriteLine(""+color.A);
            txttransparencia.Text = "" + color.A;
            txtColorAzul.Text = "" + color.B;
            txtColorRojo.Text = "" + color.R;
            txtColorVerde.Text = "" + color.G;

        }

        private void TxtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox colores = (TextBox)sender;
            try
            {
                byte colorcito = Convert.ToByte(colores.Text);
                i++;
                if (cambiarColor)
                {
                    //MessageBox.Show("Entro" + i);
                    txtHex.Text = "" + conversor.generaHEX(Convert.ToInt32(txtColorRojo.Text)) + conversor.generaHEX(Convert.ToInt32(txtColorVerde.Text))
                                    + conversor.generaHEX(Convert.ToInt32(txtColorAzul.Text));
                    i = 4;
                }
                if (i == 4)
                {
                    cambiarColor = true;
                }
            }
            catch (FormatException)
            {
                //si no lo mete, es que algo hizo mal el usuario
                colores.Text = "0";
            }
        }

        private void SlColor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            // lo ponemos así para cuando cambie por el movimiento de rgb no se ejecute este código
            if (txt.IsFocused)
            {
                int[] rgb = conversor.generarGRBA(txtHex.Text);

                //aqui ponemos el switch case para el código de la libreata
                txtColorRojo.Text = "" + rgb[0];
                txtColorVerde.Text = "" + rgb[1];
                txtColorAzul.Text = "" + rgb[2];
            }
         
        }
    }
}

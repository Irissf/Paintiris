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
            Trace.WriteLine("" + color.A);
            txttransparencia.Text = "" + color.A;
            txtColorAzul.Text = "" + color.B;
            txtColorRojo.Text = "" + color.R;
            txtColorVerde.Text = "" + color.G;

        }

        #region CAMBIO COLOR
        private void TxtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox colores = (TextBox)sender;
            try
            {
                byte colorcito = Convert.ToByte(colores.Text);
                i++;
                //necesitamos saber si está foucs el componente, para que no empiecen a cruzar datos, el hex y el rgb
                if (cambiarColor && colores.IsFocused)
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

        /// <summary>
        /// Cuando se realiza un movimiento en un slider de color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlColor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            Color color = Color.FromArgb((byte)sltransparencia.Value, (byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            cv_colorMuetra.Background = new SolidColorBrush(color);
            try
            {
                i++;
                //necesitamos saber si está foucs el componente, para que no empiecen a cruzar datos, el hex y el rgb
                if (cambiarColor && slider.IsFocused)
                {
                    //MessageBox.Show("Entro" + i);
                    txtHex.Text = "" + conversor.generaHEX(Convert.ToInt32(slColorRojo.Value)) + conversor.generaHEX(Convert.ToInt32(slColorVerde.Value))
                                    + conversor.generaHEX(Convert.ToInt32(slColorAzul.Value));
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
                slider.Value = 0;
            }
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int codigoHexTamano = txtHex.Text.Length;
            string textoHex = "";

            // lo ponemos así para cuando cambie por el movimiento de rgb no se ejecute este código
            if (txt.IsFocused)
            {
                //mediante el switch controlamos que cuando borre y empiece a escribri, los cambios se hagan en el rgb correcto
                switch (codigoHexTamano)
                {
                    case 0:
                        textoHex = string.Format("000000");
                        break;
                    case 1:
                        textoHex = string.Format("0{0}0000", txtHex.Text);
                        break;
                    case 2:
                        textoHex = string.Format("{0}{1}0000", txtHex.Text[0], txtHex.Text[1]);
                        break;
                    case 3:
                        textoHex = string.Format("{0}{1}0{2}00", txtHex.Text[0], txtHex.Text[1], txtHex.Text[2]);
                        break;
                    case 4:
                        textoHex = string.Format("{0}{1}{2}{3}00", txtHex.Text[0], txtHex.Text[1], txtHex.Text[2], txtHex.Text[3]);
                        break;
                    case 5:
                        textoHex = string.Format("{0}{1}{2}{3}0{4}", txtHex.Text[0], txtHex.Text[1], txtHex.Text[2], txtHex.Text[3], txtHex.Text[4]);
                        break;
                    case 6:
                        textoHex = string.Format("{0}{1}{2}{3}{4}{5}", txtHex.Text[0], txtHex.Text[1], txtHex.Text[2], txtHex.Text[3], txtHex.Text[4], txtHex.Text[5]);
                        break;
                    default:
                        break;
                }

                //aqui ponemos el switch case para el código de la libreata
                int[] rgb = conversor.generarGRBA(textoHex);
                txtColorRojo.Text = "" + rgb[0];
                txtColorVerde.Text = "" + rgb[1];
                txtColorAzul.Text = "" + rgb[2];

            }

        }
        #endregion

        #region BOTONES 
        /// <summary>
        /// Botón de aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = true;
            color = Color.FromArgb((byte)sltransparencia.Value, (byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            this.Close();
        }
        #endregion

    }
}

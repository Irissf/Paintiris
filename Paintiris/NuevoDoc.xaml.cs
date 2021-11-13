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
    /// Lógica de interacción para NuevoDoc.xaml
    /// </summary>
    public partial class NuevoDoc : Window
    {

        #region PROPIEDADES 

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
        public int minTamano = 0;
        public int maxtamano = 10000;

        public int anchoCanvas = 800;
        public int altoCanvas = 600;
        public Color colorCanvas = Color.FromRgb(255,255,255);
        public string nombreCanvas = "sin titulo-1";
        #endregion

        public NuevoDoc()
        {
            InitializeComponent();
            LlenarComponentes();

        }


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


        #region EVENTOS COMPONENTES


        /// <summary>
        /// Para cambiar el color del canvas a medida que movemos los valores de rgb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CambiarColorCanvasMuestrario(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromRgb((byte)slColorRojo.Value, (byte)slColorVerde.Value, (byte)slColorAzul.Value);
            cvColor.Background = new SolidColorBrush(color);
        }


        /// <summary>
        /// Para que cambien a la vez los dos combobox de las medidas del canvas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Botón de cancelar, cierra la ventana y pone el valor para el resultado del DialogResult a false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boton_Cancelar(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = false;
            //y cerramos la ventana modal
            this.Close();
        }

        /// <summary>
        /// Botón de aceptar, pone el valor para el resultado del DialogResult a true, y guarda todos los ajustes del lienzo
        /// que ha puesto el usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boton_Aceptar(object sender, RoutedEventArgs e)
        {
            //Le indicamos el resultado del formulario modal, para actuar en consonancia con él
            DialogResult = true;

            //cogemos los valores
            //TODO capturar excepciones
            if (chColor.IsChecked !=null && chColor.IsChecked == true)
            {
                colorCanvas = Color.FromRgb(Convert.ToByte(txtColorRojo.Text), Convert.ToByte(txtColorVerde.Text), Convert.ToByte(txtColorAzul.Text));
            }
            try
            {
                anchoCanvas = Convert.ToInt32(txtAncho.Text);
                altoCanvas = Convert.ToInt32(txtAlto.Text);

                //controlamos el tamaño del canvas con un mim y un máximo, que podemos cambiar
                if (anchoCanvas < minTamano || anchoCanvas> maxtamano)
                {
                    anchoCanvas = 800;
                }
                if (altoCanvas < minTamano || altoCanvas > maxtamano)
                {
                    altoCanvas = 600;
                }
            }
            catch (FormatException)
            {
                //si pone un tipo de dato incorrecto coloca los que tenia por defecto
                anchoCanvas = 800;
                altoCanvas = 600;
            }
            catch (OverflowException)
            {
                //si pone un tipo de dato demasiado grande
                anchoCanvas = 800;
                altoCanvas = 600;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            nombreCanvas = txtNombre.Text;

            this.Close();
        }

        /// <summary>
        /// Para controlar que en los textbox de los colores el usuario no ponga letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Cuando el checkbox de color personalizado es activado, habilitamos todos los componentes necesarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            slColorAzul.IsEnabled = true;
            slColorRojo.IsEnabled = true;
            slColorVerde.IsEnabled = true;
            txtColorAzul.IsEnabled = true;
            txtColorRojo.IsEnabled = true;
            txtColorVerde.IsEnabled = true;
        }

        /// <summary>
        /// Cuando el checkbox es deshabilitado, deshabilitamos los componentes para cambiar el color del canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            slColorAzul.IsEnabled = false;
            slColorRojo.IsEnabled = false;
            slColorVerde.IsEnabled = false;
            txtColorAzul.IsEnabled = false;
            txtColorRojo.IsEnabled = false;
            txtColorVerde.IsEnabled = false;
        }

        #endregion
    }
}

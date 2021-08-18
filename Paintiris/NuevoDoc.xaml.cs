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
        public NuevoDoc()
        {
            InitializeComponent();
        }

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

        //TODO controlar que el valor introducido sea número entre 0-255
        //¿Que mierda pasa con el value 255 que me salta excepción con el verde???????
    }
}

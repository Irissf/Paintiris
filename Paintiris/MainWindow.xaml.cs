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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paintiris
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
  
            NuevoDoc win = new NuevoDoc();
            win.ShowDialog();
            if (win.DialogResult == true)
            {
                Canvas lienzo = new Canvas();

                //pasamos de color a brush
                SolidColorBrush brush = new SolidColorBrush(win.colorCanvas);
                lienzo.Background = brush;
                lienzo.Height = win.altoCanvas;
                lienzo.Width = win.anchoCanvas;
                lblInfo.Content = "Nombre del documento: "+win.nombreCanvas;

                gr_folio.Children.Add(lienzo);
            }

        }

        /// <summary>
        /// Cada vez que se cambia una de las pestañas del menú
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuPestanhas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //un switch con los diferentes menus, crearemos una clase por cada uno de ellos
            /*averiguamos que pestañita tenemos marcada*/
            
            if (ti_inicio.IsSelected)
            {
                //Trace es una alternativa a console.writeline
                Trace.WriteLine("inicio");
            }
            if (ti_archivo.IsSelected)
            {
                Trace.WriteLine("archivo");
            }
            if (ti_estudio.IsSelected)
            {
                Trace.WriteLine("estudio");
            }
            if (ti_ver.IsSelected)
            {
                Trace.WriteLine("ver");
            }
        }

    }
}

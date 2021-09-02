using Paintiris.Inicio;
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

        //**************************************** PROPIEDADES ****************************************************

        //Control del dibujo por parte de la clase pincel
        public InkCanvas lienzo;
        public bool dibujar;

        //Clase gestión pinceles
        Pinceles pinceles;

        //Clase gestión de los archivos
        Archivo archi;

        //**************************************** CONSTRUCTOR ****************************************************

        public MainWindow()
        {
            InitializeComponent();
            lienzo = lienzoPorDefecto;
            archi = new Archivo(this.lienzo);

        }

        //**************************************** EVENTOS DE COMPONENTES *******************************************

        //BOTONES__________________________________________________________________

        /// <summary>
        /// Click de los elementos de archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            //Gestionamos el evento de los click de los botones mediande un switch ya que son muchos
            switch (btn.Name)
            {
                case "btnNuevo":
                    NuevoDoc win = new NuevoDoc();
                    win.ShowDialog();
                    if (win.DialogResult == true)
                    {

                        //pasamos de color a brush
                        SolidColorBrush brush = new SolidColorBrush(win.colorCanvas);
                        lienzo.Strokes.Clear();
                        lienzo.Background = brush;
                        lienzo.Height = win.altoCanvas;
                        lienzo.Width = win.anchoCanvas;
                        lblInfo.Content = "Nombre del documento: " + win.nombreCanvas;

                    }
                    break;
                case "btnGuardar":
                    archi.CrearImagenInkCanvas(this.lienzo, "D:/Users/Usuario/Desktop/logo.png");
                    break;
                case "btnAbrir":
                    ImageBrush imagen = archi.CargarImagenIncKanvas();
                    lienzo.Strokes.Clear();
                    lienzo.Height = imagen.ImageSource.Height;
                    lienzo.Width = imagen.ImageSource.Height;
                    lienzo.Background = imagen;
                    break;
                default:
                    break;
            }
          

        }

        //CANVAS_____________________________________________________________________

      

        private void lienzo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dibujar = true;
        }

        private void lienzo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dibujar = false;
        }

        //****************************************** PRUEBAS *******************************************************

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

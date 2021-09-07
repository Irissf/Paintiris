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
using System.Windows.Ink;
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
        public bool borrar;

        //Clase gestión pinceles
        Pinceles pinceles;


        //Clase gestión de los archivos
        Archivo archi;

        //**************************************** CONSTRUCTOR ****************************************************

        public MainWindow()
        {
            InitializeComponent();
            lienzo = lienzoPorDefecto;
            lienzo.EditingMode = InkCanvasEditingMode.None;

            //inicializar clases
            archi = new Archivo(lienzo);
            pinceles = new Pinceles();
        }

        //**************************************** EVENTOS DE COMPONENTES *******************************************

        //BOTONES__________________________________________________________________

        /// <summary>
        /// Click de los elementos de archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Archivo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            //Gestionamos el evento de los click de los botones mediande un switch ya que son muchos
            switch (btn.Name)
            {
                case "btnNuevo":
                    NuevoDoc nuevoCanvas = new NuevoDoc();
                    nuevoCanvas.ShowDialog();
                    if (nuevoCanvas.DialogResult == true)
                    {

                        //pasamos de color a brush
                        SolidColorBrush brush = new SolidColorBrush(nuevoCanvas.colorCanvas);
                        lienzo.Strokes.Clear();
                        lienzo.Background = brush;
                        lienzo.Height = nuevoCanvas.altoCanvas;
                        lienzo.Width = nuevoCanvas.anchoCanvas;
                        lblInfo.Content = "Nombre del documento: " + nuevoCanvas.nombreCanvas;

                    }
                    break;
                case "btnGuardar":
                    archi.GuardarImagenInkCanvas(lienzo,"D:/Users/Usuario/Desktop/logo1.png");
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

        private void HerramientasDibujo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btn_dibujar":
                    lienzo.EditingMode = InkCanvasEditingMode.Ink;
                    //TODO pasarle las variables de las carácteristicas que elija la persona
                    lienzo.DefaultDrawingAttributes = pinceles.PintarPincel(5,5, Color.FromArgb(255, 25, 47, 255));
                    break;
                case "btn_borrar":
                    Trace.WriteLine("borrar");
                    lienzo.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    //propiedades de la goma
                    lienzo.EraserShape = new EllipseStylusShape(1, 1);
                    break;
                case "btn_seleccionar":
                    break;
                default:
                    break;
            }
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

using Paintiris.Inicio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        SolidColorBrush colorPintar = new SolidColorBrush(Color.FromArgb(255,0,0,0));
        Color colorPin;

        //Clase gestión pinceles
        Pinceles pinceles;


        //Clase gestión de los archivos
        Archivo archi;

        //Para guardar por grupos los ToggleButton
        List<ToggleButton> prueba = new List<ToggleButton>();
        List<ToggleButton> pincelTamano = new List<ToggleButton>();
        List<ToggleButton> pincelColor = new List<ToggleButton>();
        List<ToggleButton> herramienta = new List<ToggleButton>();

        //**************************************** CONSTRUCTOR ****************************************************

        public MainWindow()
        {
            InitializeComponent();
            lienzo = lienzoPorDefecto;
            lienzo.EditingMode = InkCanvasEditingMode.None;

            //inicializar clases
            archi = new Archivo(lienzo);
            pinceles = new Pinceles(this);

            //llenamos colecciones
            prueba.Add(tbtn_borrar);
            prueba.Add(tbtn_pintar);
            prueba.Add(tbtn_selec);

            //ponemos color al inicio del pincel
            colorPin = Color.FromArgb(255, 0, 0, 0);
            cv_colorPintar.Background = colorPintar;

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
                    archi.GuardarImagenInkCanvas(lienzo, "D:/Users/Usuario/Desktop/logo1.png");
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



        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //le mandamos al dialog el color que tenemos en el canvas al inicio
            ColorDialog miColor = new ColorDialog(colorPintar.Color);
            miColor.ShowDialog();
            if (miColor.DialogResult == true)
            {
                colorPintar = new SolidColorBrush(miColor.color);
                cv_colorPintar.Background = colorPintar;
                //si el pincel está seleccionado, volvemos a crearlo o no se actualizará el nuevo color
                if (tbtn_pintar.IsChecked == true)
                {
                    lienzo.EditingMode = InkCanvasEditingMode.Ink;
                    lienzo.DefaultDrawingAttributes = pinceles.PintarPincel(5, 5, colorPintar.Color);
                }
            }
        }

        //prueba a botonoes como radio button
        private void tbs_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tbtn = (ToggleButton)sender;
            // si le da a uno los otros los desmarcamos
            foreach (ToggleButton boton in prueba)
            {
                if (boton.Name != tbtn.Name)
                {
                    boton.IsChecked = false;
                }
            }

            switch (tbtn.Name)
            {
                case "tbtn_pintar":
                    lienzo.EditingMode = InkCanvasEditingMode.Ink;
                    lienzo.DefaultDrawingAttributes = pinceles.PintarPincel(5, 5, colorPintar.Color);
                    break;
                case "tbtn_borrar":
                    Trace.WriteLine("borrar");
                    lienzo.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    //propiedades de la goma
                    lienzo.EraserShape = new EllipseStylusShape(1, 1);
                    break;
                case "tbtn_selec":
                    lienzo.EditingMode = InkCanvasEditingMode.Select;
                    break;

                default:
                    break;
            }
        }
    }


}


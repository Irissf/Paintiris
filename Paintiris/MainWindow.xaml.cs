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

        //********************************************************************************************
                    /*  ___  ____ ____ ___  _ ____ ___  ____ ___  ____ ____ 
                        |__] |__/ |  | |__] | |___ |  \ |__| |  \ |___ [__  
                        |    |  \ |__| |    | |___ |__/ |  | |__/ |___ ___] */

        //********************************************************************************************

        //Control del dibujo por parte de la clase pincel
        public InkCanvas lienzo; //el area de dibujo
        public bool siPincel; // si pincel está activado, dividimos el alto entre dos para el ancho 
        public bool pintarActivado; //marcamos si está activado un pincel para el cambio de color
        int altoPincel = 2; //alto del pincel
        int anchoPincel = 2; //ancho del pincel

        //Gestionar el color
        SolidColorBrush colorPintar = new SolidColorBrush(Color.FromArgb(255,0,0,0));
        Rectangle paraQuitarBorde;
        bool primerRectangleGuardado = false;

        //Clase gestión pinceles
        Pinceles pinceles;

        //Clase gestión de los archivos
        Archivo archi;

        //Para guardar por grupos los ToggleButton
        List<ToggleButton> prueba = new List<ToggleButton>();
        List<ToggleButton> pincelTamano = new List<ToggleButton>();
        List<ToggleButton> pincelColor = new List<ToggleButton>();
        List<ToggleButton> herramienta = new List<ToggleButton>();

        //********************************************************************************************
                    /*  ____ ____ _  _ ____ ___ ____ _  _ ____ ___ ____ ____ 
                        |    |  | |\ | [__   |  |__/ |  | |     |  |  | |__/ 
                        |___ |__| | \| ___]  |  |  \ |__| |___  |  |__| |  \ */

        //********************************************************************************************
        public MainWindow()
        {
            InitializeComponent();
            lienzo = lienzoPorDefecto;
            lienzo.EditingMode = InkCanvasEditingMode.None;

            //inicializar clases
            archi = new Archivo(lienzo);
            pinceles = new Pinceles(this);

            //llenamos colecciones para que al activar uno se desactiven todos
            LlenarColecciones();


        }

        //********************************************************************************************
        /*  ____ _  _ ____ _  _ ___ ____ ____    ___  ____    ____ ____ _  _ ___  ____ _  _ ____ _  _ ___ ____ 
            |___ |  | |___ |\ |  |  |  | [__     |  \ |___    |    |  | |\/| |__] |  | |\ | |___ |\ |  |  |___ 
            |___  \/  |___ | \|  |  |__| ___]    |__/ |___    |___ |__| |  | |    |__| | \| |___ | \|  |  |___  */

        //********************************************************************************************

        //BOTONES GESTIÓN__________________________________________________________________

        /// <summary>
        /// Click de los elementos de archivo => NUEVO, GUARDAR, GUARDARCOMO, ABRIR
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

        //BOTONES COLOR__________________________________________________________________

        /// <summary>
        /// Con el botón derecho del ratón cambiamos el color que se encuentra en ese recuadro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CambioColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas color = (Canvas)sender;

            //le mandamos al dialog el color que tenemos en el canvas al inicio
            ColorDialog miColor = new ColorDialog(colorPintar.Color);
            miColor.ShowDialog();

            if (miColor.DialogResult == true)
            {
                colorPintar = new SolidColorBrush(miColor.color);
                color.Background = colorPintar;
                Pintar();
            }
        }

        /// <summary>
        /// Al pulsar con el botón izquierdo del ratón elegimos el color con el que 
        /// queremos pintar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElegirColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rec = (Rectangle)sender;
            if (primerRectangleGuardado)
            {
                paraQuitarBorde.StrokeThickness = 0;
            }

            rec.Stroke = new SolidColorBrush(Colors.Black);
            rec.StrokeThickness = 2;


            paraQuitarBorde = (Rectangle)sender;
            primerRectangleGuardado = true;

            //si el pincel está seleccionado, volvemos a crearlo o no se actualizará el nuevo color
            if (pintarActivado)
            {
                //hacemos esta conversión para poder meter el color del canvas en el pincel, primero lo pasamos a brush y luego a colorSolidBrush
                Brush colorDelCanvas = rec.Fill;
                colorPintar = (SolidColorBrush)colorDelCanvas;

                Trace.WriteLine("entro");
                Pintar();
            }
        }



        //prueba a botones como radio button
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
                case "tbtn_borrar":
                    Trace.WriteLine("borrar");
                    foreach (ToggleButton boton in pincelTamano)
                    {
                        boton.IsEnabled = false;
                    }
                    lienzo.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    //propiedades de la goma
                    lienzo.EraserShape = new EllipseStylusShape(1, 1);
                    pintarActivado = false;
                    break;
                case "tbtn_selec":
                    lienzo.EditingMode = InkCanvasEditingMode.Select;
                    pintarActivado = false;
                    break;

                default:
                    break;
            }
        }

        //TAMAÑO PINCEL__________________________________________________________________
        
        /// <summary>
        /// Determinamos el tamño del pincel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pintar_checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tbtn = (ToggleButton)sender;
           

            foreach (ToggleButton boton in pincelTamano)
            {
                if (boton.Name != tbtn.Name)
                {
                    boton.IsChecked = false;
                }
            }
            altoPincel = Convert.ToInt16(tbtn.Tag);

            pintarActivado = true;

            //si está marcado que es un pincel, dividimos el alto entre dos para hacerlo de forma ovalada
            Trace.WriteLine(siPincel);
            

            Pintar();
        }

        /// <summary>
        /// Elegimos si queremos lápiz, igual ancho que alto o el pincel
        /// cuyo ancho será aproximadamente la mitad del alto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pincelOlapiz_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tbtn = (ToggleButton)sender;

            //activamos los tamaños
            foreach (ToggleButton boton in pincelTamano)
            {
                
                boton.IsEnabled = true;
            }

            // si le da a uno los otros los desmarcamos
            ActivasDesactivar(tbtn, herramienta);

            //activamos que estamos dibujando
            pintarActivado = true;

            if (tbtn.Name == "tbtn_pincel")
            {

                siPincel = true;
            }
            else
            {
               siPincel = false;
            }
           
            Pintar();

        }

        //********************************************************************************************
                        /*  ____ _  _ _  _ ____ _ ____ _  _ ____ ____ 
                            |___ |  | |\ | |    | |  | |\ | |___ [__  
                            |    |__| | \| |___ | |__| | \| |___ ___]  */

        //********************************************************************************************

        //trasladarlas luego
        //LLENAR Colecciones__________________________________________________________________
        private void LlenarColecciones()
        {
            prueba.Add(tbtn_borrar);
            prueba.Add(tbtn_selec);

            pincelTamano.Add(tbtn_pincel1px);
            pincelTamano.Add(tbtn_pincel3px);
            pincelTamano.Add(tbtn_pincel6px);
            pincelTamano.Add(tbtn_pincel10px);
            pincelTamano.Add(tbtn_pincel13px);

            herramienta.Add(tbtn_lapiz);
            herramienta.Add(tbtn_pincel);
        }

        /// <summary>
        /// Funciona como un Refresh() cada vez que hacemos un cambio en color o forma llamamos a la función para 
        /// evitar repetir código
        /// </summary>
        private void Pintar()
        {
            if (siPincel)
            {
                anchoPincel = altoPincel / 2;
                if (anchoPincel < 1)
                {
                    anchoPincel = 1;
                }
            }
            else
            {

                anchoPincel = altoPincel;
            }

            lienzo.EditingMode = InkCanvasEditingMode.Ink;
            
            lienzo.DefaultDrawingAttributes = pinceles.PintarPincel(altoPincel, anchoPincel, colorPintar.Color);
        }

        private void ActivasDesactivar(ToggleButton boton, List<ToggleButton> lista)
        {
            foreach (ToggleButton botonElemento in herramienta)
            {
                if (botonElemento.Name != boton.Name)
                {
                    botonElemento.IsChecked = false;
                }
            }
        }

    }


}


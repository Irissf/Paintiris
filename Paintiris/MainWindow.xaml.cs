

using Paintiris.Clases;
using Paintiris.Inicio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Paintiris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region PROPIEDADES

        //Control del dibujo por parte de la clase pincel
        public InkCanvas lienzo; //el area de dibujo
        public bool siPincel; // si pincel está activado, dividimos el alto entre dos para el ancho 
        public bool pintarActivado; //marcamos si está activado un pincel para el cambio de color
        int altoPincel = 2; //alto del pincel
        int anchoPincel = 2; //ancho del pincel
        int gomaTam = 2;

        //Gestionar el color
        SolidColorBrush colorPintar;
        Rectangle paraQuitarBorde;
        bool primerRectangleGuardado = false;

        //Clase gestión pinceles
        Pinceles pinceles;

        //Clase gestión de los archivos
        Archivo archi;

        //Para guardar por grupos los ToggleButton
        List<ToggleButton> pincelTamano = new List<ToggleButton>();
        List<ToggleButton> herramienta = new List<ToggleButton>();
        List<ToggleButton> gomaTamano = new List<ToggleButton>();



        //para las paletas de colores prueba
        List<Rectangle> cuadrosColores = new List<Rectangle>();
        Conversor transfromar = new Conversor();

        #endregion

        //CONSTRUCTOR
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
            cb_palette.SelectedIndex = 0;

            //Indicamos el color del pincel hasta que se seleccione otro color
            colorPintar = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            //zoom
            zoom.Value = 1;
        }

        #region EVENTOS COMPONENTE

        //BOTONES GESTIÓN__________________________________________________________________

       

        #region BOTONES COLOR

        /// <summary>
        /// Con el botón derecho del ratón cambiamos el color que se encuentra en ese recuadro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CambioColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle color = (Rectangle)sender;

            //le mandamos al dialog el color que tenemos en el canvas al inicio
            ColorDialog miColor = new ColorDialog(colorPintar.Color);
            miColor.ShowDialog();

            if (miColor.DialogResult == true)
            {
                colorPintar = new SolidColorBrush(miColor.color);
                color.Fill = colorPintar;
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

            //si ya se guardó un componente, le ponemos el borde a 0 para que no se vea
            //De esta manera conseguimos el efecto radioButton en los Rectangle
            if (primerRectangleGuardado)
            {
                paraQuitarBorde.StrokeThickness = 0;
            }

            //le ponemos un borde al Rectangle seleccionado
            rec.Stroke = new SolidColorBrush(Colors.Black);
            rec.StrokeThickness = 2;

            //guardamos el componente actual, para al dar a otro, quitarle el borde a este
            //y ponemos a true la booleana para que no de un error
            paraQuitarBorde = (Rectangle)sender;
            primerRectangleGuardado = true;

            //hacemos esta conversión para poder meter el color del canvas en el pincel, primero lo pasamos a brush y luego a colorSolidBrush
            Brush colorDelRect = rec.Fill;
            colorPintar = (SolidColorBrush)colorDelRect;

            //si el pincel está seleccionado, volvemos a crearlo o no se actualizará el nuevo color
            if (pintarActivado)
            {

                Trace.WriteLine("entro");
                Pintar();
            }
        }



        /// <summary>
        /// BORRAR Y SELECCIONAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbs_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tbtn = (ToggleButton)sender;

            // si le da a uno los otros los desmarcamos
            ActivasDesactivar(tbtn);

            switch (tbtn.Name)
            {
                case "tbtn_borrar":
                    Trace.WriteLine("borrar");
                    txtGoma.Text = ""+gomaTam;
                    //desactivamos los tamños de pintar
                    foreach (ToggleButton boton in pincelTamano)
                    {
                        boton.IsEnabled = false;
                    }

                    //activamos los taños de la goma
                    foreach (ToggleButton tamano in gomaTamano)
                    {
                        tamano.IsEnabled = true;
                    }

                    Borrar();
                    break;
                case "tbtn_selec":
                    lienzo.EditingMode = InkCanvasEditingMode.Select;
                    pintarActivado = false;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region TAMAÑO PINCEL

        /// <summary>
        /// Determinamos el tamño del pincel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tamanoPincel_checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tbtn = (ToggleButton)sender;

            //cogemos el tamño del pincel del tag del mismo
            altoPincel = Convert.ToInt16(tbtn.Tag);

            if (!pintarActivado)
            {
                //si está la goma activada
                foreach (ToggleButton boton in gomaTamano)
                {
                    if (boton.Name != tbtn.Name)
                    {
                        boton.IsChecked = false;
                    }
                }
                txtGoma.Text = ""+tbtn.Tag;
                Borrar();
            }
            else
            {

                //recorremos el array de los tamaños del pincel
                foreach (ToggleButton boton in pincelTamano)
                {
                    if (boton.Name != tbtn.Name)
                    {
                        boton.IsChecked = false;
                    }
                }

                txtTamanoPincel.Text = "" + tbtn.Tag; //y lo mostramos en el textbox del tamaño
                //pintarActivado = true;

                //si está marcado que es un pincel, dividimos el alto entre dos para hacerlo de forma ovalada
                Trace.WriteLine(siPincel);

                Pintar();
            }



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

            //desactivamos los tamaños de la goma
            foreach (ToggleButton tamano in gomaTamano)
            {
                tamano.IsEnabled = false;
            }

            // si le da a uno los otros los desmarcamos
            ActivasDesactivar(tbtn);

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

        /// <summary>
        /// El textbox del tamaño de los pinceles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            try
            {
                int combrobarNum = Convert.ToInt32(txt.Text);
                if (combrobarNum > 0)
                {

                    if (pintarActivado)
                    {
                        altoPincel = Convert.ToInt16(txt.Text);
                        Pintar();
                    }
                    else
                    {
                        gomaTam = Convert.ToInt16(txt.Text);
                        Borrar();
                    }
                }
            }
            catch (FormatException)
            {
                altoPincel = 1;
            }

        }
        #endregion

        #region OTRAS

        /// <summary>
        /// Cambiar la paleta de colores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_palette_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string seleccionado = cb_palette.SelectedValue.ToString();
            BaseDatos bd = new BaseDatos();
            bd.Conexion();
            List<string> colores = bd.CogerColores(seleccionado);
            if (colores.Count > 0)
            {
                for (int i = 0; i < cuadrosColores.Count; i++)
                {
                    int[] rgb = transfromar.generarGRBA(colores[i]);
                    colorPintar = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rgb[0]), Convert.ToByte(rgb[1]), Convert.ToByte(rgb[2])));
                    cuadrosColores[i].Fill = colorPintar;
                }
            }
            bd.CerrarConexion();
        }


        /// <summary>
        /// Folios de estudio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image tgb = (Image)sender;
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\..\Recursos\folios\" + tgb.Tag, UriKind.Relative));

            lienzo.Strokes.Clear();
            lienzo.Height = image.ImageSource.Height;
            lienzo.Width = image.ImageSource.Width;
            lienzo.Background = image;

        }

     

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
                    archi.GuardarImagenInkCanvas(lienzo);
                    break;
                case "btnGuardarComo":
                    archi.ElegirRuta();
                    goto case "btnGuardar";
                case "btnAbrir":
                    ImageBrush imagen = archi.CargarImagenIncKanvas();
                    //controlamos que venga una imagen, ya que si no lo hacemos, nos sale un nullreferenceexception
                    if (imagen.ImageSource != null)
                    {
                        lienzo.Strokes.Clear();
                        lienzo.Height = imagen.ImageSource.Height;
                        lienzo.Width = imagen.ImageSource.Height;
                        lienzo.Background = imagen;
                    }


                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// slider para cambiar el zoom del lienzo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged_ZOOM(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ponemos un tope del mínimo del zoom
            if(zoom.Value > 0.5)
            {
                ScaleTransform scaler = lienzo.LayoutTransform as ScaleTransform;
                lienzo.LayoutTransform = new ScaleTransform(zoom.Value, zoom.Value);
            }
  
        }

        /// <summary>
        /// Zoom volver a tamaño original
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbs_Zoom(object sender, RoutedEventArgs e)
        {
            ScaleTransform scaler = lienzo.LayoutTransform as ScaleTransform;
            lienzo.LayoutTransform = new ScaleTransform(1, 1);
        }
        #endregion

        #endregion


        #region FUNCIONES

        //LLENAR Colecciones__________________________________________________________________
        private void LlenarColecciones()
        {

            pincelTamano.Add(tbtn_pincel1px);
            pincelTamano.Add(tbtn_pincel3px);
            pincelTamano.Add(tbtn_pincel6px);
            pincelTamano.Add(tbtn_pincel10px);
            pincelTamano.Add(tbtn_pincel13px);

            gomaTamano.Add(tbtn_goma1px);
            gomaTamano.Add(tbtn_goma3px);
            gomaTamano.Add(tbtn_gomal6px);
            gomaTamano.Add(tbtn_goma10px);
            gomaTamano.Add(tbtn_goma13px);

            herramienta.Add(tbtn_lapiz);
            herramienta.Add(tbtn_pincel);
            herramienta.Add(tbtn_borrar);
            herramienta.Add(tbtn_selec);

            cb_palette.Items.Add("Basic");
            cb_palette.Items.Add("Dark Academia");
            cb_palette.Items.Add("Sailor Moon");
            cb_palette.Items.Add("Desert");
            cb_palette.Items.Add("Primavera Fria");
            cb_palette.Items.Add("Otoño tranquilo");
            cb_palette.Items.Add("Cold Day");

            cuadrosColores.Add(paleta1);
            cuadrosColores.Add(paleta2);
            cuadrosColores.Add(paleta3);
            cuadrosColores.Add(paleta4);
            cuadrosColores.Add(paleta5);
            cuadrosColores.Add(paleta6);
            cuadrosColores.Add(paleta7);
            cuadrosColores.Add(paleta8);
            cuadrosColores.Add(paleta9);
            cuadrosColores.Add(paleta10);
            cuadrosColores.Add(paleta11);
            cuadrosColores.Add(paleta12);


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

        /// <summary>
        /// Para desactivar los tooglebutton que no está seleccionados y generar efecto radioBox
        /// </summary>
        /// <param name="boton"></param>
        private void ActivasDesactivar(ToggleButton boton)
        {
            foreach (ToggleButton botonElemento in herramienta)
            {
                if (botonElemento.Name != boton.Name)
                {
                    botonElemento.IsChecked = false;
                }
            }
        }

        private void Borrar()
        {
            //ponemos a none, ya que la forma del pincel no se actualiza hasta el cambio de editar del inkCanvas
            lienzo.EditingMode = InkCanvasEditingMode.None;

            //activamos la goma
            pintarActivado = false;
            lienzo.EraserShape = new EllipseStylusShape(gomaTam, gomaTam); //le damos el tamaño
            lienzo.EditingMode = InkCanvasEditingMode.EraseByPoint; //poenemos el cambas en modo de goma
            //propiedades de la goma
        }

       
    }
    #endregion
    

}


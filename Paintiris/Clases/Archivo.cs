using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paintiris.Inicio
{
    class Archivo
    {
        InkCanvas lienzo;
        int alto;
        int ancho;
        string rutaGuardado = "";
        bool esJpg = true;



        public Archivo(InkCanvas lienzo)
        {
            this.lienzo = lienzo;
            this.alto = (int)this.lienzo.RenderSize.Height;
            this.ancho = (int)this.lienzo.RenderSize.Width;


        }

        //******************************* GUARDAR **********************************

        /*En el campo de la impresión, "DPI" es la abreviatura de "Puntos por pulgada".
        Este valor se refiere al número de puntos que se imprimen por pulgada. */
        private double dpi = 96.0;

        /// <summary>
        /// Elegimos la ruta donde queremos guardar el dibujo
        /// </summary>
        public bool ElegirRuta()
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                this.rutaGuardado = saveFileDialog.FileName;

                //vemos si es jpg o png pues tendrá un guardado diferente
                if (saveFileDialog.Filter.Equals("jpg"))
                {
                    esJpg = true;
                }
                else
                {
                    esJpg = false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Función para guardar un archivo
        /// </summary>
        /// <param name="canvas"></param>
        public void GuardarImagenInkCanvas(InkCanvas canvas)
        {
            //Preguntamos al usuario si quiere sobreescribir, lo pongo siempre para evitar ese guardado sin querer
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres guardar el archivo?", "Guardado", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //RenderTargetBitmap Convierte un objeto Visual en un mapa de bits.
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)canvas.ActualWidth, (int)canvas.ActualHeight,
                 dpi, dpi, PixelFormats.Default);

                renderBitmap.Render(ModificarZonaVisualImagen(canvas));
                try
                {
                    using (FileStream file = new FileStream(rutaGuardado, FileMode.Create))
                    {
                        if (esJpg)
                        {
                            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                            encoder.Save(file);
                        }
                        else
                        {
                            PngBitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                            encoder.Save(file);
                        }

                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// Función para cargar una imagen del equipo
        /// </summary>
        /// <returns></returns>
        public ImageBrush CargarImagenIncKanvas()
        {
            ImageBrush image = new ImageBrush();

            OpenFileDialog imagenCargar = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg)|*.jpg|Image Files (*.png)|*.png",
                Title = "Abrir imagen"
            };

            if (imagenCargar.ShowDialog() == true)
            {
                rutaGuardado = imagenCargar.FileName;
                /*Como se mencionó, una ImageBrush pinta un área con un ImageSource .
                 * El tipo más común de ImageSource que se utiliza con ImageBrush es un BitmapImage */
                //BitmapImage imagen = new BitmapImage(new Uri(imagenCargar.FileName, UriKind.Relative));

                BitmapImage imagen = new BitmapImage();

                //Lo que hacemos a continuación, es para evitar el error de no poder guardar por usar el mismo proceso
                imagen.BeginInit();
                imagen.CacheOption = BitmapCacheOption.OnLoad;
                imagen.UriSource = new Uri(imagenCargar.FileName);
                imagen.EndInit();
                imagen.Freeze();

                image.ImageSource = imagen;
            }
            return image;
        }


        /// <summary>
        /// Es necesario para poder guardar la imagen entera, puesto que wpf por defecto cuando le pasamos
        /// al RangerTargetBitmap el tamaño del mismo, este toma como referencia al padre donde está
        /// el inkCanvas por lo que guarda una captura de pantalla sonse igual solo sale un cacho del inkcanvas
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private DrawingVisual ModificarZonaVisualImagen(Visual v)
        {
            Rect b = VisualTreeHelper.GetDescendantBounds(v);
            /// new a drawing visual and get its context
            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            /// generate a visual brush by input, and paint
            VisualBrush vb = new VisualBrush(v);
            dc.DrawRectangle(vb, null, b);
            dc.Close();

            return dv;
        }


    }
}

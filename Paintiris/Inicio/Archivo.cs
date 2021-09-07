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

        public void GuardarImagenInkCanvas(InkCanvas canvas, string ruta)
        {
            

            //RenderTargetBitmap Convierte un objeto Visual en un mapa de bits.
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
            (int)canvas.ActualWidth, (int)canvas.ActualHeight,
             dpi, dpi, PixelFormats.Default);
            //el fallo es que captura todo el programa, la zona que le digo, tengo que dirigirme hasta la zona......


            renderBitmap.Render(ModificarZonaVisualImagen(canvas));

            using (FileStream file = new FileStream(ruta, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                //PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(file);
            }

        }

        public ImageBrush CargarImagenIncKanvas()
        {
            ImageBrush image = new ImageBrush(); 

            OpenFileDialog imagenCargar = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg)|*.jpg|Image Files (*.png)|*.png|Image Files (*.bmp)|*.bmp",
                Title = "Abrir imagen"
            };

            if (imagenCargar.ShowDialog() == true)
            {
                /*Como se mencionó, una ImageBrush pinta un área con un ImageSource .
                 * El tipo más común de ImageSource que se utiliza con ImageBrush es un BitmapImage */
                image.ImageSource = new BitmapImage(new Uri(imagenCargar.FileName, UriKind.Relative));
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

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
        private int ancho;
        private int alto;

        public Archivo(InkCanvas lienzo)
        {
            this.lienzo = lienzo;
            this.alto = (int)this.lienzo.RenderSize.Height;
            this.ancho = (int)this.lienzo.RenderSize.Width;


        }
        //******************************* GUARDAR **********************************

        /*En el campo de la impresión, "DPI" es la abreviatura de "Puntos por pulgada".
        Este valor se refiere al número de puntos que se imprimen por pulgada. */
        private float dpi = 96.0f;

        public void CrearImagenInkCanvas(InkCanvas canvas, string filename)
        {
            //RenderTargetBitmap Convierte un objeto Visual en un mapa de bits.
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             dpi, dpi, PixelFormats.Pbgra32);

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }

        }

        public ImageBrush CargarImagenIncKanvas()
        {
            InkCanvas lienzo = new InkCanvas();
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


    }
}

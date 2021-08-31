using System;
using System.Collections.Generic;
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
        Canvas lienzo;
        private int ancho;
        private int alto;
        Image imagen;
        public Archivo(Canvas lienzo)
        {
            this.lienzo = lienzo;
            this.alto = (int)this.lienzo.RenderSize.Height;
            this.ancho = (int)this.lienzo.RenderSize.Width;
            CreateSaveBitmap(this.lienzo, "D:/Users/Usuario/Desktop/logo.png");

        }
        //******************************* GUARDAR **********************************

        /*En el campo de la impresión, "DPI" es la abreviatura de "Puntos por pulgada".
        Este valor se refiere al número de puntos que se imprimen por pulgada. */
        private float dpi = 96.0f;

        private void CreateSaveBitmap(Canvas canvas, string filename)
        {
            //RenderTargetBitmap Convierte un objeto Visual en un mapa de bits.
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             96d, 96d, PixelFormats.Pbgra32);

             
            //necesario, de lo contrario, la salida de la imagen es negra
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }
    


    }
}

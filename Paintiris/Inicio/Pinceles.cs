using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Windows.Input;

namespace Paintiris.Inicio
{
    class Pinceles
    {
        MainWindow win;
        public Pinceles(MainWindow win)
        {
            this.win = win;
        }
        
        public DrawingAttributes PintarPincel(int alto, int ancho, Color color )
        {
            DrawingAttributes inkDA = new DrawingAttributes();
            inkDA.Width = ancho;
            inkDA.Height = alto;
            inkDA.Color = color;
            return inkDA;
            //inkCanvas.DefaultDrawingAttributes = inkDA;
        }

        public Rectangle Rectangulo()
        {
            Rectangle myRect = new Rectangle();
            myRect.Stroke = Brushes.Black;
            //myRect.Fill = Brushes.SkyBlue;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = 150;
            myRect.Width = 150;
            return myRect;
        }

        // prueba arrastrar rectángulo
        

    }
}

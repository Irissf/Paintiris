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
        public DrawingAttributes PintarPincel(int alto, int ancho, Color color )
        {
            DrawingAttributes inkDA = new DrawingAttributes();
            inkDA.Width = ancho;
            inkDA.Height = alto;
            inkDA.Color = color;
            return inkDA;
            //inkCanvas.DefaultDrawingAttributes = inkDA;
        }

       
    }
}

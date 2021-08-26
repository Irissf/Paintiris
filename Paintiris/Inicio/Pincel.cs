using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paintiris.Inicio
{
    class Pincel
    {
        MainWindow main;

        //Ajustes del pincel
        private int diametro = 5;
        private Brush color = Brushes.Black;

        public Pincel(MainWindow main)
        {
            //TODO en un futuo le llegarán otras propiedades como serian el tamaño color etc
            this.main = main;
        }

        public void CrearEllipsePincel(Point position)
        {
            Ellipse nuevaElipse = new Ellipse();

            nuevaElipse.Fill = color;
            nuevaElipse.Width = diametro;
            nuevaElipse.Height = diametro;

            Canvas.SetTop(nuevaElipse,position.Y);
            Canvas.SetLeft(nuevaElipse, position.X);

            main.lienzo.Children.Add(nuevaElipse);
        }
    }
}

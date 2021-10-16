using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paintiris.Clases
{
    class Conversor
    {

        public Conversor()
        {
            //generateGRBA("E08F62");
            //Console.WriteLine("#" + generateHEX(224) + "" + generateHEX(143) + "" + generateHEX(98));
        }

        public int[] generarGRBA(string colorHex)
        {
            int[] rgb = new int[3];
            //tengo que añadirle el #
            string colo = "#" + colorHex.Trim();
            Color color;
            try
            {
                color = (Color)ColorTranslator.FromHtml(colo); //FFE699
                                                               //pasamos de hex a rgb
                rgb[0] = Convert.ToInt16(color.R);
                rgb[1] = Convert.ToInt16(color.G);
                rgb[2] = Convert.ToInt16(color.B);
            }
            catch
            {

            }
            return rgb;
        }

        public string generaHEX(int numero)
        {
            double residuo = 0;
            string resultado = "0";
            do
            {
                residuo = ((double)(numero)) / 16; /*realizamos la división de rojo entre 16 y guardamos el residuo en "residuo". 
                                               * (Como queremos obtener números con punto decimal, tenemos que realizar la conversión de "número"
                                               *  a double, ya que, si dividimos sin hacer esto, la división solo nos dará la parte entera)*/
                numero = numero / 16;
                residuo = (residuo - numero) * 16;/*a "residuo" le restamos "numero", lo que nos dará los dígitos después del punto de la
                                                 * variable "residuo" ya que le estamos restando su parte entera y este resultado lo multiplicamos por 16*/
                switch (residuo)
                {
                    case 10:
                        resultado = "A" + resultado; // si el dígito a añadir es 10, ponemos A, su correspondiente en hexadecimal
                        break;
                    case 11:
                        resultado = "B" + resultado;
                        break;
                    case 12:
                        resultado = "C" + resultado;
                        break;
                    case 13:
                        resultado = "D" + resultado;
                        break;
                    case 14:
                        resultado = "E" + resultado;
                        break;
                    case 15:
                        resultado = "F" + resultado;
                        break;
                    default:
                        resultado = residuo + resultado;
                        break;
                }

            } while (numero != 0);
            return resultado;
        }

    
    }
}

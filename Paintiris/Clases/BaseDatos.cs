using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Windows;

namespace Paintiris.Clases
{
    class BaseDatos
    {
        //atributos string de las tablas
        public string nombreColor;  //colores
        public string nombrePaleta; //paletas
        public string codigoHex;    //colores

        //atributos int de las tablas
        public int rojo;    //colores
        public int verde;   //colores
        public int azul;    //colores
        public int IdPaleta;//paletas

        public void Conexion()
        {
            //nos coge la cadena de conexión que está en app.config con el nombre de cadena
            string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            SQLiteConnection connexion = new SQLiteConnection(cadena);

            try
            {
                connexion.Open();
                MessageBox.Show("conectado");

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
    }
}

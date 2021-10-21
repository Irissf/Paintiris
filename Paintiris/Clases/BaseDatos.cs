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

        SQLiteConnection connexion;

        public void Conexion()
        {
            //nos coge la cadena de conexión que está en app.config con el nombre de cadena
            string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
            connexion = new SQLiteConnection(cadena);

            try
            {
                connexion.Open();
                //MessageBox.Show("conectado");
                CogerColores("");

            }
            catch (Exception e)
            {

                MessageBox.Show("error "+e.Message);
            }

        }

        //select colorHex from paletas_colores where paletaId = (select id from Paletas where nombre like 'Dark Academia')
        public List<string> CogerColores(string paleta)
        {
            MessageBox.Show(paleta);
            string consulta = string.Format("select colorHex from paletas_colores where paletaId = (select id from Paletas where nombre = '{0}');",paleta);
            List<string> colores = new List<string>();
            try
            {
                using (SQLiteCommand fmd = new SQLiteCommand(connexion))
                {
                    fmd.CommandText = consulta;//"select colorHex from paletas_colores where paletaId = (select id from Paletas where nombre = 'Dark Academia');";
                    using (SQLiteDataReader dr = fmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            colores.Add(dr.GetString(0));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            MessageBox.Show("tamaño de "+paleta +" "+colores.Count);
            return colores;
        }

        public void CerrarConexion()
        {
            connexion.Close();
        }
    }
}

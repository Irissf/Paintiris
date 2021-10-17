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
                MessageBox.Show("conectado");
                CogerColores("");

            }
            catch (Exception e)
            {

                MessageBox.Show("error "+e.Message);
            }

        }

        public List<string> CogerColores(string paleta)
        {
            MessageBox.Show("consulta");
            List<string> colores = new List<string>();
            //tendrá que ser un innerjoin, para coger los colores que tienen el nombre de la tabla que pasamos???
            using (SQLiteCommand fmd = new SQLiteCommand(connexion))
            {
                fmd.CommandText = @"select colorHex from paletas_colores;";
                using (SQLiteDataReader dr = fmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //MessageBox.Show("color"+dr.GetString(0));
                        colores.Add(dr.GetString(0));
                    }
                }
            }
            return colores;
        }

        public void CerrarConexion()
        {
            connexion.Close();
        }
    }
}

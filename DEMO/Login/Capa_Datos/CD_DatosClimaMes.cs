﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace Capa_Datos
{
    public class CD_DatosClimaMes
    {
        private CD_ConexionBD conexion = new CD_ConexionBD();
        MySqlCommand comando = new MySqlCommand();
        DataTable tablaDatosClimaMes = new DataTable();
        DataTable tablaDatosClima = new DataTable();
        MySqlDataReader leer;

        public void InsertarDatosClimaMes(String Estacion,String Fecha_Local, String Direccion_de_Viento, String Direccion_de_Rafaga,
            String Rapidez_de_Viento, String Rapidez_de_Rafaga, String Temperatura, String Humedad_Relativa, String Presion_Atmosferica,
            String Precipitacion, String Radiacion_Solar)
        {
            float direccionViento, direccionRafaga, rapidezViento, rapidezRafaga, temperatura, humedadRelativa, presionAtmosferica,precipitacion;
            direccionViento = float.Parse(Direccion_de_Viento);
            direccionRafaga = float.Parse(Direccion_de_Rafaga);
            rapidezViento = float.Parse(Rapidez_de_Viento);
            rapidezRafaga = float.Parse(Rapidez_de_Rafaga);
            temperatura = float.Parse(Temperatura);
            humedadRelativa = float.Parse(Humedad_Relativa);
            presionAtmosferica = float.Parse(Presion_Atmosferica);
            precipitacion = float.Parse(Precipitacion);


            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "INSERT INTO datosatmosfericos (estacion,fechaLocal,direccionViento,direccionRafaga,rapidezViento,rapidezRafaga,temperatura,humedadRelativa,presionAtmosferica,precipitacion)  VALUES('"+Estacion+"','"
                +Fecha_Local+"' , "+direccionViento+" , "+direccionRafaga+" , "+rapidezViento+" , "+rapidezRafaga+" , "+temperatura+" , "+humedadRelativa+" , "+presionAtmosferica
                +","+precipitacion+");";

            comando.CommandType = CommandType.Text;
            comando.ExecuteReader();
            conexion.CerrarConexion();
        }

        public DataTable MostrarDatosClimaMes()
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM datosatmosfericos ORDER BY fechaLocal DESC";
            //comando.CommandText = "MostrarDatosClimaMes";
            //comando.CommandType = CommandType.StoredProcedure;
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tablaDatosClimaMes.Load(leer);
            conexion.CerrarConexion();
            return tablaDatosClimaMes;
        }
        //Trae desde la bd Los datos Fecha_Local y temperatura y
        //Los hace una tabla
        public DataTable MostrarAlarmaClima()
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "mostrarAlarmaClima";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tablaDatosClima.Load(leer);
            conexion.CerrarConexion();
            return tablaDatosClima;
        }
        public void AgregarDiario(String fecha)
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "agregardiarios";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("_Fecha",fecha);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public String top_fecha()
        {
            
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT  distinct fechaLocal  FROM datosatmosfericos WHERE fechaLocal=(SELECT MAX(fechaLocal)  FROM datosatmosfericos);";
            leer = comando.ExecuteReader();
            String salida = "";
            if (leer.Read()==true)
            {
                salida = Convert.ToString(leer["fechaLocal"]);
            }
            conexion.CerrarConexion();
            return salida;
           
        }
    }
}

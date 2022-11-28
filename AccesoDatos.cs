using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;
using System.Collections;


namespace negocio
{  

    public class AccesoDatos
    {
        // creamos los OBJETOS de la Clase - Private porque la funcion/metodo lo hacemos dentro del mismo AMBITO.
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector; //while lectura

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
             conexion = new SqlConnection("server =.\\SQLEXPRESS01; database=POKEDEX_DB; integrated security=true");
             comando = new SqlCommand();  
        }

        public void setearConsulta(string consulta) //recibe la variable string del comantex.consulta. EJ: " select Numero, Nombre,P.Descripcion...."
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearProcedure(string exec)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = exec;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int EjecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
                                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        public void SetearParametros(string nombre,object valor)
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }



        // CIERRE CONEXION //
        public void cerrarConexion()
        {
            if (lector != null)
                conexion.Close();
        }

    }
}

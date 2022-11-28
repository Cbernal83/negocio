﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace negocio
{
    public class ElementoNegocio

    {
        public List <Elemento> listar()
        {
            List<Elemento> lista = new List<Elemento>();
            AccesoDatos datos = new AccesoDatos(); // Creamos un OBJETO (datos) de la CLASE AccesoDatos donde estan los Metodo/Funcion

            try
            {
                datos.setearConsulta (" select id,Descripcion from ELEMENTOS ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.id = (int) datos.Lector["id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }       
        
    }

}


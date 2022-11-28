using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace negocio
{
    public class TraineeNegocio
    {
        //DATOS QUE TENEMOS EN PRIMERA INSTANCIA DE REGISTRO.
        /* Id = automatico
         * email
         * pass
         * admin false
         */

        //NO TENEMOS
        //nombre, apellido,fecha,imagen

        public int AltaNuevoUser (Trainee nuevo)
        {
                AccesoDatos sql = new AccesoDatos();
            try
            {

                sql.SetearProcedure ("AltaNuevoUser");
                sql.SetearParametros("@email", nuevo.Email);
                sql.SetearParametros("@pass", nuevo.Pass);
                
                return sql.EjecutarAccionScalar();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sql.cerrarConexion();
            }
        }



    }
}

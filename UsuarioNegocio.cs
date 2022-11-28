using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear (Usuario Usuario)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.setearConsulta ("select Id,TipoUser from USUARIOS where Usuario=@User and Pass=@Pass ");
                Datos.SetearParametros("@User",Usuario.User);
                Datos.SetearParametros("@Pass", Usuario.Pass);

                Datos.ejecutarLectura();

                while (Datos.Lector.Read())
                {
                    Usuario.Id = (int)Datos.Lector["Id"];
                    Usuario.TipoUsuario = (int)(Datos.Lector["TipoUser"]) == 2 ? TipoUsuario.ADMIN : TipoUsuario.NORMAL;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Datos.cerrarConexion();
            }
        }
    }
}

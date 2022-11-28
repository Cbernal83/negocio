using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PokemonNegocio2
    {
        // METODOS LISTAR - AGREGAR - MODIFICAR - ELIMINAR FISICO
        public List<Pokemons> lista(string id="") // creo lista para guardar lo que traigo de la db


        {
            List<Pokemons> lista = new List<Pokemons>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta(" select Numero, Nombre,P.Descripcion, E.Descripcion Tipo ,D.Descripcion Debilidad,UrlImg,P.idTipo,P.idDebilidad,P.id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = idTipo AND D.id = idDebilidad AND P.Activo = 1  ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemons aux = new Pokemons();

                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.id = (int)datos.Lector["id"];


                    //if(!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("UrlImg")))
                    //aux.UrlImg = (string)datos.Lector["UrlImg"];

                    if (!(datos.Lector["UrlImg"] is DBNull)) // Sino es NULL leelo. Asi validamos q traiga los datos y no se rompa si la base tiene NULL
                        aux.UrlImg = (string)datos.Lector["UrlImg"]; //No todos los datos necesariamente, sino los datos q SI puede ser NULL.

                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["idTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["idDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

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

        public List<Pokemons> listaConSP()
        {
            List<Pokemons> lista = new List<Pokemons>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearProcedure("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemons aux = new Pokemons();

                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.id = (int)datos.Lector["id"];

                    if (!(datos.Lector["UrlImg"] is DBNull))
                        aux.UrlImg = (string)datos.Lector["UrlImg"];

                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["idTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["idDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

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
        public void AgregarConSP(Pokemons nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedure("StoredAltaPokemon");
                datos.SetearParametros("@numero", nuevo.Numero);
                datos.SetearParametros("@nombre", nuevo.Nombre);
                datos.SetearParametros("@descripcion", nuevo.Descripcion);
                datos.SetearParametros("@urlimg", nuevo.UrlImg);
                datos.SetearParametros("@idTipo", nuevo.Tipo.id);
                datos.SetearParametros("@idDebilidad", nuevo.Debilidad.id);
                datos.SetearParametros("@activo", nuevo.Activo);

                //datos.SetearParametros("@idEvolucion",null);

                datos.EjecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public void Agregar(Pokemons nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into POKEMONS (Numero,Nombre,Descripcion,UrlImg,Activo,idTipo,idDebilidad) " +
                    "values (" + nuevo.Numero + ",'" + nuevo.Nombre + "','" + nuevo.Descripcion + "',@UrlImg,1,@idTipo,@idDebilidad)");
                datos.SetearParametros("@idTipo", nuevo.Tipo.id);
                datos.SetearParametros("@idDebilidad", nuevo.Debilidad.id);
                datos.SetearParametros("@UrlImg", nuevo.UrlImg);

                datos.EjecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void Modificar(Pokemons modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @descripcion, UrlImg = @urlimg, idTipo = @idtipo, idDebilidad = @iddebilidad where id = @id");
                datos.SetearParametros("@numero", modificar.Numero);
                datos.SetearParametros("@nombre", modificar.Nombre);
                datos.SetearParametros("@descripcion", modificar.Descripcion);
                datos.SetearParametros("@urlimg", modificar.UrlImg);
                datos.SetearParametros("@idtipo", modificar.Tipo.id); // es tipo.id porque TIPO esta en la Clase Pokemos y id lo trae d clase elementos 
                datos.SetearParametros("@iddebilidad", modificar.Debilidad.id);
                datos.SetearParametros("@id", modificar.id);

                datos.EjecutarAccion();

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

        public void ModificarConSP(Pokemons modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedure("StoredModificarPokemon");
                datos.SetearParametros("@numero", modificar.Numero);
                datos.SetearParametros("@nombre", modificar.Nombre);
                datos.SetearParametros("@descripcion", modificar.Descripcion);
                datos.SetearParametros("@urlimg", modificar.UrlImg);
                datos.SetearParametros("@idtipo", modificar.Tipo.id); // es tipo.id porque TIPO esta en la Clase Pokemos y id lo trae d clase elementos 
                datos.SetearParametros("@iddebilidad", modificar.Debilidad.id);
                datos.SetearParametros("@activo",modificar.Activo);
                datos.SetearParametros("@id", modificar.id);

                datos.EjecutarAccion();

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





        public void Eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from POKEMONS where id = @id");
                datos.SetearParametros("@id", id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EliminarLogico(int id) // lo llamos desde frmPokemons
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update POKEMONS set Activo = 0 where id=@id");
                datos.SetearParametros("@id", id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<Pokemons> filtrarId (string id)
        {
            List<Pokemons> lista = new List<Pokemons>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select Numero, Nombre,P.Descripcion, E.Descripcion Tipo ,D.Descripcion Debilidad,UrlImg,P.idTipo,P.idDebilidad,P.id,P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = idTipo AND D.id = idDebilidad AND P.id =  ";

                consulta += id;

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemons aux = new Pokemons();

                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.id = (int)datos.Lector["id"];

                    if (!(datos.Lector["UrlImg"] is DBNull))
                        aux.UrlImg = (string)datos.Lector["UrlImg"];

                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["idTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["idDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

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



        public List<Pokemons> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemons> lista = new List<Pokemons>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select Numero, Nombre,P.Descripcion, E.Descripcion Tipo ,D.Descripcion Debilidad,UrlImg,P.idTipo,P.idDebilidad,P.id, Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = idTipo AND D.id = idDebilidad AND  ";
                //string orden = " order by Numero";
    

                if (campo == "Numero")
                {
                    switch (criterio)
                    {
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        case "Mayor a":
                            consulta += "Numero >" + filtro ;
                            break;
                        default:
                            consulta += "Numero =" + filtro ;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like' " + filtro + " %'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like'%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like'%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Tipo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like'" + filtro + "%'" ;
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like'%" + filtro + "'" ;
                            break;
                        default:
                            consulta += "E.Descripcion like'%" + filtro + "%'" ;
                            break;
                    }
                }

                if(estado== "Activo")
                {
                    consulta += " and Activo = 1";
                }
                else if(estado == "Inactivo")
                {
                    consulta += " and Activo = 0";
                }


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemons aux = new Pokemons();

                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.id = (int)datos.Lector["id"];

                    if (!(datos.Lector["UrlImg"] is DBNull))
                        aux.UrlImg = (string)datos.Lector["UrlImg"];

                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["idTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["idDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

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



        public List<Pokemons> filtrar (string campo, string criterio)
        {
            List<Pokemons> listaFiltro = new List<Pokemons>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select Numero, Nombre,P.Descripcion, E.Descripcion Tipo ,D.Descripcion Debilidad,UrlImg,P.idTipo,P.idDebilidad,P.id, Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = idTipo AND D.id = idDebilidad AND  ";
                
               
                if (campo == "Tipo")
                {
                  
                   consulta += "E.Descripcion = ' " + criterio + " ' ";
                   
                }


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemons aux = new Pokemons();

                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.id = (int)datos.Lector["id"];

                    if (!(datos.Lector["UrlImg"] is DBNull))
                        aux.UrlImg = (string)datos.Lector["UrlImg"];

                    aux.Activo = (bool) datos.Lector["Activo"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["idTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["idDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    listaFiltro.Add(aux);

                }

                return listaFiltro;
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

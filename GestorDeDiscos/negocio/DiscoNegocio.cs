using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class DiscoNegocio
    {
        public List<Disco> listar()
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select D.Id, D.Titulo, D.CantidadCanciones, D.UrlImagenTapa, d.FechaLanzamiento, e.Id as IdEstilo, E.Descripcion as Estilo, t.Id as IdTipoEdicion, T.Descripcion as TipoEdicion from DISCOS D, ESTILOS E, TIPOSEDICION T where d.IdEstilo = e.Id and d.IdTipoEdicion = t.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                    {
                        aux.Url = (string)datos.Lector["UrlImagenTapa"];
                    }
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)datos.Lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)datos.Lector["Estilo"];
                    aux.TipoEdicion = new TipoEdicion();
                    aux.TipoEdicion.Id = (int)datos.Lector["IdTipoEdicion"];
                    aux.TipoEdicion.Descripcion = (string)datos.Lector["TipoEdicion"];

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

        public void agregarDisco(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa) values (@Titulo, CAST(@Fecha AS smalldatetime), @CantCanciones, @IdEstilo, @IdTIpoEdicion, @UrlImagen)");
                datos.setearParametros("@Titulo", nuevo.Titulo);
                datos.setearParametros("@Fecha", nuevo.FechaLanzamiento);
                datos.setearParametros("@CantCanciones", nuevo.CantidadCanciones);
                datos.setearParametros("UrlImagen", nuevo.Url);
                datos.setearParametros("@IdEstilo", nuevo.Estilo.Id);
                datos.setearParametros("@IdTipoEdicion", nuevo.TipoEdicion.Id);

                datos.ejecutarAccion();
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

        public void modificarDisco(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @Titulo, FechaLanzamiento = @FechaLanz, CantidadCanciones = @CantCanc, UrlImagenTapa = @Imagen, IdEstilo = @IdEstilo, IdTipoEdicion = @IdTipoEdicion where Id = @Id");
                datos.setearParametros("@Titulo", disco.Titulo);
                datos.setearParametros("@FechaLanz", disco.FechaLanzamiento);
                datos.setearParametros("@CantCanc", disco.CantidadCanciones);
                datos.setearParametros("@Imagen", disco.Url);
                datos.setearParametros("@IdEstilo", disco.Estilo.Id);
                datos.setearParametros("@IdTipoEdicion", disco.TipoEdicion.Id);
                datos.setearParametros("@Id", disco.Id);
                datos.ejecutarAccion();
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

        public void eliminarDisco(int idDisco)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("delete from DISCOS where Id = @Id");
                datos.setearParametros("@Id", idDisco);
                datos.ejecutarAccion();
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

        public List<Disco> filtrar(string campo, string criterio, string filtro)
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select D.Id, D.Titulo, D.CantidadCanciones, D.UrlImagenTapa, d.FechaLanzamiento, e.Id as IdEstilo, E.Descripcion as Estilo, t.Id as IdTipoEdicion, T.Descripcion as TipoEdicion from DISCOS D, ESTILOS E, TIPOSEDICION T where d.IdEstilo = e.Id and d.IdTipoEdicion = t.Id and ";
                
                if(campo == "Id")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "D.Id > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "D.Id < " + filtro;
                            break;
                        case "Igual a":
                            consulta += "D.Id = " + filtro;
                            break;
                        default:
                            break;
                    }
                }
                if(campo == "Cantidad Canciones")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "D.CantidadCanciones > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "D.CantidadCanciones < " + filtro;
                            break;
                        case "Igual a":
                            consulta += "D.CantidadCanciones = " + filtro;
                            break;
                        default:
                            break;
                    }
                }
                if(campo == "Titulo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "D.Titulo like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "D.Titulo like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "D.Titulo like '%" + filtro + "%'";
                            break;
                        default:
                            break;
                    }
                }
                if (campo == "Estilo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                        default:
                            break;
                    }
                }
                if (campo == "Tipo Edicion")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "T.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                        default:
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                    {
                        aux.Url = (string)datos.Lector["UrlImagenTapa"];
                    }
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)datos.Lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)datos.Lector["Estilo"];
                    aux.TipoEdicion = new TipoEdicion();
                    aux.TipoEdicion.Id = (int)datos.Lector["IdTipoEdicion"];
                    aux.TipoEdicion.Descripcion = (string)datos.Lector["TipoEdicion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

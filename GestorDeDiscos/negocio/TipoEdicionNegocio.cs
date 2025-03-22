using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class TipoEdicionNegocio
    {
        public List<TipoEdicion> listar()
        {
            List<TipoEdicion> lista = new List<TipoEdicion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from TIPOSEDICION");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoEdicion aux = new TipoEdicion();
                    aux.Id = (int)datos.Lector["Id"];
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

        public bool agregar(TipoEdicion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "insert into TIPOSEDICION (Descripcion) values (@Descripcion)";
                datos.setearConsulta(consulta);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);

                datos.ejecutarAccion();
                return true;
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

        public bool estiloRepetido(TipoEdicion nuevo)
        {
            List<TipoEdicion> listaEstilos = listar();

            foreach (var tipoEdicion in listaEstilos)
            {
                if (nuevo.Descripcion.ToUpper().Trim() == tipoEdicion.Descripcion.ToUpper().Trim())
                {
                    return true;
                }
            }
            return false;
        }
    }
}

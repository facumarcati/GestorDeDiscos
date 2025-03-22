using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EstiloNegocio
    {
        public List<Estilo> listar()
        {
			List<Estilo> lista = new List<Estilo>();
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("select Id, Descripcion from ESTILOS");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Estilo aux = new Estilo();
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

		public bool agregar(Estilo nuevo)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				string consulta = "insert into ESTILOS (Descripcion) values (@Descripcion)";
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

		public bool estiloRepetido(Estilo nuevo)
		{
			List<Estilo> listaEstilos = listar();

			foreach (var estilo in listaEstilos)
			{
				if(nuevo.Descripcion.ToUpper().Trim() == estilo.Descripcion.ToUpper().Trim())
				{
					return true;
				}
			}
			return false;
		}
    }
}

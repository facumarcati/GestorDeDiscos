using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_app
{
    public partial class frmAgregarEstiloTipoEdicion: Form
    {
        private string tipoAlta;
        public frmAgregarEstiloTipoEdicion()
        {
            InitializeComponent();
        }

        public frmAgregarEstiloTipoEdicion(string tipo)
        {
            InitializeComponent();
            tipoAlta = tipo;
        }

        private void FrmAgregarEstiloTipoEdicion_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Estilo estilo = new Estilo();
            EstiloNegocio negocioEstilo = new EstiloNegocio();
            TipoEdicion tipoEdicion = new TipoEdicion();
            TipoEdicionNegocio negocioTipoEdicion = new TipoEdicionNegocio();

            try
            {
                estilo.Descripcion = txtEstiloTipoEdicion.Text;
                if (validarDescripcion(txtEstiloTipoEdicion.Text))
                {
                    MessageBox.Show("Se requiere una descripcion valida");
                    return;
                }

                if (tipoAlta == "Estilo")
                {
                    if (negocioEstilo.estiloRepetido(estilo))
                    {
                        MessageBox.Show("Ya existe un estilo con ese nombre");
                        return;
                    }
                    else
                    {
                        if (negocioEstilo.agregar(estilo))
                        {
                            MessageBox.Show("Estilo agregado con exito");
                        }
                    }
                }
                if(tipoAlta == "Tipo Edicion")
                {
                    if (negocioTipoEdicion.estiloRepetido(tipoEdicion))
                    {
                        MessageBox.Show("Ya existe un tipo de edicion con ese nombre");
                        return;
                    }
                    else
                    {
                        if (negocioTipoEdicion.agregar(tipoEdicion))
                        {
                            MessageBox.Show("Tipo edicion agregado con exito");
                        }
                    }
                }

                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool validarDescripcion(string descripcion)
        {
            if (descripcion.Equals(""))
            {
                return true;
            }

            return false;
        }
    }
}

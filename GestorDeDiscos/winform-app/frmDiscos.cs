using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace winform_app
{
    public partial class frmDiscos: Form
    {
        private List<Disco> listaDisco;
        public frmDiscos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxCampo.Items.Add("Id");
            cbxCampo.Items.Add("Titulo");
            cbxCampo.Items.Add("Cantidad Canciones");
            cbxCampo.Items.Add("Estilo");
            cbxCampo.Items.Add("Tipo Edicion");
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco discoSeleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            cargarImagen(discoSeleccionado.Url);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            cargarDiscos();
        }

        private void cargarDiscos()
        {
            DiscoNegocio negocio = new DiscoNegocio();

            try
            {
                listaDisco = negocio.listar();
                dgvDiscos.DataSource = listaDisco;
                ocultarColumnas();
                cargarImagen(listaDisco[0].Url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvDiscos.Columns["Url"].Visible = false;
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxDiscos.Load(imagen);
            }
            catch (Exception)
            {

                pbxDiscos.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarDisco agregar = new frmAgregarDisco();
            agregar.ShowDialog();
            cargarDiscos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco seleccionado;

            try
            {
                if (dgvDiscos.CurrentRow == null || dgvDiscos.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Ningún disco seleccionado");
                    return;
                }

                seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                frmAgregarDisco modificar = new frmAgregarDisco(seleccionado);
                modificar.ShowDialog();
                cargarDiscos();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Disco seleccionado;
            DiscoNegocio negocio = new DiscoNegocio();

            try
            {
                if (dgvDiscos.CurrentRow == null || dgvDiscos.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Ningún disco seleccionado");
                    return;
                }

                DialogResult respuesta =  MessageBox.Show("Eliminar disco?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                    negocio.eliminarDisco(seleccionado.Id);
                    cargarDiscos();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAgregarEstilo_Click(object sender, EventArgs e)
        {
            frmAgregarEstiloTipoEdicion agregar = new frmAgregarEstiloTipoEdicion("Estilo");
            agregar.ShowDialog();
        }

        private void btnAgregarTipoEdicion_Click(object sender, EventArgs e)
        {
            frmAgregarEstiloTipoEdicion agregar = new frmAgregarEstiloTipoEdicion("Tipo Edicion");
            agregar.ShowDialog();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            DiscoNegocio negocio = new DiscoNegocio();
            string filtro = txtFiltro.Text;

            if(dgvDiscos.CurrentRow == null)
            {
                controladorDGV(listaDisco = negocio.listar());
            }

            if(filtro != "")
            {
                listaFiltrada = listaDisco.FindAll(x => x.Titulo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaDisco;
            }

            controladorDGV(listaFiltrada);
            ocultarColumnas();
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxCampo.SelectedItem.ToString() == "Id" || cbxCampo.SelectedItem.ToString() == "Cantidad Canciones")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Comienza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }
        }

        private bool validarFiltro()
        {
            if(cbxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un campo para filtrar");
                return true;
            }
            if(cbxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un criterio para filtrar");
                return true;
            }
            if(cbxCampo.SelectedItem.ToString() == "Cantidad Canciones" || cbxCampo.SelectedItem.ToString() == "Id")
            {
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingresar solo numeros");
                    return true;
                }
                if(txtFiltroAvanzado.Text == "")
                {
                    return true;
                }
            }

            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }

            return true;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();

            try
            {
                if (validarFiltro())
                {
                    return;
                }
                
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                listaDisco = negocio.filtrar(campo, criterio, filtro);
                dgvDiscos.DataSource = listaDisco;
                ocultarColumnas();
                cargarImagen(listaDisco[0].Url);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            List<Disco> listaDiscos = negocio.listar();

            if (rbId.Checked)
            {
                listaDiscos = listaDiscos.OrderBy(disco => disco.Id).ToList(); // Orden ascendente

                controladorDGV(listaDiscos);
                ocultarColumnas();
            }
            if (rbTitulo.Checked)
            {
                listaDiscos = listaDiscos.OrderBy(disco => disco.Titulo).ToList(); // Orden ascendente

                controladorDGV(listaDiscos);
                ocultarColumnas();
            }
            if (rbFecha.Checked)
            {
                listaDiscos = listaDiscos.OrderBy(disco => disco.FechaLanzamiento).ToList(); // Orden ascendente

                controladorDGV(listaDiscos); 
                ocultarColumnas();
            }
            if (rbCantCanciones.Checked)
            {
                listaDiscos = listaDiscos.OrderBy(disco => disco.CantidadCanciones).ToList(); // Orden ascendente

                controladorDGV(listaDiscos);
                ocultarColumnas();
            }
        }

        public void controladorDGV(List<Disco> listaDiscos)
        {
            dgvDiscos.DataSource = null;
            dgvDiscos.DataSource = listaDiscos;
        }
    }
}

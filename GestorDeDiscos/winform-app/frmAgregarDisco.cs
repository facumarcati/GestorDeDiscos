using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace winform_app
{
    public partial class frmAgregarDisco: Form
    {
        private Disco disco = null;
        private OpenFileDialog archivo = null;
        public frmAgregarDisco()
        {
            InitializeComponent();
        }
        public frmAgregarDisco(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAgregarDisco_Load(object sender, EventArgs e)
        {
            EstiloNegocio negocioEstilo = new EstiloNegocio();
            TipoEdicionNegocio negocioTipoEdicion = new TipoEdicionNegocio();

            try
            {
                cbxEstilo.DataSource = negocioEstilo.listar();
                cbxEstilo.ValueMember = "Id";
                cbxEstilo.DisplayMember = "Descripcion";
                cbxTipoEdicion.DataSource = negocioTipoEdicion.listar();
                cbxTipoEdicion.ValueMember = "Id";
                cbxTipoEdicion.DisplayMember = "Descripcion";

                if (disco != null)
                {
                    txtTitulo.Text = disco.Titulo;
                    dtpFechaLanzamiento.Text = disco.FechaLanzamiento.ToString();
                    txtCantCanciones.Text = disco.CantidadCanciones.ToString();
                    txtUrlImagen.Text = disco.Url;
                    cargarImagen(disco.Url);
                    cbxEstilo.SelectedValue = disco.Estilo.Id;
                    cbxTipoEdicion.SelectedValue = disco.TipoEdicion.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();

            try
            {
                if(disco == null)
                {
                    disco = new Disco();
                }
                disco.Titulo = txtTitulo.Text;
                if (validarTitulo(txtTitulo.Text))
                {
                    MessageBox.Show("Se requiere un titulo");
                    return;
                }
                Console.WriteLine("Fecha seleccionada: " + dtpFechaLanzamiento.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                disco.FechaLanzamiento = new DateTime(
                    dtpFechaLanzamiento.Value.Year,
                    dtpFechaLanzamiento.Value.Month,
                    dtpFechaLanzamiento.Value.Day
                );
                disco.CantidadCanciones = int.Parse(txtCantCanciones.Text);
                if (validarCantCanciones(int.Parse(txtCantCanciones.Text)))
                {
                    MessageBox.Show("Ingresar una cantidad de canciones valida");
                    return;
                }
                disco.Url = txtUrlImagen.Text;
                disco.Estilo = (Estilo)cbxEstilo.SelectedItem;
                disco.TipoEdicion = (TipoEdicion)cbxTipoEdicion.SelectedItem;

                if(disco.Id != 0)
                {
                    negocio.modificarDisco(disco);
                    MessageBox.Show("Disco modificado");
                }
                else
                {
                    negocio.agregarDisco(disco);
                    MessageBox.Show("Disco agregado");
                }

                //Guardo imagen local
                if(archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["discos-img"] + archivo.FileName);
                }

                Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool validarTitulo(string campo)
        {
            if(campo.Equals(""))
            {
                return true;
            }

            return false;
        }

        private bool validarCantCanciones(int cantCanciones)
        {
            if (soloNumeros(cantCanciones.ToString()) == false)
            {
                return true;
            }
            if(cantCanciones < 0)
            {
                return true;
            }
            if(cantCanciones.ToString().Equals(""))
            {
                return true;
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

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg|png|*.png";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }
    }
}

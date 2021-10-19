using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmClient;
using TrabajoPracticoIntegradorPav1.Entities;
using TrabajoPracticoIntegradorPav1.Exceptions;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            try
            {
                CargarComboBarrio();
                CargarComboContacto();
                CargarGrilla();
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar conectarse con la base de datos" + ex.Message);
            }
            
        }
        //Metodo para cargar el combobox de barrios
        private void CargarComboBarrio()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Barrios WHERE borrado IS NULL or borrado = 0";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                cmbBarrio.DataSource = table;
                cmbBarrio.DisplayMember = "nombre";
                cmbBarrio.ValueMember = "id_barrio";
                cmbBarrio.SelectedIndex = -1;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        //Metodo para cargar el combo de contactos
        private void CargarComboContacto()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT id_contacto, nombre, apellido, email, telefono, borrado, CONCAT(nombre, ' ', apellido) as nombreCompleto FROM Contactos WHERE borrado IS NULL or borrado = 0";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                cmbContacto.DataSource = table;
                cmbContacto.DisplayMember = "nombreCompleto";
                cmbContacto.ValueMember = "id_contacto";
                cmbContacto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        //Metodo para cargar la grilla de clientes
        private void CargarGrilla()
        {

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT id_cliente, cuit, razon_social, calle, numero, fecha_alta, Barrios.nombre AS barrio, CONCAT(CONTACTOS.NOMBRE, ' ', CONTACTOS.APELLIDO) AS contacto from Clientes INNER JOIN Barrios ON Clientes.id_barrio = Barrios.id_barrio INNER JOIN CONTACTOS ON CONTACTOS.ID_CONTACTO = CLIENTES.ID_CONTACTO WHERE Clientes.borrado IS NULL or Clientes.borrado = 0";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                dgvClientes.DataSource = table;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                Client c = new Client();
                c.id_cliente = String.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text);
                
                c.razon_social = txtRazonSocial.Text;
                c.calle = txtCalle.Text;
                c.numero = int.Parse(txtNumeroCalle.Text);
                c.cuit = long.Parse(txtCuit.Text);
                c.fecha_alta = DateTime.ParseExact(txtFechaAlta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                c.fecha_alta = DateTime.Now;
                c.id_barrio = (int)cmbBarrio.SelectedValue;
                c.id_contacto = (int)cmbContacto.SelectedValue;

                bool result = AddClient.Add(c);
                if (result)
                {
                    MessageBox.Show("Cliente agregado");
                    CargarGrilla();
                    LimpiarCampos();
                    txtCuit.Focus();
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Ocurrio un error inesperado al intentar cargar el cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar el cliente " + ex.Message);
            }
        }

        //Metodo para limpiar los campos
        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtCuit.Text = "";
            txtCalle.Text = "";
            txtNumeroCalle.Text = "";
            txtFechaAlta.Text = "";
            cmbBarrio.SelectedIndex = -1;
            cmbContacto.SelectedIndex = -1;
        }

        //Metodo para cargar los campos
        private void CargarCampos(Client c)
        {
            txtId.Text = c.id_cliente.ToString();
            txtCuit.Text = c.cuit.ToString();
            txtRazonSocial.Text = c.razon_social;
            txtCalle.Text = c.calle;
            txtNumeroCalle.Text = c.numero.ToString();
            txtFechaAlta.Text = c.fecha_alta.ToString("dd/MM/yyyy");
            cmbBarrio.SelectedValue = c.id_barrio;
            cmbBarrio.SelectedValue = c.id_contacto;
        }
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAgregar.Enabled = false;

            try
            {
                var filaSeleccionada = ((DataRowView) dgvClientes.SelectedRows[0].DataBoundItem).Row;
                string id = filaSeleccionada["id_cliente"].ToString();

                Client c = GetClient.Get(id);

                CargarCampos(c);
            }
            catch (Exception ex)
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = true;

                LimpiarCampos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (var context = new tpDbContext())
            {
                var clienteId = int.Parse(txtId.Text);
                var cliente = context.Clientes.First(c => c.id_cliente == clienteId);

                cliente.razon_social = txtRazonSocial.Text;
                cliente.calle = txtCalle.Text;
                cliente.numero = (txtNumeroCalle.Text);
                cliente.cuit = txtCuit.Text;
                cliente.fecha_alta = DateTime.ParseExact(txtFechaAlta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cliente.fecha_alta = DateTime.Now;
                cliente.id_barrio = (int)cmbBarrio.SelectedValue;
                cliente.id_contacto = (int)cmbContacto.SelectedValue;

                context.SaveChanges();
            }
            MessageBox.Show("Cliente eliminado exitosamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (var context = new tpDbContext()) {
                var selectedId = int.Parse(((DataRowView)dgvClientes.SelectedRows[0].DataBoundItem).Row["id_cliente"].ToString());

                var clienteEliminar = new Cliente { id_cliente = selectedId };

                context.Clientes.Attach(clienteEliminar);
                clienteEliminar.borrado = true;
                context.SaveChanges();
            
            }
            MessageBox.Show("Cliente eliminado exitosamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarGrilla();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmClient;
using TrabajoPracticoIntegradorPav1.Entities;

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
                CargarCombo();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar conectarse con la base de datos" + ex.Message);
            }
            
        }
        //Metodo para cargar el combobox de barrios
        private void CargarCombo()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Barrios WHERE borrado IS NULL";

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

        //Metodo para cargar la grilla de clientes
        private void CargarGrilla()
        {

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                string consulta = "select cuit, razon_social, calle, numero, fecha_alta, Barrios.nombre AS barrio from Clientes INNER JOIN Barrios ON Clientes.id_barrio = Barrios.id_barrio WHERE Clientes.borrado IS NULL";

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
            Client c = new Client();
            c.cuit = long.Parse(txtCuit.Text);
            c.razon_social = txtRazonSocial.Text;
            c.calle = txtCalle.Text;
            c.numero = int.Parse(txtNumeroCalle.Text);
            c.fecha_alta = DateTime.Parse(txtFechaAlta.Text);
            c.id_barrio = (int)cmbBarrio.SelectedValue;

            try
            {
                bool result = AddClient.Add(c);
                if (result)
                {
                    MessageBox.Show("Cliente agregado");
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error inesperado al intentas cargar el cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

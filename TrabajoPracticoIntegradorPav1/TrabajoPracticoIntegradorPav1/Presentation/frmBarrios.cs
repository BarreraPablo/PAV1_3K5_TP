using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmNeighborhood;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentacion
{
    public partial class frmBarrios : Form
    {
        public frmBarrios()
        {
            InitializeComponent();
        }

        private void frmBarrios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            txtIdBarrio.Enabled = false;
        }

        //Metodo para cargar los barrios en la grilla
        private void CargarGrilla()
        {

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                
                    string consulta = "SELECT nombre FROM Barrios WHERE borrado is NULL";

                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = consulta;

                    cn.Open();
                    cmd.Connection = cn;

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);

                    dgvBarrios.DataSource = table;  
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
                //Validamos que el campo barrio no este vacio
                if (txtNombreBarrio.Text.Equals(""))
                {
                    MessageBox.Show("El nombre del barrio es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    txtNombreBarrio.Focus();
                    return;
                }else if(txtNombreBarrio.Text.Length > 50)
                {
                    MessageBox.Show("El barrio que intenta ingresar no puede tener mas de 50 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //Si pasamos la validacion...
                    bool result = AddNeighborhood.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text));
                    if (result)
                    {
                        MessageBox.Show("Barrio agregado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla();
                        txtNombreBarrio.Text = "";
                        txtIdBarrio.Text = "";
                        txtNombreBarrio.Focus();
                    }
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar establecer conexion con la base de datos");
            }
        }

        //Metodo que obtiene el nombre de la fila cuando hacemos click sobre ella
        private void dgvBarrios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                DataGridViewRow selectedRow = dgvBarrios.Rows[i];
                string name = selectedRow.Cells["nombre"].Value.ToString();

                //Pasamos por parametro el nombre del registro que obtuvimos al hacerle click para obtener el barrio
                Neighborhood b = GetNeighborhood.Get(name);
                //Cargamos el texbox con el nombre del barrio que obtuvimos del metodo anterior 
                LoadFiel(b);
            }
            catch (Exception)
            {

            }
            
        }
        
        //Metodo para cargar el texbox de barrio
        private void LoadFiel(Neighborhood b)
        {
            txtNombreBarrio.Text = b.nombre;
            txtIdBarrio.Text = b.id_barrio.ToString();
            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;
            btnAgregar.Enabled = false;
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (!txtNombreBarrio.Text.Equals(""))
            {
                try
                {
                    bool result = DeleteNeighborhood.Delete(txtNombreBarrio.Text);
                    if (result)
                    {
                        MessageBox.Show("Barrio eliminado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla();
                        txtNombreBarrio.Text = "";
                        txtIdBarrio.Text = "";
                        btnAgregar.Enabled = true;
                        btnEliminar.Enabled = false;
                        btnEditar.Enabled = false;
                        txtNombreBarrio.Enabled = true;
                        txtNombreBarrio.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Error, no se pudo eliminar el barrio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error inesperado al intentar consultar la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error, no se pudo eliminar el barrio porque usted modifico el nombre del barrio a eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //Neighborhood b = GetNeighborhood.Get(txtNombreBarrio.Text);

                bool result = UpdateNeighborhood.Update(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text), txtIdBarrio.Text);
                if (result)
                {
                    MessageBox.Show("Barrio editado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    txtNombreBarrio.Text = "";
                    txtIdBarrio.Text = "";
                    btnAgregar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnEditar.Enabled = false;
                    txtNombreBarrio.Enabled = true;
                    txtNombreBarrio.Focus();
                }
                else
                {
                    MessageBox.Show("Error, no se pudo editar el barrio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado al intentar consultar la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                string consulta = "SELECT nombre FROM Barrios WHERE nombre LIKE '" +txtBuscar.Text+ "%' AND borrado IS NULL";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                dgvBarrios.DataSource = table;
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
    }
}

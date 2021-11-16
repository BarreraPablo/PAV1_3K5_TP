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
using TrabajoPracticoIntegradorPav1.DataAccesLayer.AbmProject;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmProyecto : Form
    {
        public frmProyecto()
        {
            InitializeComponent();
        }

        private void frmProyecto_Load(object sender, EventArgs e)
        {
            try
            {
                CargarComboProductos();
                CargarComboUsuarios();
                CargarGrilla();
                LimpiarCampos();
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error al intentar conectarse con la base de datos" + ex.Message);
                
            }
        }

        //Metodo para cargar el combo de productos
        private void CargarComboProductos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Productos WHERE borrado IS NULL or borrado = 0";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                table.Columns.Add("nombre");
                table.Columns.Add("id_producto");

                var seleccionarRow = table.NewRow();
                seleccionarRow["nombre"] = "Seleccionar";
                seleccionarRow["id_producto"] = -1;

                table.Rows.Add(seleccionarRow);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                cmbProductos.DataSource = table;
                cmbProductos.DisplayMember = "nombre";
                cmbProductos.ValueMember = "id_producto";
                cmbProductos.SelectedValue = -1;
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

        //Metodo para cargar el combo de los usuarios
        private void CargarComboUsuarios()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                string consulta = "SELECT * FROM Usuarios WHERE borrado IS NULL or borrado = 0";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                table.Columns.Add("usuario");
                table.Columns.Add("id_usuario");
                var seleccionarRow = table.NewRow();
                seleccionarRow["usuario"] = "Seleccionar";
                seleccionarRow["id_usuario"] = -1;

                table.Rows.Add(seleccionarRow);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                cmbUsuarios.DataSource = table;
                cmbUsuarios.DisplayMember = "usuario";
                cmbUsuarios.ValueMember = "id_usuario";
                cmbUsuarios.SelectedValue = -1;
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

        //Metodo para cargar la grilla
        private void CargarGrilla()
        {

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                string consulta = "SELECT id_proyecto, Productos.id_producto, nombre, usuario, descripcion, version, alcance, id_responsable FROM Proyectos INNER JOIN Productos on Proyectos.id_producto = Productos.id_producto INNER JOIN Usuarios ON Usuarios.id_usuario = Proyectos.id_responsable WHERE Proyectos.borrado IS NULL ";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                dgvProyectos.AutoGenerateColumns = false;
                dgvProyectos.DataSource = table;
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

        //Metodo para cargar la grilla con los proyectos borrados
        private void CargarGrillaBorrados()
        {

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["conexionDB"];
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {

                string consulta = "SELECT id_proyecto, id_producto, descripcion, version, alcance, id_responsable FROM Proyectos WHERE borrado = 1 ";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                dgvProyectos.DataSource = table;
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

        //Metodo para limpiar los campos
        private void LimpiarCampos()
        {
            cmbProductos.SelectedValue = -1;
            txtDescripcion.Text = "";
            txtVersion.Text = "";
            txtAlcance.Text = "";
            cmbUsuarios.SelectedValue = -1;
            txtId.Text = "";
        }

        //Metodo para obtener los datos que tengo en los textbox y checkboxs
        private Project ObtenerDatosProyecto()
        {
            Project p = new Project();
            p.id_proyecto = String.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text);
            p.id_producto = int.Parse(cmbProductos.SelectedValue.ToString());
            p.descripcion = txtDescripcion.Text;
            p.version = txtVersion.Text;
            p.alcance = txtAlcance.Text;
            p.id_responsable = int.Parse(cmbUsuarios.SelectedValue.ToString());

            return p;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(int.Parse(cmbProductos.SelectedValue.ToString()) == -1)
            {
                MessageBox.Show("Debe seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(cmbUsuarios.SelectedValue.ToString()) == -1)
            {
                MessageBox.Show("Debe seleccionar un responsable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtDescripcion.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar una descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtVersion.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar una version", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtAlcance.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar un alcance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Project p = ObtenerDatosProyecto();

            try
            {
                bool result = AddProject.Add(p);
                if (result)
                {
                    MessageBox.Show("Proyecto creado con exito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar agregar el proyecto " + ex.Message);
            }
        }

        private void dgvProyectos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnAgregar.Enabled = false;

            try
            {
                int indice = e.RowIndex;
                DataGridViewRow filaSeleccionada = dgvProyectos.Rows[indice];
                string id = filaSeleccionada.Cells["id_proyecto"].Value.ToString();

                Project p = GetProject.Get(id);

                CargarCampos(p);
            }
            catch (Exception ex)
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = true;

                LimpiarCampos();
            }
        }

        //Metodo para cargar los textbox
        private void CargarCampos(Project p)
        {
            txtId.Text = p.id_proyecto.ToString();
            txtDescripcion.Text = p.descripcion;
            txtVersion.Text = p.version;
            txtAlcance.Text = p.alcance;
            cmbProductos.SelectedValue = p.id_producto;
            cmbUsuarios.SelectedValue = p.id_responsable;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (int.Parse(cmbProductos.SelectedValue.ToString()) == 1)
            {
                MessageBox.Show("Debe seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(cmbUsuarios.SelectedValue.ToString()) == 1)
            {
                MessageBox.Show("Debe seleccionar un responsable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtDescripcion.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar una descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtVersion.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar una version", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtAlcance.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar un alcance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Project p = ObtenerDatosProyecto();
            try
            {
                bool result = EditProject.Edit(p);
                if (result)
                {
                    MessageBox.Show("Proyecto editado correctamente");
                    CargarComboProductos();
                    CargarComboUsuarios();
                    CargarGrilla();
                    LimpiarCampos();
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnAgregar.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar editar el proyecto" + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Project p = ObtenerDatosProyecto();

            try
            {
                bool result = DeleteProject.Delete(p);
                if (result)
                {
                    MessageBox.Show("Proyecto eliminado");
                    CargarComboProductos();
                    CargarComboUsuarios();
                    CargarGrilla();
                    LimpiarCampos();
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnAgregar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar el proyecto" + ex.Message);
            }
            
        }

        private void chkBorrado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBorrado.Checked)
            {
                CargarGrillaBorrados();
            }
            else
            {
                CargarGrilla();
            }
            
        }
    }
}

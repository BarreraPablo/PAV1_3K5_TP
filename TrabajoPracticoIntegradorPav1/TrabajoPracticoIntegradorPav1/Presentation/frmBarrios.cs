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
using TrabajoPracticoIntegradorPav1.Entities;
using TrabajoPracticoIntegradorPav1.DataAccesLayer;

namespace TrabajoPracticoIntegradorPav1.Presentacion
{
    public partial class frmBarrios : Form
    {
        private UnitOfWork unitOfWork;

        public frmBarrios()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
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
            unitOfWork.Open();
            var neighborhoods = unitOfWork.NeighborhoodDao.GetAll();
            unitOfWork.Close();

            dgvBarrios.DataSource = neighborhoods;  
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
                    unitOfWork.Open();
                    unitOfWork.NeighborhoodDao.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text));
                    unitOfWork.Close();

                    MessageBox.Show("Barrio agregado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    txtNombreBarrio.Text = "";
                    txtIdBarrio.Text = "";
                    txtNombreBarrio.Focus();

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
                unitOfWork.Open();
                Neighborhood b = unitOfWork.NeighborhoodDao.GetByName(name).First();
                unitOfWork.Close();
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
                    unitOfWork.Open();
                    unitOfWork.NeighborhoodDao.Delete(txtNombreBarrio.Text);
                    unitOfWork.Close();

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
                unitOfWork.Open();
                unitOfWork.NeighborhoodDao.Update(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text), txtIdBarrio.Text);
                unitOfWork.Close();

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
            catch (Exception)
            {
                MessageBox.Show("Error inesperado al intentar consultar la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_KeyUp(object sender, KeyEventArgs e)
        {


            try
            {
                unitOfWork.Open();
                dgvBarrios.DataSource = unitOfWork.NeighborhoodDao.GetByName(txtBuscar.Text);
                unitOfWork.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

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
using TrabajoPracticoIntegradorPav1.Business;

namespace TrabajoPracticoIntegradorPav1.Presentacion
{

    public partial class frmSimple<T, X> : Form where T : class, ISimpleService<X>, new()
    {
        private ISimpleService<X> simpleService;

        public frmSimple(string titlesNames)
        {
            InitializeComponent();
            simpleService = new T();

            setTitles(titlesNames);
        }

        private void setTitles(string titlesNames)
        {
            lblTitulo.Text = $"Alta de {titlesNames}";
            gbSearch.Text = $"Todos los {titlesNames}";
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
            var entities = simpleService.GetAll();

            dgvBarrios.DataSource = entities;  
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
                    simpleService.Add(txtNombreBarrio.Text);

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
                ISimpleEntity entitySelected = (ISimpleEntity)dgvBarrios.SelectedRows[0].DataBoundItem;

                LoadFiel(entitySelected);
            }
            catch (Exception)
            {

            }
            
        }
        
        //Metodo para cargar el texbox de barrio
        private void LoadFiel(ISimpleEntity b)
        {
            txtNombreBarrio.Text = b.nombre;
            txtIdBarrio.Text = b.id.ToString();
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
                    ISimpleEntity entitySelected = (ISimpleEntity)dgvBarrios.SelectedRows[0].DataBoundItem;
                    simpleService.Delete(entitySelected.id);

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
                simpleService.Update(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text), txtIdBarrio.Text);

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
               dgvBarrios.DataSource = simpleService.GetByName(txtBuscar.Text);
            }
            catch (Exception)
            {

            }
        }
    }
}

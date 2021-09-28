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
using TrabajoPracticoIntegradorPav1.Exceptions;

namespace TrabajoPracticoIntegradorPav1.Presentacion
{

    public partial class frmSimple<T, X> : Form where T : class, ISimpleService<X>, new()
    {
        private ISimpleService<X> simpleService;
        private string name;
        public frmSimple(string titleName)
        {
            InitializeComponent();
            simpleService = new T();
            name = titleName;
            setTitles();
        }

        private void setTitles()
        {
            lblTitulo.Text = $"Alta de {name + 's'}";
            gbSearch.Text = $"Todos los {name + 's'}";
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
                    MessageBox.Show($"El nombre del {name} es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    txtNombreBarrio.Focus();
                    return;
                }else if(txtNombreBarrio.Text.Length > 50)
                {
                    MessageBox.Show($"El {name} que intenta ingresar no puede tener mas de 50 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //Si pasamos la validacion...
                    simpleService.Add(txtNombreBarrio.Text);

                    MessageBox.Show($"{char.ToUpper(name[0]) + name.Substring(1)} agregado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    txtNombreBarrio.Text = "";
                    txtIdBarrio.Text = "";
                    txtNombreBarrio.Focus();

                }

            }

            catch (EntityAlreadyExistsException)
            {
                MessageBox.Show($"El {name} ya se encuentra registrado.");
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
            
            if (dgvBarrios.SelectedRows.Count == 1)
            {
                try
                {
                    ISimpleEntity entitySelected = (ISimpleEntity)dgvBarrios.SelectedRows[0].DataBoundItem;
                    simpleService.Delete(entitySelected.id);

                    MessageBox.Show($"{char.ToUpper(name[0]) + name.Substring(1)} eliminado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    txtNombreBarrio.Text = "";
                    txtIdBarrio.Text = "";
                    btnAgregar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnEditar.Enabled = false;
                    txtNombreBarrio.Enabled = true;
                    txtNombreBarrio.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado al intentar consultar la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Tiene que seleccionar un barrio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //Neighborhood b = GetNeighborhood.Get(txtNombreBarrio.Text);
                simpleService.Update(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNombreBarrio.Text), txtIdBarrio.Text);

                MessageBox.Show($"{char.ToUpper(name[0]) + name.Substring(1)} editado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

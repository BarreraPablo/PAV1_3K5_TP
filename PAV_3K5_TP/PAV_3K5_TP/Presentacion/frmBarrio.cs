using PAV_3K5_TP.Datos;
using PAV_3K5_TP.Negocio.Entidades;
using PAV_3K5_TP.Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K5_TP.Presentacion
{
    public partial class frmBarrio : Form
    {
        private readonly GestorBarrios gestorBarrios;

        public frmBarrio()
        {
            InitializeComponent();
            this.gestorBarrios = new GestorBarrios();

            cargarBarrios();
        }

        public void cargarBarrios()
        {
            var barrios = gestorBarrios.obtenerUsuarios();

            dgBarrios.AutoGenerateColumns = false;
            dgBarrios.DataSource = barrios;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtNombreBarrio.Text))
                {
                    MessageBox.Show("Por favor carge un nombre de barrio");
                }

                gestorBarrios.agregarBarrio(txtNombreBarrio.Text);

                cargarBarrios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un error inesperado a occurido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgBarrios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor seleccione un barrio a borrar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Barrio barrio = (Barrio)dgBarrios.SelectedRows[0].DataBoundItem;

                gestorBarrios.borrarBarrio(barrio.id_barrio);
                MessageBox.Show("El barrio se ha borrado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarBarrios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

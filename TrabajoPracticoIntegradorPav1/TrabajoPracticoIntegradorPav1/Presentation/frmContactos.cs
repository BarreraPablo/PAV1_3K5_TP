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
    public partial class frmContactos : Form
    {
        public frmContactos()
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            cargarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmContacto(frmContactoType.Crear, null))
            {
                frm.ShowDialog();
            }

            cargarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            using (var context = new tpDbContext())
            {
                IQueryable<Contacto> contactos = context.Contactos.Where(c => c.borrado == chkDadasDeBaja.Checked);

                if (!String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    contactos = contactos.Where(c => c.nombre.Contains(txtNombre.Text));
                }

            
                if (!String.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    contactos = contactos.Where(c => c.apellido.Contains(txtApellido.Text));
                }

            
                if (!String.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    contactos = contactos.Where(c => c.email.Contains(txtEmail.Text));
                }

            
                if (!String.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    contactos = contactos.Where(c => c.telefono.Contains(txtTelefono.Text));
                }

                context.Configuration.LazyLoadingEnabled = false;
                dgvContactos.AutoGenerateColumns = false;
                dgvContactos.DataSource = contactos.ToList();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvContactos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Tiene que seleccionar un contacto", "Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var contactoSeleccionado = (Contacto)dgvContactos.SelectedRows[0].DataBoundItem;

            using (var frm = new frmContacto(frmContactoType.Ver, contactoSeleccionado))
            {
                frm.ShowDialog();
            }

            cargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvContactos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Tiene que seleccionar un contacto", "Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var contactoSeleccionado = (Contacto)dgvContactos.SelectedRows[0].DataBoundItem;

            using (var frm = new frmContacto(frmContactoType.Editar, contactoSeleccionado))
            {
                frm.ShowDialog();
            }

            cargarGrilla();
        }

    }
}

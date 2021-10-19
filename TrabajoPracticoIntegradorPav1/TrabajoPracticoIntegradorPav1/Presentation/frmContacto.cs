using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public enum frmContactoType
    {
        Crear,
        Editar,
        Ver
    }
    public partial class frmContacto : Form
    {
        private Contacto Contacto;
        private frmContactoType frmModo;

        public frmContacto(frmContactoType modo, Contacto contacto)
        {
            InitializeComponent();

            frmModo = modo;

            if (modo == frmContactoType.Editar)
            {
                btnAceptar.Text = "Editar";

                this.Contacto = contacto;
                txtNombre.Text = contacto.nombre;
                txtApellido.Text = contacto.apellido;
                txtEmail.Text = contacto.email;
                txtTelefono.Text = contacto.telefono;
            } else if (modo == frmContactoType.Ver)
            {
                btnAceptar.Text = contacto.borrado.HasValue && contacto.borrado.Value ? "Dar de alta" : "Dar de baja";
                this.Contacto = contacto;

                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtEmail.Enabled = false;
                txtTelefono.Enabled = false;

                txtNombre.Text = contacto.nombre;
                txtApellido.Text = contacto.apellido;
                txtEmail.Text = contacto.email;
                txtTelefono.Text = contacto.telefono;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (!validar())
            {
                return;
            }

            switch (frmModo)
            {
                case frmContactoType.Crear:
                    crear();
                    break;
                case frmContactoType.Editar:
                    editar();
                    break;
                case frmContactoType.Ver:
                    actualizarEstado();
                    break;
                default:
                    crear();
                    break;
            }

        }

        private void actualizarEstado()
        {
            using (var context = new tpDbContext())
            {
                context.Contactos.Attach(Contacto);

                Contacto.borrado = Contacto.borrado.HasValue && Contacto.borrado.Value ? false : true;

                context.SaveChanges();
            }

            MessageBox.Show("Contacto editado con exito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void editar()
        {
            using (var context = new tpDbContext())
            {
                context.Contactos.Attach(Contacto);

                Contacto.nombre = txtNombre.Text;
                Contacto.apellido = txtApellido.Text;
                Contacto.telefono = txtTelefono.Text;
                Contacto.email = txtEmail.Text;

                context.SaveChanges();
            }

            MessageBox.Show("Contacto editado con exito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void crear()
        {
            var nuevoContacto = new Contacto();

            nuevoContacto.nombre = txtNombre.Text;
            nuevoContacto.apellido = txtApellido.Text;
            nuevoContacto.email = txtEmail.Text;
            nuevoContacto.telefono = txtTelefono.Text;
            nuevoContacto.borrado = false;

            using (var context = new tpDbContext())
            {
                context.Contactos.Add(nuevoContacto);
                context.SaveChanges();
            }

            MessageBox.Show("Contacto creado con exito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private bool validar()
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Tiene que ingresar un nombre", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            if (String.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Tiene que ingresar un apellido", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            if (String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Tiene que ingresar un mail", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            if (String.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Tiene que ingresar un telefono", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}

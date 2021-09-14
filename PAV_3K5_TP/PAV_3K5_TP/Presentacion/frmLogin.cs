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
    public partial class frmLogin : Form
    {
        private GestorUsuarios gestorUsuarios;

        public frmLogin()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            gestorUsuarios = new GestorUsuarios(); ;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!validarCampos())
            {
                return;
            }

            var usuarioLogeado = gestorUsuarios.validarUsuario(txtUsuario.Text, txtPassword.Text);

            if(usuarioLogeado)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            } else
            {
                MessageBox.Show("Usuario o password incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool validarCampos()
        {
            if (String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El campo usuario es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("El campo password es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

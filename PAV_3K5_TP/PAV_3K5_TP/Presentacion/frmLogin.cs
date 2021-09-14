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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Un error inesperado a ocurrido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var resultado = MessageBox.Show("¿Desea salir de la aplicación?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(resultado == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult != DialogResult.OK)
            {
                var resultado = MessageBox.Show("¿Desea salir de la aplicación?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    this.Dispose();
                } else
                {
                    this.DialogResult = DialogResult.None;
                }
            } 
        }
    }
}

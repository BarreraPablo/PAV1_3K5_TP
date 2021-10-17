using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer;
using TrabajoPracticoIntegradorPav1.Entities;
using TrabajoPracticoIntegradorPav1.Presentacion;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            User usu = new User(txtUser.Text, txtPassword.Text);

            try
            {
                bool result = ValidateUser.Validate(usu);
                if (result)
                {
                    frmPrincipal windows = new frmPrincipal();
                    this.Hide();
                    windows.Show();

                    
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al intentar consultar con la base de datos");
            }
        }
    }
}

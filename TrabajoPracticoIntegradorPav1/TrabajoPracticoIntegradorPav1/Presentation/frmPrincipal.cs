using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.Business;
using TrabajoPracticoIntegradorPav1.DataAccesLayer;
using TrabajoPracticoIntegradorPav1.Entities;
using TrabajoPracticoIntegradorPav1.Presentation;

namespace TrabajoPracticoIntegradorPav1.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            panelSubMenuGestion.Visible = false;
            panelSubMenuReportes.Visible = false;
        }

        private void btnGestionar_Click(object sender, EventArgs e)
        {
            if (panelSubMenuGestion.Visible == false)
            {
                panelSubMenuGestion.Visible = true;
            }
            else if (panelSubMenuGestion.Visible == true)
            {
                panelSubMenuGestion.Visible = false;
            }
            
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (panelSubMenuReportes.Visible == false)
            {
                panelSubMenuReportes.Visible = true;
            }
            else if (panelSubMenuReportes.Visible == true)
            {
                panelSubMenuReportes.Visible = false;
            }
        }

        //Metodo para agregar un formulario al formulario padre
        private void AddForm(Form f)
        {
            if (this.panelPadre.Controls.Count > 0)
            {
                this.panelPadre.Controls.RemoveAt(0);
            }
            f.TopLevel = false;
            this.panelPadre.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void btnBarrios_Click(object sender, EventArgs e)
        {
            var form = new frmSimple<NeighborhoodService, Neighborhood>("barrios");
            AddForm(form);
        }


        private void btnContactos_Click(object sender, EventArgs e)
        {
            frmContactos form = new frmContactos();
            AddForm(form);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmCliente form = new frmCliente();
            AddForm(form);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var form = new frmSimple<ProductService, Product>("productos");
            AddForm(form);
        }
    }
}

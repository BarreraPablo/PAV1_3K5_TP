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

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            panelSubMenuSoporte.Visible = false;
            panelSubMenuTransacciones.Visible = false;
            pnlSubMenuReportes.Visible = false;
        }

        private void btnSoporte_Click(object sender, EventArgs e)
        {
            if (panelSubMenuSoporte.Visible == false)
            {
                panelSubMenuSoporte.Visible = true;
            }
            else if (panelSubMenuSoporte.Visible == true)
            {
                panelSubMenuSoporte.Visible = false;
            }
            
        }

        private void btnOperaciones_Click(object sender, EventArgs e)
        {
            if (panelSubMenuTransacciones.Visible == false)
            {
                panelSubMenuTransacciones.Visible = true;
            }
            else if (panelSubMenuTransacciones.Visible == true)
            {
                panelSubMenuTransacciones.Visible = false;
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
            var form = new frmSimple<NeighborhoodService, Neighborhood>("barrio");
            AddForm(form);
        }


        private void btnContactos_Click(object sender, EventArgs e)
        {
            frmFacturas form = new frmFacturas();
            AddForm(form);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmCliente form = new frmCliente();
            AddForm(form);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var form = new frmSimple<ProductService, Product>("producto");
            AddForm(form);
        }

        private void btnContactos_Click_1(object sender, EventArgs e)
        {
            frmContactos form = new frmContactos();
            AddForm(form);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (pnlSubMenuReportes.Visible == false)
            {
                pnlSubMenuReportes.Visible = true;
            }
            else if (pnlSubMenuReportes.Visible == true)
            {
                pnlSubMenuReportes.Visible = false;
            }
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            frmFacturas form = new frmFacturas();
            AddForm(form);
        }

        private void btnProyectos_Click(object sender, EventArgs e)
        {
            frmProyecto frm = new frmProyecto();
            AddForm(frm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmReporteEstadisticaProyectos form = new frmReporteEstadisticaProyectos();
            AddForm(form);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmVolumenFacturacion form = new frmVolumenFacturacion();
            AddForm(form);
        }
    }
}

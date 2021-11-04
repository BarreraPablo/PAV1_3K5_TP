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
    public partial class frmFacturas : Form
    {
        public frmFacturas()
        {
            InitializeComponent();
            cargarCombos();
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            using (var context = new tpDbContext())
            {
                IQueryable<Factura> facturas = context.Facturas.Include("Cliente").Include("FacturasDetalle");
                DateTime fecha = new DateTime();
                var hayFechaValida = DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);

                if (hayFechaValida)
                {
                    facturas = facturas.Where(f => f.fecha.Year == fecha.Year && f.fecha.Day == fecha.Day && f.fecha.Month == fecha.Month);
                }

                if (txtNroFactura.Text.Length > 0)
                {
                    facturas = facturas.Where(f => f.numero_factura == txtNroFactura.Text);
                }

                if (txtCuit.Text.Length > 0)
                {
                    facturas = facturas.Where(f => f.Cliente.cuit == txtCuit.Text);
                }

                if ((int)cboUsuarioCreador.SelectedValue != -1)
                {
                    facturas = facturas.Where(f => f.id_usuario_creador == (int)cboUsuarioCreador.SelectedValue);
                }

                facturas = facturas.Where(f => f.borrado == chkDadasDeBaja.Checked);

                var facturasResultado = facturas.GroupBy(c => new { c.id_factura, c.numero_factura, c.Cliente.razon_social, c.fecha, c.Cliente.cuit, c.Usuario.usuario1 })
                    .Select(f => new
                    {
                        f.Key.id_factura,
                        f.Key.numero_factura,
                        f.Key.razon_social,
                        f.Key.fecha,
                        f.Key.cuit,
                        usuario = f.Key.usuario1,
                        total = f.Sum(c => c.FacturasDetalles.Select(r => r.precio).Sum())
                    }).ToList();

                var facturasListadoGrilla = facturasResultado
                    .Select(c => new { c.id_factura, numero_factura = Convert.ToInt32(c.numero_factura), c.razon_social, fecha = c.fecha.ToString("dd/MM/yyyy"), cuit = c.cuit, usuario = c.usuario, total = c.total })
                    .OrderBy(f => f.numero_factura).ToList();

                dgvFacturas.AutoGenerateColumns = false;
                context.Configuration.LazyLoadingEnabled = false;

                dgvFacturas.DataSource = facturasListadoGrilla;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmFactura(frmType.Crear, null))
            {
                frm.ShowDialog();
                cargarGrilla();
            }
        }

        private void cargarCombos()
        {
            using (var context = new tpDbContext())
            {
                var usuariosCreadores = context.Usuarios.Where(u => u.borrado != true).Prepend(new Usuario { id_usuario = -1, usuario1 = "Seleccionar" }).ToList();
                LlenarCombo(cboUsuarioCreador, usuariosCreadores, "usuario1", "id_usuario");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedValue = -1;
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Tiene que seleccionar una factura al menos");
                return;
            }

            var facturaSeleccionada = (dynamic)dgvFacturas.SelectedRows[0].DataBoundItem;

            using (var frm = new frmFactura(frmType.Eliminar, (int)facturaSeleccionada.id_factura))
            {
                frm.ShowDialog();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Tiene que seleccionar una factura al menos");
                return;
            }

            var facturaSeleccionada = (dynamic)dgvFacturas.SelectedRows[0].DataBoundItem;

            using (var frm = new frmVisualizadorFactura((int)facturaSeleccionada.id_factura))
            {
                frm.ShowDialog();
            }
        }
    }
}

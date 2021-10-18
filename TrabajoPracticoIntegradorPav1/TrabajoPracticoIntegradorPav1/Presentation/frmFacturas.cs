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
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            using (var context = new tpDbContext())
            {
                IQueryable<Factura> facturas = context.Facturas.Include("Cliente");
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

                if (cboUsuarioCreador.SelectedValue != null)
                {
                    facturas = facturas.Where(f => f.id_usuario_creador == (int)cboUsuarioCreador.SelectedValue);
                }

                var facturasResultado = facturas.GroupBy(c => new { c.numero_factura, c.Cliente.razon_social, c.fecha, c.Cliente.cuit, c.Usuario.usuario1 })
                    .Select(f => new
                    {
                        numero_factura = f.Key.numero_factura,
                        razon_social = f.Key.razon_social,
                        fecha = f.Key.fecha,
                        cuit = f.Key.cuit,
                        usuario = f.Key.usuario1,
                        total = f.Sum(c => c.FacturasDetalles.Select(r => r.precio).Sum())
                    }).ToList();

                dgvClientes.AutoGenerateColumns = false;
                context.Configuration.LazyLoadingEnabled = false;

                dgvClientes.DataSource = facturasResultado;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmFactura())
                frm.ShowDialog();
        }

        private void cargarCombos()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }
    }
}

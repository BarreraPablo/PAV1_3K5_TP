using Microsoft.Reporting.WinForms;
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
    public partial class frmVisualizadorFactura : Form
    {
        public frmVisualizadorFactura(int facturaId)
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            cargarFactura(facturaId);
        }

        private void cargarFactura(int facturaId)
        {

            var factura = new Factura();
            using(var context = new tpDbContext())
            {
                factura = context.Facturas.Include("Cliente").Include("FacturasDetalles.Producto").Include("FacturasDetalles.Proyecto").SingleOrDefault(f => f.id_factura == facturaId);
            }

            var detalleFacturas = new DataTable();
            detalleFacturas.Columns.Add("numero_orden", typeof(string));
            detalleFacturas.Columns.Add("precio", typeof(decimal));
            detalleFacturas.Columns.Add("nombre_producto", typeof(string));
            detalleFacturas.Columns.Add("descripcion_proyecto", typeof(string));

            foreach (var item in factura.FacturasDetalles)
            {
                var row = detalleFacturas.NewRow();

                row["numero_orden"] = item.numero_orden;
                row["precio"] = item.precio;
                row["nombre_producto"] = item.Producto != null ? item.Producto.nombre : "";
                row["descripcion_proyecto"] = item.Proyecto != null ? item.Proyecto.descripcion : "";

                detalleFacturas.Rows.Add(row);
            }

            var parameters = new List<ReportParameter>
            {
                new ReportParameter("pCUIT", factura.Cliente.cuit),
                new ReportParameter("pRAZONSOCIAL", factura.Cliente.razon_social),
                new ReportParameter("pFECHA", factura.fecha.ToString()),
                new ReportParameter("pDOMICILIO", factura.Cliente.calle + " " +
                 factura.Cliente.numero),
                new ReportParameter("pNUMEROFACTURA", factura.numero_factura)
            };

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dtDetalle", detalleFacturas));
            this.reportViewer1.LocalReport.SetParameters(parameters);

            this.reportViewer1.RefreshReport();
        }

        private void frmVisualizadorFactura_Load(object sender, EventArgs e)
        {


        }
    }
}

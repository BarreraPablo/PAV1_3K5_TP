using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmVolumenFacturacion : Form
    {
        public frmVolumenFacturacion()
        {
            InitializeComponent();
            obtenerClientes();
            obtenerData();
        }

        private void frmVolumenFacturacion_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            obtenerData();
        }

        private void obtenerClientes()
        {
            using(var context = new tpDbContext())
            {
                var clientes = context.Clientes.Where(c => c.borrado != true).ToList();

                clientes = clientes.Prepend(new Cliente
                {
                    id_cliente = -1,
                    razon_social = "Seleccionar"
                }).ToList();

                LlenarCombo(cboCliente, clientes, "razon_social", "id_cliente");
            }
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

        public void obtenerData()
        {
            var clienteIdSeleccionado = (int)cboCliente.SelectedValue;

            var fechaDesde = new DateTime();
            var hayFechaDesdeValida = DateTime.TryParseExact(txtFechaDesde.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaDesde);

            var fechaHasta = new DateTime();
            var hayFechaHastaValida = DateTime.TryParseExact(txtFechaHasta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHasta);


            using (var context = new tpDbContext())
            {
                var query = context.Facturas.AsQueryable().Include(f => f.FacturasDetalles).Include(f => f.Cliente);

                if(clienteIdSeleccionado != -1)
                {
                    query = query.Where(f => f.id_cliente == clienteIdSeleccionado);
                }

                if (hayFechaDesdeValida)
                {
                    query = query.Where(f => f.fecha >= fechaDesde && f.fecha >= fechaDesde && f.fecha >= fechaDesde);
                }

                if (hayFechaHastaValida)
                {
                    query = query.Where(f => f.fecha <= fechaHasta && f.fecha <= fechaHasta && f.fecha <= fechaHasta);
                };

                query = query.OrderBy(f => f.fecha.Month);

                var resultados = query.ToList().Select(factura =>
                new
                {
                    nombre_mes = factura.fecha.ToString("yy") + "/" + factura.fecha.ToString("MMMM", new CultureInfo("es-ES")).FirstCharToUpper(),
                    cliente_nombre = factura.Cliente.razon_social,
                    fecha_numero_mes = Int32.Parse(factura.fecha.Year.ToString() + factura.fecha.Month.ToString()),
                    fecha = factura.fecha.ToString("D", new CultureInfo("es-ES")),
                    total = factura.FacturasDetalles.Select(f => f.precio).Sum(),
                    numero_factura = factura.numero_factura
                });


                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("TotalFacturado", resultados));

                this.reportViewer1.RefreshReport();
            }
        }
    }
}

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}

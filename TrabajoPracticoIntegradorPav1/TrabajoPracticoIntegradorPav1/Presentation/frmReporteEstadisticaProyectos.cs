using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer.Statiscal;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmReporteEstadisticaProyectos : Form
    {
        public frmReporteEstadisticaProyectos()
        {
            InitializeComponent();
            cargarCombos();
            cargarReporte();
        }

        private void cargarCombos()
        {
            using(var context = new tpDbContext())
            {
                var query = context.Usuarios.Where(u => u.borrado != true);

                var Usuarios = query.ToList();

                Usuarios = Usuarios.Prepend(new Usuario
                {
                    id_usuario = -1,
                    usuario1 = "Seleccionar"
                }).ToList();

                LlenarCombo(cboResponsable, Usuarios, "usuario1", "id_usuario");
            }
        }

        private void cargarReporte()
        {
            var responsableSeleccionado = (int)cboResponsable.SelectedValue;

            using (var context = new tpDbContext())
            {
                var query = context.Proyectos.Include(p => p.Usuario).Where(p => p.borrado == null);

                if(responsableSeleccionado != -1)
                {
                    query = query.Where(p => p.id_responsable == responsableSeleccionado);
                }

                var response = query.Select(p => new
                {
                    nombre_responsable = p.Usuario.usuario1,
                    proyecto_descripcion = p.descripcion,
                    nombre_producto = p.Producto.nombre,
                    version = p.version,
                    alcance = p.alcance

                }).ToList();

                ReportDataSource ds = new ReportDataSource("DatosEstadisticaProyectos", response);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);
                reportViewer1.RefreshReport();
            }

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            cargarReporte();
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

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}

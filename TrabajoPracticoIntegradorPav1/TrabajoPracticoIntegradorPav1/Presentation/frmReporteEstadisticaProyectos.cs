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
using TrabajoPracticoIntegradorPav1.DataAccesLayer.Statiscal;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public partial class frmReporteEstadisticaProyectos : Form
    {
        public frmReporteEstadisticaProyectos()
        {
            InitializeComponent();
        }

        private void frmReporteEstadisticaProyectos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = GetProjectsForUser.ObtenerTabla();

            ReportDataSource ds = new ReportDataSource("DatosEstadisticaProyectos", table);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }
    }
}

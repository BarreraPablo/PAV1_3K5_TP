using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoPracticoIntegradorPav1.DataAccesLayer;
using TrabajoPracticoIntegradorPav1.Entities;

namespace TrabajoPracticoIntegradorPav1.Presentation
{
    public enum frmType
    {
        Crear,
        Eliminar
    }
    public partial class frmFactura : Form
    {
        private Factura facturaActual;
        private DataTable dtDetalle;
        bool fireEvents = false;
        public frmFactura(frmType modo, int? facturaId)
        {
            InitializeComponent();
            cargarCombos();
            inicializarTabla();
            fireEvents = true;
            txtPrecio.Minimum = 1;
            txtPrecio.Maximum = 15000;

            if (modo == frmType.Eliminar)
            {
                using (var context = new tpDbContext())
                {
                    facturaActual = context.Facturas.Include("FacturasDetalles").Include("FacturasDetalles.Proyecto").Include("FacturasDetalles.Producto").First(f => f.id_factura == facturaId);
                    cbCliente.SelectedValue = facturaActual.id_cliente;
                    txtFecha.Text = facturaActual.fecha.ToString("dd/MM/yyyy");

                    foreach (var detalle in facturaActual.FacturasDetalles)
                    {
                        var drDetalle = dtDetalle.NewRow();
                        drDetalle["numero_orden"] = detalle.numero_orden;
                        drDetalle["precio"] = detalle.precio;
                        drDetalle["nombre_producto"] = detalle.Producto != null ? detalle.Producto.nombre : null;
                        drDetalle["descripcio_proyecto"] = detalle.Proyecto != null ? detalle.Proyecto.descripcion : null;
                        drDetalle["id_producto"] = detalle.id_producto;
                        drDetalle["id_proyecto"] = detalle.id_proyecto;


                        dtDetalle.Rows.Add(drDetalle);

                    }

                    cbCliente.Enabled = false;
                    txtFecha.Enabled = false;

                    cbProducto.Enabled = false;
                    cbProyecto.Enabled = false;

                    txtPrecio.Enabled = false;

                    btnAgregar.Enabled = false;
                    btnEliminar.Enabled = false;

                    btnBaja.Visible = true;
                    btnFacturar.Visible = false;
                }
            }
        }

        private void inicializarTabla()
        {
            dtDetalle = new DataTable();
            dtDetalle.Columns.Add("numero_orden");
            dtDetalle.Columns.Add("precio");
            dtDetalle.Columns.Add("nombre_producto");
            dtDetalle.Columns.Add("descripcio_proyecto");
            dtDetalle.Columns.Add("id_producto");
            dtDetalle.Columns.Add("id_proyecto");

            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.DataSource = dtDetalle;
        }

        private void resetCombos()
        {
            cbProducto.SelectedItem = null;
            cbProducto.SelectedIndex = -1;
            cbProducto.Enabled = true;
            cbProyecto.SelectedItem = null;
            cbProyecto.Enabled = true;
            cbProyecto.SelectedIndex = -1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!validarAgregar())
                return;

            var productoSeleccionado = (Producto)cbProducto.SelectedItem;
            var proyectoSeleccionado = (Proyecto)cbProyecto.SelectedItem;

            var drDetalle = dtDetalle.NewRow();
            drDetalle["numero_orden"] = dtDetalle.Rows.Count + 1;
            drDetalle["precio"] = txtPrecio.Text;
            drDetalle["nombre_producto"] = productoSeleccionado != null ? productoSeleccionado.nombre : null;
            drDetalle["descripcio_proyecto"] = proyectoSeleccionado != null ? proyectoSeleccionado.descripcion : null;
            drDetalle["id_producto"] = productoSeleccionado != null ? productoSeleccionado.id_producto : (int?)null;
            drDetalle["id_proyecto"] = proyectoSeleccionado != null ? proyectoSeleccionado.id_proyecto : (int?)null;


            dtDetalle.Rows.Add(drDetalle);
            dgvDetalle.Refresh();

            resetCombos();
        }

        private bool validarAgregar()
        {
            if(cbProducto.SelectedIndex == -1 && cbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Tiene que seleccionar un producto o un proyecto", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void cargarCombos()
        {
            using(var context = new tpDbContext())
            {
                var clientes = context.Clientes.Where(c => c.borrado != true).ToList();
                var productos = context.Productos.Where(c => c.borrado != true).ToList();
                var proyectos = context.Proyectos.Where(c => c.borrado != null).ToList();

                LlenarCombo(cbProducto, productos, "nombre", "id_producto");
                LlenarCombo(cbCliente, clientes, "razon_social", "id_cliente");
                LlenarCombo(cbProyecto, proyectos, "descripcion", "id_proyecto");
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
            cbo.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(dgvDetalle.SelectedRows.Count == 0)
            {
                return;
            }
            var filaEliminada = ((DataRowView)dgvDetalle.SelectedRows[0].DataBoundItem).Row;

            dtDetalle.Rows.Remove(filaEliminada);

            var index = 1;
            foreach (DataRow item in dtDetalle.Rows)
            {
                item["numero_orden"] = index;
                index++;
            }
            dgvDetalle.Refresh();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (!validarFacturar())
                return;

            var factura = new Factura();
            var detalleFacturas = new List<FacturasDetalle>();
            factura.id_cliente = int.Parse(cbCliente.SelectedValue.ToString());
            factura.numero_factura = new Random().Next(5, 6000).ToString();
            factura.fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            factura.id_usuario_creador = ValidateUser.UsuarioLogeado.id_usuario;
            factura.borrado = false;
            factura.FacturasDetalles = detalleFacturas;


            foreach (DataRow row in dtDetalle.Rows)
            {
                var detalleFactura = new FacturasDetalle();
                detalleFactura.id_proyecto = row["id_proyecto"].ToString().ToNullableInt();
                detalleFactura.id_producto = row["id_producto"].ToString().ToNullableInt();
                detalleFactura.precio = int.Parse(row["precio"].ToString());
                detalleFactura.borrado = false;
                detalleFactura.numero_orden = int.Parse(row["numero_orden"].ToString());
                detalleFacturas.Add(detalleFactura);
            }

            using(var context = new tpDbContext())
            {
                context.Facturas.Add(factura);
                context.SaveChanges();
            }

            MessageBox.Show("Facturación realizada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private bool validarFacturar()
        {
            if(dtDetalle.Rows.Count < 1)
            {
                MessageBox.Show("Debe cargar por lo menos un detalle", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(cbCliente.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime fecha = new DateTime();
            var hayFechaValida = DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);

            if (!hayFechaValida)
            {
                MessageBox.Show("Ingrese una fecha valida, formato dd/mm/aaaa", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(fecha < DateTime.Now.AddDays(-10))
            {
                MessageBox.Show("La fecha de la factura no puede ser menor a 10 dias", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void cbProyecto_SelectedValueChanged(object sender, EventArgs e)
        {
            if(fireEvents)
                cbProducto.Enabled = false;
        }

        private void cbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            if(fireEvents)
                cbProyecto.Enabled = false;
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            using (var context = new tpDbContext())
            {
                context.Facturas.Attach(facturaActual);

                facturaActual.borrado = true;

                context.SaveChanges();
            }

            MessageBox.Show("La factura a sido borrada con exito", "Atencion", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }
    }
}

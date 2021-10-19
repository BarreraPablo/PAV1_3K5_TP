namespace TrabajoPracticoIntegradorPav1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacturasDetalle")]
    public partial class FacturasDetalle
    {
        [Key]
        public int id_detalle_factura { get; set; }

        public int? id_factura { get; set; }

        public int? numero_orden { get; set; }

        public int? id_producto { get; set; }

        public int? id_proyecto { get; set; }

        public int? id_ciclo_prueba { get; set; }

        public decimal? precio { get; set; }

        public bool? borrado { get; set; }

        public virtual Factura Factura { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Proyecto Proyecto { get; set; }
    }
}

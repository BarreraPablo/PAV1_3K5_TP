namespace TrabajoPracticoIntegradorPav1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            FacturasDetalles = new HashSet<FacturasDetalle>();
        }

        [Key]
        public int id_factura { get; set; }

        [Required]
        [StringLength(50)]
        public string numero_factura { get; set; }

        public int id_cliente { get; set; }

        public DateTime fecha { get; set; }

        public int id_usuario_creador { get; set; }

        public bool borrado { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacturasDetalle> FacturasDetalles { get; set; }
    }
}

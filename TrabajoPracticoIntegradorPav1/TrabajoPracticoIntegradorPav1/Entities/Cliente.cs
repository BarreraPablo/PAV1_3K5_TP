namespace TrabajoPracticoIntegradorPav1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        [Key]
        public int id_cliente { get; set; }

        [StringLength(50)]
        public string cuit { get; set; }

        [StringLength(50)]
        public string razon_social { get; set; }

        public bool? borrado { get; set; }

        [StringLength(500)]
        public string calle { get; set; }

        [StringLength(50)]
        public string numero { get; set; }

        public DateTime? fecha_alta { get; set; }

        public int? id_barrio { get; set; }

        public int? id_contacto { get; set; }

        public virtual Barrio Barrio { get; set; }

        public virtual Contacto Contacto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}

namespace TrabajoPracticoIntegradorPav1.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Facturas = new HashSet<Factura>();
            Proyectos = new HashSet<Proyecto>();
        }

        [Key]
        public int id_usuario { get; set; }

        public int id_perfil { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(50)]
        public string usuario1 { get; set; }

        [Required]
        [StringLength(10)]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public bool? borrado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Facturas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}

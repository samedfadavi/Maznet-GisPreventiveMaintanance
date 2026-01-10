namespace pmService.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Moshtari
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Moshtari()
        {
            Tbl_Sorathesab = new HashSet<Tbl_Sorathesab>();
        }

        [Key]
        public int Code_Moshtari { get; set; }

        public byte? Noe_Moshtari { get; set; }

        [StringLength(250)]
        public string Name_Moshtari { get; set; }

        [StringLength(15)]
        public string Shenase_Melli { get; set; }

        [StringLength(10)]
        public string Shomare_Sabt { get; set; }

        [StringLength(10)]
        public string Code_Posti { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Code_Shobe { get; set; }

        [StringLength(15)]
        public string Code_Eghresadi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Tbl_Sorathesab> Tbl_Sorathesab { get; set; }
    }
}

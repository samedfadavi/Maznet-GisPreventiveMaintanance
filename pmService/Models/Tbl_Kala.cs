namespace pmService.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Kala
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Kala()
        {
            Tbl_Sorathesab = new HashSet<Tbl_Sorathesab>();
        }

        [Key]
        public int Code_Kala { get; set; }

        [StringLength(500)]
        public string Onvan { get; set; }

        [StringLength(15)]
        public string Shenase_Kala { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Tbl_Sorathesab> Tbl_Sorathesab { get; set; }
    }
}

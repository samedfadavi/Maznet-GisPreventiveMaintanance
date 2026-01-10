namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Sorathesab
    {
        [Key]
        public int ID_Sorathesab { get; set; }

        public int? Code_Moshtari { get; set; }

        public int? Code_Kala { get; set; }

        [StringLength(12)]
        public string Tarikh { get; set; }

        public decimal? Mablagh { get; set; }

        [StringLength(500)]
        public string Tozihat { get; set; }

        public byte? Vazeiat { get; set; }

        [StringLength(50)]
        public string Shenase { get; set; }

        public virtual Tbl_Kala Tbl_Kala { get; set; }

        public virtual Tbl_Moshtari Tbl_Moshtari { get; set; }
    }
}

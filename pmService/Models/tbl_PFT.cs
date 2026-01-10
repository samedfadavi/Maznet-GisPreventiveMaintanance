namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PFT
    {
        [Key]
        public int Code_PFT { get; set; }

        [StringLength(50)]
        public string Name_PFT { get; set; }

        public int? Omoor { get; set; }

        [StringLength(50)]
        public string Ghodrat_PFT { get; set; }

        [StringLength(50)]
        public string Date_PFT { get; set; }

        [StringLength(50)]
        public string Address_PFT { get; set; }

        [StringLength(50)]
        public string Tozihat_PFT { get; set; }

        [StringLength(50)]
        public string X_PFT { get; set; }

        [StringLength(50)]
        public string Y_PFT { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        [StringLength(50)]
        public string Map_cod_ol { get; set; }
    }
}

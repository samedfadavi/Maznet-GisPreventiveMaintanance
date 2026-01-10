namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Omoor
    {
        [Key]
        public int Code_Omoor { get; set; }

        public int? id { get; set; }

        public int? Ostan { get; set; }

        public int? Shahr { get; set; }

        [StringLength(50)]
        public string Name_Omoor { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        public int? Daraje_Omoor { get; set; }

        public int? Id_Gis { get; set; }

        public int? code_moshtarekin { get; set; }

        public int? code_121 { get; set; }

        public double? lvr { get; set; }
    }
}

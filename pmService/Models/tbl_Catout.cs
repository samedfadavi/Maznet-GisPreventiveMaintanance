namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Catout
    {
        [Key]
        public int Code_Catout { get; set; }

        [StringLength(150)]
        public string Name_Catout { get; set; }

        public int? Code_Parent { get; set; }

        public int? CO { get; set; }

        [StringLength(50)]
        public string ANE { get; set; }

        [StringLength(50)]
        public string ANK { get; set; }

        [StringLength(50)]
        public string Jaryan { get; set; }

        public int? Noe { get; set; }

        [StringLength(50)]
        public string X { get; set; }

        [StringLength(50)]
        public string Y { get; set; }

        [StringLength(50)]
        public string Serial { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        public int? hozeh { get; set; }

        [StringLength(200)]
        public string Sazande { get; set; }

        [StringLength(20)]
        public string Sal_Sakht { get; set; }
    }
}

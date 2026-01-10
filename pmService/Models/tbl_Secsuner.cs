namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Secsuner
    {
        [Key]
        public int Code_Secsuner { get; set; }

        [StringLength(250)]
        public string Name_Secsuner { get; set; }

        public int? Code_Parent { get; set; }

        public int? PT { get; set; }

        public int? CO { get; set; }

        public int? NoeSecsuner { get; set; }

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

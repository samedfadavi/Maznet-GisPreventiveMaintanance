namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Trance
    {
        [Key]
        public int Code_Trance { get; set; }

        [StringLength(50)]
        public string Name_Trance { get; set; }

        [StringLength(50)]
        public string Serial { get; set; }

        public int? Code_Parent { get; set; }

        [StringLength(50)]
        public string globalid { get; set; }

        [StringLength(200)]
        public string sazande { get; set; }

        [StringLength(20)]
        public string sale_sakht { get; set; }

        [StringLength(20)]
        public string sale_nasb { get; set; }

        public int? zarfiat { get; set; }

        public int? hozeh { get; set; }
    }
}

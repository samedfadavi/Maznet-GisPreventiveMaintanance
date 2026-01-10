namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Tablo
    {
        [Key]
        public int Code_Tablo { get; set; }

        [StringLength(100)]
        public string Name_Tablo { get; set; }

        public int? Code_Parent { get; set; }

        public int? PT { get; set; }

        public int? CO { get; set; }

        [StringLength(50)]
        public string GKelid_Tablo { get; set; }

        [StringLength(50)]
        public string CT_Tablo { get; set; }

        public byte? Connect_Tablo { get; set; }

        [StringLength(50)]
        public string Date_Tablo { get; set; }

        [StringLength(50)]
        public string TedadTablo_Tablo { get; set; }

        [StringLength(50)]
        public string TedadCell_Tablo { get; set; }

        [StringLength(50)]
        public string Maqta_Tablo { get; set; }

        [StringLength(50)]
        public string Serial { get; set; }

        [StringLength(50)]
        public string Ertelektriki { get; set; }

        [StringLength(50)]
        public string Erthefazati { get; set; }

        [StringLength(50)]
        public string globalid { get; set; }

        [StringLength(200)]
        public string sazande { get; set; }

        [StringLength(20)]
        public string sale_sakht { get; set; }

        [StringLength(50)]
        public string sale_nasb { get; set; }

        public int? hozeh { get; set; }
    }
}

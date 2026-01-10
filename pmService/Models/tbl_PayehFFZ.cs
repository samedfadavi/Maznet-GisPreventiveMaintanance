namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PayehFFZ
    {
        public int? Code_FFZ { get; set; }

        [Key]
        public int Code_Payeh { get; set; }

        [StringLength(250)]
        public string NamePayeh { get; set; }

        [StringLength(2000)]
        public string Address_Payeh { get; set; }

        [StringLength(50)]
        public string Tozihat_Payeh { get; set; }

        [StringLength(50)]
        public string X_Payeh { get; set; }

        [StringLength(50)]
        public string Y_Payeh { get; set; }

        [StringLength(20)]
        public string Noe_Payeh { get; set; }

        [StringLength(10)]
        public string Keshesh { get; set; }

        [StringLength(50)]
        public string Ertefa { get; set; }

        public int? hozeh { get; set; }

        [StringLength(50)]
        public string MAPCODE { get; set; }

        [StringLength(10)]
        public string sal_sakht { get; set; }

        [StringLength(10)]
        public string sal_nasb { get; set; }

        [StringLength(50)]
        public string Sazande { get; set; }

        public short? vaz_paye { get; set; }

        public short? Earth { get; set; }

        public int? mgh_erth { get; set; }

        [StringLength(50)]
        public string noe_fondasoun { get; set; }

        public short? Jelobar { get; set; }

        public short? noe_arayesh_paye { get; set; }

        public short? noe_kar { get; set; }

        [StringLength(50)]
        public string GlobalID_Old { get; set; }

        [StringLength(50)]
        public string GlobalID_1 { get; set; }

        [StringLength(50)]
        public string GlobalID_2 { get; set; }

        public int? noe_cheragh { get; set; }

        public int? tavan { get; set; }

        public double? tole_shabake_moshajjar { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        public int? hazf { get; set; }
    }
}

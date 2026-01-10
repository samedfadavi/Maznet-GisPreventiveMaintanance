namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PayehFFM
    {
        public int? Code_FFM { get; set; }

        [Key]
        public int Code_Payeh { get; set; }

        [StringLength(500)]
        public string NamePayeh { get; set; }

        [StringLength(500)]
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

        public short? noe_arayesh { get; set; }

        public short? sazande { get; set; }

        public short? vaz_paye { get; set; }

        public short? earth { get; set; }

        public int? mgh_erth { get; set; }

        [StringLength(50)]
        public string noe_fondasoun { get; set; }

        public short? jelobar { get; set; }

        public int? num_madar_payeh { get; set; }

        public int? sal_nasb { get; set; }

        [StringLength(100)]
        public string tedad_mghr { get; set; }

        public short? noe_kar { get; set; }

        [StringLength(50)]
        public string GlobalID_Old { get; set; }

        public double? emtiaz { get; set; }

        public double? EmtiazSharayetOmomi { get; set; }

        public double? EmtiazSharayetKhamoshi { get; set; }

        public double? EmtiazSharayetBargiri { get; set; }

        public double? EmtiazSharayetDoreBazdid { get; set; }

        public long? GisObjectId { get; set; }

        public int? Code_Payeh_121 { get; set; }

        public double? tole_shabake_moshajjar { get; set; }

        [StringLength(50)]
        public string GlobalID_1 { get; set; }

        [StringLength(50)]
        public string GlobalID_2 { get; set; }

        [StringLength(50)]
        public string GlobalID_3 { get; set; }

        public int? noe_cheragh { get; set; }

        public int? tavan { get; set; }

        public double? tole_shabake_dargir { get; set; }

        [StringLength(20)]
        public string Code_Section_Gis { get; set; }

        public long? Code_Section_121 { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        public int? hazf { get; set; }

        //public DbGeometry location { get; set; }
    }
}

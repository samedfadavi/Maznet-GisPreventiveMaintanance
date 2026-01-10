namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_QFFM
    {
        [Key]
        public int Code_QFFM { get; set; }

        public int? Fider { get; set; }

        public int? Bakhsh { get; set; }

        [StringLength(50)]
        public string NMasir_QFFM { get; set; }

        [StringLength(50)]
        public string AddressM_QFFM { get; set; }

        public int? Moshajjar_QFFM { get; set; }

        [StringLength(50)]
        public string JMasir_QFFM { get; set; }

        [StringLength(50)]
        public string EbMasir_QFFM { get; set; }

        [StringLength(50)]
        public string EnMasir_QFFM { get; set; }

        public byte? NShabakeh_QFFM { get; set; }

        public int? TKMasir_QFFM { get; set; }

        public decimal? Havaee_QFFM { get; set; }

        public decimal? Zamini_QFFM { get; set; }

        [StringLength(50)]
        public string Xsh_QFFM { get; set; }

        [StringLength(50)]
        public string Ysh_QFFM { get; set; }

        [StringLength(50)]
        public string Xp_QFFM { get; set; }

        [StringLength(50)]
        public string Yp_QFFM { get; set; }

        public int? NoeHadi { get; set; }

        public int? Maqta { get; set; }

        public int? hozeh { get; set; }

        public double? emtiaz { get; set; }

        public int? ZamanRaftoBargasht { get; set; }

        public double? EmtiazSharayetOmomi { get; set; }

        public double? EmtiazSharayetKhamoshi { get; set; }

        public double? EmtiazSharayetBargiri { get; set; }

        public double? EmtiazSharayetDoreBazdid { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }
    }
}

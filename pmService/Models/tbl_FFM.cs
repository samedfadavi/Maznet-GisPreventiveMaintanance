namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_FFM
    {
        [Key]
        public int Code_FFM { get; set; }

        public int? Bakhsh { get; set; }

        public int? PFT { get; set; }

        [StringLength(50)]
        public string Date_FFM { get; set; }

        public int? TFider_FFM { get; set; }

        public decimal? Havaee_FFM { get; set; }

        public decimal? Zamini_FFM { get; set; }

        [StringLength(50)]
        public string ANami_FFM { get; set; }

        [StringLength(50)]
        public string MFider_FFM { get; set; }

        [StringLength(50)]
        public string BarS_FFM { get; set; }

        [StringLength(50)]
        public string Diagram_FFM { get; set; }

        public byte? NFider_FFM { get; set; }

        public decimal? Ghati_FFM { get; set; }

        [StringLength(50)]
        public string X_FFM { get; set; }

        [StringLength(50)]
        public string Y_FFM { get; set; }

        public int? noemasraf_ffm { get; set; }

        public int? zamanbandi { get; set; }

        public int? hozeh { get; set; }

        [StringLength(50)]
        public string MAPCODE { get; set; }

        [StringLength(50)]
        public string SoholateDastresi { get; set; }

        public int? DomadareBa { get; set; }

        public int? DomadareBa2 { get; set; }

        public int? MetrajDomadareBa1 { get; set; }

        public int? MetrajDomadareBa2 { get; set; }

        public bool? FiderHasas { get; set; }

        [StringLength(50)]
        public string Name_Fider { get; set; }

        [StringLength(50)]
        public string Shomareh_Fider { get; set; }

        [StringLength(50)]
        public string BarS_Fider { get; set; }

        [StringLength(50)]
        public string SPBar_Fider { get; set; }

        public byte? KFider_Fider { get; set; }

        [StringLength(50)]
        public string TZsef_Fider { get; set; }

        [StringLength(50)]
        public string TZef_Fider { get; set; }

        [StringLength(50)]
        public string TZoc_Fider { get; set; }

        [StringLength(50)]
        public string THsef_Fider { get; set; }

        [StringLength(50)]
        public string THef_Fider { get; set; }

        [StringLength(50)]
        public string THoc_Fider { get; set; }

        [StringLength(50)]
        public string NTsef_Fider { get; set; }

        [StringLength(50)]
        public string NTef_Fider { get; set; }

        [StringLength(50)]
        public string NToc_Fider { get; set; }

        [StringLength(50)]
        public string GlobalID { get; set; }

        public int? Code_121 { get; set; }
    }
}

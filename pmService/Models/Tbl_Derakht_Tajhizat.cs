namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Derakht_Tajhizat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? Bazdid { get; set; }

        public int? Parent_tajhiz { get; set; }

        [StringLength(250)]
        public string Sharh { get; set; }

        public int? Code_No_DerakhtTajhizat { get; set; }

        [StringLength(250)]
        public string Name_Layeh_Gis { get; set; }

        [StringLength(250)]
        public string Name_Jadval_Pm { get; set; }

        [StringLength(250)]
        public string Code_Pm { get; set; }

        [StringLength(250)]
        public string Name_Pm { get; set; }

        [StringLength(250)]
        public string Field_Rabet { get; set; }

        public int? image { get; set; }

        public int? Goroh_Tajhiz { get; set; }
    }
}

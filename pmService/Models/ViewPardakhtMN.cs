namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewPardakhtMN")]
    public partial class ViewPardakhtMN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Idrow { get; set; }

        public string malek { get; set; }

        [StringLength(20)]
        public string codemeli { get; set; }

        public int? codeitem { get; set; }

        public string onvanitem { get; set; }

        public decimal? pardakhti { get; set; }

        [StringLength(50)]
        public string datepardakht { get; set; }

        [StringLength(50)]
        public string shenasehghabz { get; set; }

        [StringLength(50)]
        public string shomarepeygiri { get; set; }

        public int? state { get; set; }
    }
}

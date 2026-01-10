namespace pmService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PT
    {
        [Key]
        public int Code_PT { get; set; }

        [StringLength(2000)]
        public string Name_PT { get; set; }

        public int? QFFM { get; set; }

        public int? Masir20 { get; set; }

        [StringLength(50)]
        public string Ghodrat_PT { get; set; }

        public int? NoeP_PT { get; set; }

        public short? Karbari_PT { get; set; }

        public short? Karbari_PT_old { get; set; }

        [StringLength(50)]
        public string Date_PT { get; set; }

        [StringLength(2000)]
        public string Address_PT { get; set; }

        [StringLength(50)]
        public string X_PT { get; set; }

        public double? Y_PT { get; set; }

        [StringLength(50)]
        public string SakhtemanPost_PT { get; set; }

        [StringLength(50)]
        public string NoeSystemZamin_PT { get; set; }

        [StringLength(50)]
        public string Moqavemqt_PT { get; set; }

        [StringLength(50)]
        public string Boof_PT { get; set; }

        [StringLength(50)]
        public string RTU_PT { get; set; }

        [StringLength(50)]
        public string NoeTahviye_PT { get; set; }

        [StringLength(50)]
        public string NoeMasraf_PT { get; set; }

        [StringLength(50)]
        public string MoshakhaseKelid_PT { get; set; }

        [StringLength(50)]
        public string VaziyatErtebat_PT { get; set; }

        [StringLength(50)]
        public string ChaheNol_PT { get; set; }

        [StringLength(50)]
        public string ChaheBarq_PT { get; set; }

        [StringLength(50)]
        public string KhateGarm_PT { get; set; }

        [StringLength(50)]
        public string Shserial_Pt { get; set; }

        public double? emtiaz { get; set; }

        public int? zamanbandi { get; set; }

        public int? hozeh { get; set; }

        [StringLength(50)]
        public string MAPCODE { get; set; }

        [StringLength(254)]
        public string Pelak { get; set; }

        [StringLength(50)]
        public string SoholateDastresi { get; set; }

        [StringLength(50)]
        public string Ghodrat_PT2 { get; set; }

        [StringLength(50)]
        public string Ghodrat_PT3 { get; set; }

        [StringLength(50)]
        public string Ghodrat_PT4 { get; set; }

        public int? ZamanRaftoBargasht { get; set; }

        public double? EmtiazSharayetBargiri { get; set; }

        public double? EmtiazSharayetKhamoshi { get; set; }

        public double? EmtiazSharayetOmomi { get; set; }

        public double? EmtiazSharayetDoreBazdid { get; set; }

        [StringLength(50)]
        public string GlobalID_Old { get; set; }

        public int? ZamanVoghPik { get; set; }

        public long? GisObjectId { get; set; }

        public int? Code_Pt_121 { get; set; }

        [StringLength(50)]
        public string GlobalID_1 { get; set; }

        [StringLength(50)]
        public string GlobalID_2 { get; set; }

        [StringLength(50)]
        public string GlobalID_3 { get; set; }

        public double? Olaviat_Kharabi { get; set; }

        public int? SalSakht { get; set; }

        public double? count_tfz1 { get; set; }

        [StringLength(50)]
        public string Pelak_121_New { get; set; }

        public double? RCA_JaryanNami { get; set; }

        public bool? RCA_Barghgir { get; set; }

        public double? RCA_JaryanKelid { get; set; }

        public bool? RCA_VaziatSelikajol { get; set; }

        public double? RCA_Andaze_Ert { get; set; }

        public double? RCA_TanzimKelid { get; set; }

        public bool? RCA_KablBaghgir { get; set; }

        public double? RCA_BarPik { get; set; }

        public double? RCA_BarBahrebardari { get; set; }

        public double? RCA_KeshvarSazande { get; set; }

        public double? count_tfz2 { get; set; }

        [StringLength(20)]
        public string Code_Section_Gis { get; set; }

        public long? Code_Section_121 { get; set; }

        [StringLength(50)]
        public string T_ID_CODE { get; set; }

        public double? Add_Bargiri { get; set; }

        [StringLength(70)]
        public string GlobalID { get; set; }

        public int? hazf { get; set; }

   

        public int? ezafe { get; set; }
    }
}

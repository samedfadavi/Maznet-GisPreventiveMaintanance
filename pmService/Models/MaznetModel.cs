using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace pmService.Models
{
    public partial class MaznetModel : DbContext
    {
        public MaznetModel()
            : base("name=MaznetModel")
        {
        }
        public virtual DbSet<tbl_Catout> tbl_Catout { get; set; }
        public virtual DbSet<tbl_FFM> tbl_FFM { get; set; }
        public virtual DbSet<tbl_Omoor> tbl_Omoor { get; set; }
        public virtual DbSet<tbl_PayehFFM> tbl_PayehFFM { get; set; }
        public virtual DbSet<tbl_PayehFFZ> tbl_PayehFFZ { get; set; }
        public virtual DbSet<tbl_PFT> tbl_PFT { get; set; }
        public virtual DbSet<tbl_PT> tbl_PT { get; set; }
        public virtual DbSet<tbl_QFFM> tbl_QFFM { get; set; }
        public virtual DbSet<tbl_Secsuner> tbl_Secsuner { get; set; }
        public virtual DbSet<tbl_Tablo> tbl_Tablo { get; set; }
        public virtual DbSet<tbl_Trance> tbl_Trance { get; set; }

        public virtual DbSet<Tbl_Derakht_Tajhizat> Tbl_Derakht_Tajhizat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_FFM>()
                .Property(e => e.Havaee_FFM)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tbl_FFM>()
                .Property(e => e.Zamini_FFM)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tbl_FFM>()
                .Property(e => e.Ghati_FFM)
                .HasPrecision(8, 4);

            modelBuilder.Entity<tbl_Omoor>()
                .Property(e => e.GlobalID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_QFFM>()
                .Property(e => e.Havaee_QFFM)
                .HasPrecision(18, 3);

            modelBuilder.Entity<tbl_QFFM>()
                .Property(e => e.Zamini_QFFM)
                .HasPrecision(18, 3);
        }


    }
}

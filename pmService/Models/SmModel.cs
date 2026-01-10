namespace pmService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SmModel : DbContext
    {
        public SmModel()
            : base("name=SmModel")
        {
        }
        public virtual DbSet<Tbl_Kala> Tbl_Kala { get; set; }
        public virtual DbSet<Tbl_Moshtari> Tbl_Moshtari { get; set; }
        public virtual DbSet<Tbl_Sorathesab> Tbl_Sorathesab { get; set; }

        public virtual DbSet<ViewPardakhtMN> ViewPardakhtMNs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewPardakhtMN>()
                .Property(e => e.Idrow)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ViewPardakhtMN>()
                .Property(e => e.pardakhti)
                .HasPrecision(18, 0);
            modelBuilder.Entity<Tbl_Sorathesab>()
                .Property(e => e.Mablagh)
                .HasPrecision(18, 0);
            modelBuilder.Entity<Tbl_Sorathesab>()
          .HasRequired(p => p.Tbl_Kala)     // Each Product requires a Supplier
          .WithMany(s => s.Tbl_Sorathesab)        // A Supplier can have many Products
          .HasForeignKey(p => p.Code_Kala);
            modelBuilder.Entity<Tbl_Sorathesab>()
.HasRequired(p => p.Tbl_Moshtari)     // Each Product requires a Supplier
.WithMany(s => s.Tbl_Sorathesab)        // A Supplier can have many Products
.HasForeignKey(p => p.Code_Moshtari);
            base.OnModelCreating(modelBuilder);
        }
    }
}

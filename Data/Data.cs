using Microsoft.EntityFrameworkCore;
using Recyclable.Models;

namespace Recyclable.Data
{
    public partial class Data : DbContext
    {
        public Data(DbContextOptions<Data> options) : base(options)
        {
        }

        public virtual DbSet<RecyclableItem> RecyclableItems { get; set; } = null!;
        public virtual DbSet<RecyclableType> RecyclableTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SCIL843;Database=Recyclable;User=sa;Password=test;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecyclableItem>(entity =>
            {
                entity.ToTable("Recyclable_Item");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ComputedRate).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("Type_Id");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

              
            });

            modelBuilder.Entity<RecyclableType>(entity =>
            {
                entity.ToTable("Recyclable_Type");

                entity.HasIndex(e => e.Type, "UQ__Type__F9B8A48BAA284991")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MaxKg).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MinKg).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   
    }
}

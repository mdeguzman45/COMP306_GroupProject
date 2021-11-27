using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StockAPI.Models
{
    public partial class STOCKSContext : DbContext
    {
        public STOCKSContext()
        {
        }

        public STOCKSContext(DbContextOptions<STOCKSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<UserTrade> UserTrades { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=mssqlserver.cnkgfnag93kf.us-east-1.rds.amazonaws.com;Initial Catalog=STOCKS;User ID=rdsadmin;Password=rdspassword");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Symbol)
                    .HasName("PK__Stocks__B7CC3F00D01F814A");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastPrice).HasColumnType("numeric(10, 5)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(10, 5)");
            });

            modelBuilder.Entity<UserTrade>(entity =>
            {
                entity.HasKey(e => e.TradeId)
                    .HasName("PK__UserTrad__3028BABBEEE95DB1");

                entity.Property(e => e.TradeId)
                    .ValueGeneratedNever()
                    .HasColumnName("TradeID");

                entity.Property(e => e.EntryPrice).HasColumnType("numeric(10, 5)");

                entity.Property(e => e.Position)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeCloseDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TradeOpenDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SymbolNavigation)
                    .WithMany(p => p.UserTrades)
                    .HasForeignKey(d => d.Symbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTrade__Symbo__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

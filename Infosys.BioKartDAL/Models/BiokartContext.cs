using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infosys.BioKartDAL.Models
{
    public partial class BiokartContext : DbContext
    {
        public BiokartContext()
        {
        }

        public BiokartContext(DbContextOptions<BiokartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminForwardedRequest> AdminForwardedRequest { get; set; }
        public virtual DbSet<AuctionBid> AuctionBid { get; set; }
        public virtual DbSet<Auctioncart> Auctioncart { get; set; }
        public virtual DbSet<AuctionItem> AuctionItem { get; set; }
        public virtual DbSet<Auctionstock> Auctionstock { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<FarmBank> FarmBank { get; set; }
        public virtual DbSet<FarmerStock> FarmerStock { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<ItemSellable> ItemSellable { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<PastAuctionResult> PastAuctionResult { get; set; }
        public virtual DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source =(localdb)\\mssqllocaldb;Initial Catalog=Biokart;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminForwardedRequest>(entity =>
            {
                entity.HasKey(e => e.Arid);

                entity.Property(e => e.Arid).HasColumnName("ARId");

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.CustomerRid).HasColumnName("CustomerRID");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.C)
                    .WithMany(p => p.AdminForwardedRequest)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_this");
            });

            modelBuilder.Entity<AuctionBid>(entity =>
            {
                entity.HasKey(e => e.BidId);

                entity.Property(e => e.BidAmount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.BidDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.AuctionBid)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("fk_aucId");

                entity.HasOne(d => d.Bidder)
                    .WithMany(p => p.AuctionBid)
                    .HasForeignKey(d => d.BidderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bidid");
            });

            modelBuilder.Entity<Auctioncart>(entity =>
            {
                entity.HasKey(e => e.Cartid);

                entity.Property(e => e.Cartid).HasColumnName("cartid");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice).HasColumnType("numeric(10, 2)");
            });

            modelBuilder.Entity<AuctionItem>(entity =>
            {
                entity.HasKey(e => e.AuctionId);

                entity.Property(e => e.AuctionStatus)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.BasePrice).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerUnit).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.SellerUid).HasColumnName("SellerUId");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.AuctionItemEmp)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_h");

                entity.HasOne(d => d.SellerU)
                    .WithMany(p => p.AuctionItemSellerU)
                    .HasForeignKey(d => d.SellerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_hdrrr");
            });

            modelBuilder.Entity<Auctionstock>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Item });

                entity.ToTable("auctionstock");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.Item)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerUnit).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Auctionstock)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_ChouiceId");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Auctionstock)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Cartid).HasColumnName("cartid");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerUnit).HasColumnType("numeric(10, 2)");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("uq_CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FarmBank>(entity =>
            {
                entity.HasKey(e => e.AccountNumber);

                entity.ToTable("farmBank");

                entity.Property(e => e.AccountNumber).HasColumnType("numeric(15, 0)");

                entity.Property(e => e.Amount).HasColumnType("numeric(15, 0)");

                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsc)
                    .IsRequired()
                    .HasColumnName("IFSC")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.FarmBank)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("fk_farmID");
            });

            modelBuilder.Entity<FarmerStock>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Item });

                entity.ToTable("farmerStock");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.Item)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PricePerUnit).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FarmerStock)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_ChioId");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.FarmerStock)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_hdrr");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.ToTable("feedback");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemSellable>(entity =>
            {
                entity.HasKey(e => e.CategoryName);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ItemSellable)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_CatId");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Nid);

                entity.Property(e => e.Nid).HasColumnName("NId");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PastAuctionResult>(entity =>
            {
                entity.HasKey(e => e.AuctionResult);

                entity.Property(e => e.BidAmount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.PastAuctionResult)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("fk_auctionId");

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.PastAuctionResultFarmer)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_farid");

                entity.HasOne(d => d.Winner)
                    .WithMany(p => p.PastAuctionResultWinner)
                    .HasForeignKey(d => d.WinnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Biderid");
            });

            modelBuilder.Entity<PurchaseDetails>(entity =>
            {
                entity.HasKey(e => e.PurchaseId);

                entity.ToTable("purchaseDetails");

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OrderedDate).HasColumnType("datetime");

                entity.Property(e => e.PricePerUnit).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.PurchaseType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.BuyerNavigation)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.Buyer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UssId");
            });

            modelBuilder.Entity<Requests>(entity =>
            {
                entity.HasKey(e => e.Rid);

                entity.Property(e => e.Rid).HasColumnName("RId");

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.ForwardStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CatryId");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.RoleName)
                    .HasName("uq_RoleName")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pan)
                    .HasColumnName("PAN")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Pin)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Security)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_RoleId");
            });
        }
    }
}

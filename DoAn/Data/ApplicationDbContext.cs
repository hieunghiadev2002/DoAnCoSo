using DoAn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DoAn.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region DbSet
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanPhamVariant> ThongTinSanPhams { get; set; }
        public DbSet<HangSX> HangSXes { get; set; }
        public DbSet<SanPhamVariant> SanPhamVariants { get; set; }
        public DbSet<CT_DonHang> CT_DonHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<TinhTrang> TinhTrangs { get; set; }
        public DbSet<HinhThucThanhToan> HTThanhToans { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<SanPham>()
           .HasOne(s => s.LoaiSanPham)
           .WithMany() 
           .HasForeignKey(s => s.MaLoai)
           .HasConstraintName("FK_SanPham_LoaiSanPham");

            modelBuilder.Entity<CT_SanPham>(e =>
            {
                e.ToTable("CT_SanPham");
                e.HasKey(e => new { e.MaSanPham, e.MaHsx });


                modelBuilder.Entity<SanPham>().
                    HasMany(s => s.Variants).
                    WithOne(s => s.SanPham)
                    .HasForeignKey(s => s.MaSanPham);

                modelBuilder.Entity<CT_SanPham>(entity =>
                {
                    entity.HasKey(e => new { e.MaSanPham, e.MaHsx });

                    entity.HasOne(e => e.SanPham)
                          .WithMany()
                          .HasForeignKey(e => e.MaSanPham)
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(e => e.HangSX)
                          .WithMany()
                          .HasForeignKey(e => e.MaHsx)
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.Property(e => e.BoPhan)
                          .HasMaxLength(100)
                          .IsRequired(false);
                });

                modelBuilder.Entity<CT_DonHang>(e =>
                {
                    e.ToTable("CT_DonHang");
                    e.HasKey(e => new { e.MaSanPham, e.MaDonHang });

                    e.HasOne(s => s.SanPhamVariant)
                    .WithMany(s => s.ChiTietDonHangs)
                    .HasForeignKey(s => s.MaSanPham);

                    e.HasOne(s => s.DonHang)
                    .WithMany(s => s.ct_DonHang)
                    .HasForeignKey(s => s.MaDonHang);
                });

                modelBuilder.Entity<RefreshToken>().HasIndex(t => t.UserId);
                var adminRoleID = "16522854-f588-4c1c-9476-a0539d30f6e4";
                var userRoleID = "d54d2f2f-63af-4fd3-93ee-0c7f929aec08";
                var managerRoleID = "ac4f19e9-6a22-4664-b164-e7ea301def27";


                var roles = new List<IdentityRole>
              {
                  new IdentityRole {
                      Id = adminRoleID,
                      ConcurrencyStamp = adminRoleID,
                      Name  = "ADMIN",
                      NormalizedName = "ADMIN".ToUpper()
                  },
                  new IdentityRole {
                      Id = userRoleID,
                      ConcurrencyStamp = userRoleID,
                      Name  = "USER",
                      NormalizedName = "USER".ToUpper()
                  },
                    new IdentityRole
                    {
                      Id = managerRoleID,
                      ConcurrencyStamp = managerRoleID,
                      Name  = "MANAGER",
                      NormalizedName = "MANAGER".ToUpper()
                    }
              };
                modelBuilder.Entity<IdentityRole>().HasData(roles);
                var tinhTrangDons = new List<TinhTrang>
                {
                    new TinhTrang
                    {
                        MaTinhtrang = 1,
                        TenTinhTrang = "Chờ xác nhận"
                    },
                    new TinhTrang
                    {
                        MaTinhtrang = 2,
                        TenTinhTrang = "Đang giao"
                    },
                    new TinhTrang
                    {
                        MaTinhtrang = 3,
                        TenTinhTrang = "Đã giao"
                    },
                    new TinhTrang
                    {
                        MaTinhtrang = 4,
                        TenTinhTrang = "Đã hủy"
                    },
                };
                modelBuilder.Entity<TinhTrang>().HasData(tinhTrangDons);
            }
            );
        }
    }
}


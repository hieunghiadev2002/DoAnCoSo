using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace DoAn.Data
{
    public class SanPhamVariant
    {
        [Key]
        public int MaThongTin { get; set; }
        [Required]
        public int MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public string KichThuoc { get; set; }
        public int TrongLuong { get; set; }
        public string MauSac { get; set; }
        public string CPU { get; set; }
        public double GiaBan { get; set; }
        public int SoLuongKho { get; set; }
        public string Camera { get; set; }
        public string BoNho { get; set; }
        public string HeDieuHanh { get; set; }  
        public int Pin { get; set; } 
        public double CanNang { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Liên kết với Chi tiết đơn hàng 
        public ICollection<CT_DonHang> ChiTietDonHangs { get; set; }
    }
}
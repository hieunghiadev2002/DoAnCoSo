using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Data
{
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }
        public string TenSanPham { set; get; }
        public string MoTa { set; get; }
        [ForeignKey("LoaiSanPham")]
        public int MaLoai { set; get; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public string NgayPhatHanh { get; set; }
        public int SoLuongBan { set; get; }
        public int ThoiGianBaoHanh { set; get; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<SanPhamVariant> Variants { get; set; }
        
    }
}
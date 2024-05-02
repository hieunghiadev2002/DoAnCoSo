using System.ComponentModel.DataAnnotations;

namespace DoAn.Data
{
    public class LoaiSanPham
    {
        [Key]
        [Required]
        public int MaLoai {  get; set; }
        public string TenLoai { get; set; }
        public virtual ICollection<SanPham>? SanPham { get; set; } // Mối quan hệ 1 - n
    } 
}

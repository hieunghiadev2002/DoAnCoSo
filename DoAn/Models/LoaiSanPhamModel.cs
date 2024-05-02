using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class LoaiSanPhamModel
    {
        [Key]
        [Required]
        public int MaLoai { get; set; }
        [Required]
        public string TenLoai { get; set; } = null!;
    }
}

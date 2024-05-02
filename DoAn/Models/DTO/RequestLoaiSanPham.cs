using System.ComponentModel.DataAnnotations;

namespace DoAn.Models.RequestModel
{
    public class RequestLoaiSanPham
    {
        [Required]
        public string TenLoai { get; set; } = null!;
    }
}

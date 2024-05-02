using DoAn.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class SanPhamModel
    {
      
        public string TenSanPham { set; get; }
        public string MoTa { set; get; }
        public int MaLoai { set; get; }
        public string NgayPhatHanh { get; set; }
        public int SoLuongBan { set; get; }
        public int ThoiGianBaoHanh { set; get; }
    }
}

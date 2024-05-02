using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Data
{
    public class CT_DonHang
    {
        [Key, Column(Order = 0)]        
        
        public int MaDonHang { get; set; }
        [Key, Column(Order = 1)]
        public int MaSanPham {  get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }

        public SanPhamVariant SanPhamVariant { get; set; }
        public DonHang DonHang { get; set; }

    }
}
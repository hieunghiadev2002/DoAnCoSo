using DoAn.Data;
using System.ComponentModel.DataAnnotations;

namespace DoAn.Models.DTO
{
    public class ProductVariantDTO
    {        
    
        public int MaSanPham { get; set; }
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
    }
}

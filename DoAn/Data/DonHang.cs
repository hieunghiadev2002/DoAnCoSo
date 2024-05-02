using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Data
{
    public class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
        public string? NoiNhan { get; set; }
        public string? GhiChu { get; set; }
        public int MaThanhToan { get; set; }
        [ForeignKey("MaThanhToan")]
        public HinhThucThanhToan HTTToan { get; set; }
        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public int MaTinhTrang { get; set; }
        [ForeignKey("MaTinhTrang")]
        public TinhTrang tinhTrang { get; set; }  

        public ICollection<CT_DonHang>? ct_DonHang { get; set; }
        public DonHang()
        {
            ct_DonHang = new List<CT_DonHang>();
        }
    }
}
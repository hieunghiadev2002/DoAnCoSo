using System.ComponentModel.DataAnnotations;

namespace DoAn.Data
{
    public class HinhThucThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }
        public string HinhThuc {  get; set; }
        public ICollection<DonHang>? DonHang { get; set;}

    }
}
using System.ComponentModel.DataAnnotations;

namespace DoAn.Data
{
    public class HangSX
    {
        [Key]
        public int MaHsx { get; set; }
        public string TenHangSX { get; set; }
        public ICollection<CT_SanPham> cT_SanPhams { get; set; }
        public HangSX()
        {
            cT_SanPhams = new List<CT_SanPham>();
        }

    }
}

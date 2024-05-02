namespace DoAn.Data
{
    public class CT_SanPham
    {
        public int MaSanPham {  get; set; }
        public int MaHsx { get; set; }
        public string? BoPhan { get; set; }
        public SanPham SanPham { get; set; }
        public HangSX HangSX { get; set; }
    }
}
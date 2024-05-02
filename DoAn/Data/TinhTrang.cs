using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace DoAn.Data
{
    public class TinhTrang
    {
        [Key]
        public int MaTinhtrang { get; set; }
        public string TenTinhTrang {  get; set; }
        public ICollection<DonHang>? DonHang { get; set;}
    }
}
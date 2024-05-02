using DoAn.Data;
using DoAn.Models.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSanPhamController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        public LoaiSanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllLoaiSanPham()
        {
            var loaiSanPhams = _context.LoaiSanPhams;
            var res = new
            {
                message = "Get all loai san pham successfully",
                data = loaiSanPhams
            };
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoaiSanPham([FromBody] RequestLoaiSanPham requestLoaiSanPham)
        {
            var loaiSanPham = new LoaiSanPham
            {
                TenLoai = requestLoaiSanPham.TenLoai
            };
            _context.LoaiSanPhams.Add(loaiSanPham);
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Create loai san pham successfully",
                data = loaiSanPham
            };
            return Ok(res);
        }
    }
}

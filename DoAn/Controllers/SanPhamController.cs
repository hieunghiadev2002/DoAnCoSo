using DoAn.Data;
using DoAn.Models;
using DoAn.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;

        public SanPhamController(ApplicationDbContext context)
        {
           
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
           var sanPhams = await _context.SanPhams.Include(x => x.LoaiSanPham).ToListAsync();
            var res = new
            {
                message = "Get all products successfully",
                data = sanPhams
            };
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _context.SanPhams.Include(x => x.LoaiSanPham).FirstOrDefaultAsync(x => x.MaSanPham == id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            var res = new
            {
                message = "Get product by id successfully",
                data = result
            };
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSanPham([FromBody] SanPhamModel sanPhamModel)
        {
            var checkMaLoai = await _context.LoaiSanPhams.FirstOrDefaultAsync(x => x.MaLoai == sanPhamModel.MaLoai);
            if(checkMaLoai == null)
            {
                return BadRequest("Loai san pham khong ton tai");
            }

            var sanPhamEntity = new SanPham
            {
                TenSanPham = sanPhamModel.TenSanPham,
                MoTa = sanPhamModel.MoTa,
                MaLoai = sanPhamModel.MaLoai,
                NgayPhatHanh = sanPhamModel.NgayPhatHanh,
                SoLuongBan = sanPhamModel.SoLuongBan,
                ThoiGianBaoHanh = sanPhamModel.ThoiGianBaoHanh
            };
            await _context.SanPhams.AddAsync(sanPhamEntity);
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Create product successfully",
                data = sanPhamEntity
            };
            return Ok(res);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSanPham(int id, [FromBody] SanPhamModel sanPhamModel)
        {
            var sanPhamEntity = await _context.SanPhams.FirstOrDefaultAsync(x => x.MaSanPham == id);
            if (sanPhamEntity == null)
            {
                return NotFound("Product not found");
            }
            var checkMaLoai = await _context.LoaiSanPhams.FirstOrDefaultAsync(x => x.MaLoai == sanPhamModel.MaLoai);
            if (checkMaLoai == null)
            {
                return BadRequest("Loai san pham khong ton tai");
            }
            sanPhamEntity.TenSanPham = sanPhamModel.TenSanPham;
            sanPhamEntity.MoTa = sanPhamModel.MoTa;
            sanPhamEntity.MaLoai = sanPhamModel.MaLoai;
            sanPhamEntity.NgayPhatHanh = sanPhamModel.NgayPhatHanh;
            sanPhamEntity.SoLuongBan = sanPhamModel.SoLuongBan;
            sanPhamEntity.ThoiGianBaoHanh = sanPhamModel.ThoiGianBaoHanh;
            _context.SanPhams.Update(sanPhamEntity);
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Update product successfully",
                data = sanPhamEntity
            };
            return Ok(res);
        }

    }
}

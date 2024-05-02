using DoAn.Data;
using DoAn.Models.DTO;
using DoAn.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangSanXuatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHangSanXuatRepository _hangSanXuatRepository;
        public HangSanXuatController(ApplicationDbContext context, IHangSanXuatRepository hangSanXuatRepository)
        {
           _context = context;
            _hangSanXuatRepository = hangSanXuatRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHangSanXuat(string sortBy = "tenHang", bool isAscending = true, string keyword = null, int page = 1, int pageSize = 10)
        {

            var HangSanXuats = await _hangSanXuatRepository.GetAllHangSanXuatAsync(sortBy, isAscending, keyword, page, pageSize);
          



            return Ok(HangSanXuats);     
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHangSanXuatById(int id)
        {
            var HangSanXuat = await _context.HangSXes.FindAsync(id);
            if (HangSanXuat == null)
            {
                return NotFound("Hang san xuat not found");
            }
            var res = new
            {
                message = "Get hang san xuat by id successfully",
                data = HangSanXuat
            };
            return Ok(res);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateHangSanXuat(HangSanXuatDTO hangSanXuatDTO)
        {
            var hangSanXuat = new HangSX
            {
                TenHangSX = hangSanXuatDTO.TenHangSX
            };
            _context.HangSXes.Add(hangSanXuat);
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Create hang san xuat successfully",
                data = hangSanXuat
            };
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHangSanXuat(int id, HangSanXuatDTO hangSanXuatDTO)
        {
            var hangSanXuat = await _context.HangSXes.FindAsync(id);
            if (hangSanXuat == null)
            {
                return NotFound("Hang san xuat not found");
            }
            hangSanXuat.TenHangSX = hangSanXuatDTO.TenHangSX;
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Update hang san xuat successfully",
                data = hangSanXuat
            };
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHangSanXuat(int id)
        {
            var hangSanXuat = await _context.HangSXes.FindAsync(id);
            if (hangSanXuat == null)
            {
                return NotFound("Hang san xuat not found");
            }
            _context.HangSXes.Remove(hangSanXuat);
            await _context.SaveChangesAsync();
            return Ok("Delete hang san xuat successfully");
        }

    }
}

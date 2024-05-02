using AutoMapper;
using DoAn.Data;
using DoAn.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    [Route("api/variant-product")]
    [ApiController]
    public class VariantProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VariantProductController(ApplicationDbContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVariantProducts()
        {
           //find All 
           var variantProducts = await _context.SanPhamVariants.ToListAsync();
            var res = new
            {
                message = "Get all variant products successfully",
                data = variantProducts
            };
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariantProductById(int id)
        {
            if(id == null)
            {
                return BadRequest("Id is null");
            }
            var variantProduct = await _context.SanPhamVariants.FindAsync(id);
            //query them SanPham
            

            if (variantProduct == null)
            {
                return NotFound("Product variant not found");
            }
            var res = new
            {
                message = "Get variant product by id successfully",
                data = variantProduct
            };

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariantProduct(ProductVariantDTO productVariantDTO)
        {
            var checkSanPham = await _context.SanPhams.FindAsync(productVariantDTO.MaSanPham);
            if(checkSanPham == null)
            {
                return BadRequest("San pham khong ton tai");
            }
            var variant = new SanPhamVariant
            {
                MaSanPham = productVariantDTO.MaSanPham,
                KichThuoc = productVariantDTO.KichThuoc,
                TrongLuong = productVariantDTO.TrongLuong,
                MauSac = productVariantDTO.MauSac,
                CPU = productVariantDTO.CPU,
                GiaBan = productVariantDTO.GiaBan,
                SoLuongKho = productVariantDTO.SoLuongKho,
                Camera = productVariantDTO.Camera,
                BoNho = productVariantDTO.BoNho,
                HeDieuHanh = productVariantDTO.HeDieuHanh,
                Pin = productVariantDTO.Pin,
                CanNang = productVariantDTO.CanNang
            };
            await _context.SanPhamVariants.AddAsync(variant);
            await _context.SaveChangesAsync();
            return Ok("Create variant product successfully");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductVariant(int id, ProductVariantDTO productVariantDTO)
        {
            var variant = await _context.SanPhamVariants.FindAsync(id);
            if(variant == null)
            {
                return NotFound("Product variant not found");
            }
            variant.KichThuoc = productVariantDTO.KichThuoc;
            variant.TrongLuong = productVariantDTO.TrongLuong;
            variant.MauSac = productVariantDTO.MauSac;
            variant.CPU = productVariantDTO.CPU;
            variant.GiaBan = productVariantDTO.GiaBan;
            variant.SoLuongKho = productVariantDTO.SoLuongKho;
            variant.Camera = productVariantDTO.Camera;
            variant.BoNho = productVariantDTO.BoNho;
            variant.HeDieuHanh = productVariantDTO.HeDieuHanh;
            variant.Pin = productVariantDTO.Pin;
            variant.CanNang = productVariantDTO.CanNang;
            await _context.SaveChangesAsync();
            var res = new
            {
                message = "Update product variant successfully",
                data = variant
            };
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductVariant(int id)
        {
            var variant = await _context.SanPhamVariants.FindAsync(id);
            if (variant == null)
            {
                return NotFound("Product variant not found");
            }
            _context.SanPhamVariants.Remove(variant);
            await _context.SaveChangesAsync();
            return Ok("Delete product variant successfully");
        }
    }
}

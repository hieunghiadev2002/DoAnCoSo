using DoAn.Data;
using DoAn.Models.DTO;
using DoAn.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Repository
{
    public class HangSanXuatRepository : IHangSanXuatRepository
    {
        private readonly ApplicationDbContext _context; 
        public HangSanXuatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        async Task<HangSanXuatDTO> IHangSanXuatRepository.Create(HangSanXuatDTO hangSanXuat)
        {
            var hangSanXuatEntity = new HangSX
            {
                TenHangSX = hangSanXuat.TenHangSX
            };
            await _context.HangSXes.AddAsync(hangSanXuatEntity);
            await _context.SaveChangesAsync();
            return hangSanXuat;
        }

        Task<HangSanXuatDTO> IHangSanXuatRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<HangSX>> IHangSanXuatRepository.GetAllHangSanXuatAsync()
        {
            throw new NotImplementedException();
        }

        Task<HangSanXuatDTO> IHangSanXuatRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<HangSanXuatDTO> IHangSanXuatRepository.Update(HangSanXuatDTO hangSanXuat)
        {
            throw new NotImplementedException();
        }
    }
}

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

        async Task<IEnumerable<HangSX>> IHangSanXuatRepository.GetAllHangSanXuatAsync(string sortBy, bool isAscending, string keyword, int page, int pageSize)
        {
            IQueryable<HangSX> query = _context.HangSXes; 
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenHangSX.Contains(keyword));
            }
            //Sorting 
            switch(sortBy)
            {
                case "TenHangSX":
                    if (isAscending)
                    {
                        query = query.OrderBy(x => x.TenHangSX);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.TenHangSX);
                    }
                    break;
                default:
                    query = query.OrderBy(x => x.TenHangSX);
                    break;
            }
            var skipAmount = (page - 1) * pageSize;
            query = query.Skip(skipAmount).Take(pageSize);
            return await query.ToListAsync();
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

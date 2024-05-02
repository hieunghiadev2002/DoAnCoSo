using DoAn.Data;
using DoAn.Models.DTO;

namespace DoAn.Repository.Interface
{
    public interface IHangSanXuatRepository
    {
        Task<IEnumerable<HangSX>> GetAllHangSanXuatAsync(string sortBy, bool isAscending, string keyword, int page, int pageSize);
        Task<HangSanXuatDTO> GetById(int id);
        Task<HangSanXuatDTO> Create(HangSanXuatDTO hangSanXuat);
        Task<HangSanXuatDTO> Update(HangSanXuatDTO hangSanXuat);
        Task<HangSanXuatDTO> Delete(int id);
    }
}

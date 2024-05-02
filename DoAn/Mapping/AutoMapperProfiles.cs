using AutoMapper;
using DoAn.Data;
using DoAn.Models;

namespace DoAn.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LoaiSanPham, LoaiSanPhamModel>();
            CreateMap<LoaiSanPhamModel, LoaiSanPham>(); 
        }
    }
}

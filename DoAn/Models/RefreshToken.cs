using DoAn.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        //UserId de cho bk la RefreshToken thuoc ve User nao 
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        //Bien bool de check xem la RefreshToken da duoc sd hay chua 
        public bool IsRevoked { get; set; }
        public string Token { get; set; }
        //Jwt Id de cho bk la RefreshToken nay thuoc ve AccessToken nao
        public string JwtId { get; set; }   
        //Ngay tao refreshTOken 
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    
    }
}

using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class RequestRefreshToken
    {
        [Required(ErrorMessage = "Refresh token is require")]
        public string RefreshToken { get; set; } = null!;
    }
}

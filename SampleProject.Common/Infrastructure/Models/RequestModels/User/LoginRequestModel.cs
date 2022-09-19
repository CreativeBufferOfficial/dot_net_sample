using System.ComponentModel.DataAnnotations;

namespace SampleProject.Common.Infrastructure.Models.RequestModels.User
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

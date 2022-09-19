using System.ComponentModel.DataAnnotations;

namespace SampleProject.Common.Infrastructure.Models.RequestModels.User
{
    public class CreateUserRequestModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}

using SampleProject.Common.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Common.Infrastructure.Models.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        [Required]
        [NotEmptyGuid]
        public Guid UserGuid { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}

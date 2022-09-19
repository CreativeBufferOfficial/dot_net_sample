using System.ComponentModel.DataAnnotations;

namespace SampleProject.Common.Infrastructure.Models.RequestModels.Customer
{
    public class CreateCustomerRequestModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 1)]
        public string Email { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string Address { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string State { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Country { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Zip { get; set; }
    }
}

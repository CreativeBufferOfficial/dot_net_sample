using SampleProject.Common.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Common.Infrastructure.Models.RequestModels.Customer
{
    public class UpdateCustomerRequestModel : CreateCustomerRequestModel
    {
        [Required]
        [NotEmptyGuid]
        public Guid CustomerGuid { get; set; }
    }
}

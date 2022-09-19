namespace SampleProject.Common.Infrastructure.Models.ResponseModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public bool IsDeleted { get; set; }
    }
}

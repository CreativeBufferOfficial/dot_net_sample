namespace SampleProject.Common.Infrastructure.Models.Entities
{
    public class CustomerModel
    {
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
        public int CreatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
    }
}

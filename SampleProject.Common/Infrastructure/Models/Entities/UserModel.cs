namespace SampleProject.Common.Infrastructure.Models.Entities
{
    public class UserModel
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
    }
}

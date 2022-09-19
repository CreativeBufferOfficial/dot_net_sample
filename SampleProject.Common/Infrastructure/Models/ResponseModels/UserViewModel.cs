namespace SampleProject.Common.Infrastructure.Models.ResponseModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Name => $"{FirstName} {LastName}".Trim();
    }
}

namespace SampleProject.Common.Infrastructure.Models.Entities
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public Guid RoleGuid { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}

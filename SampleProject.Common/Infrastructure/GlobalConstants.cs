namespace SampleProject.Common.Infrastructure
{
    public class GlobalConstants
    {
        public class TokenClaims
        {
            public const string Id = "UserId";
            public const string Guid = "UserGuid";
            public const string Name = "UserName";
            public const string Email = "UserEmail";
            public const string PhoneNumber = "UserPhoneNumber";
            public const string IsActive = "IsActive";
            public const string RoleId = "RoleId";
            public const string RoleName = "RoleName";
        }

        public enum RolesEnum
        {
            Admin = 1,
            Employee = 2
        }

        public class Roles
        {
            public const string Admin = "Admin";
            public const string Employee = "Employee";
        }

    }
}

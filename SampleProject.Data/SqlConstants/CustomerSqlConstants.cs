namespace SampleProject.Data.SqlConstants
{
    public class CustomerSqlConstants
    {
        public const string GET_ALL_CUSTOMERS = @"usp_GetAllCustomers";

        public const string GET_CUSTOMER_DETAILS_BY_GUID = @"usp_GetCustomerDetailsByGuid";

        public const string DELETE_CUSTOMER = @"usp_DeleteCustomer";

        public const string CREATE_CUSTOMER = @"usp_CreateCustomer";

        public const string UPDATE_CUSTOMER = @"usp_UpdateCustomer";
    }
}

-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to get list of all customers
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllCustomers]
	@ShowDeleted BIT
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT CustomerId, CustomerGuid, CustomerName, PhoneNumber, Email, 
		[Address], City, [State], Country, Zip, IsDeleted
	FROM [dbo].[Customers] WITH (NOLOCK)
	WHERE (@ShowDeleted IS NULL OR IsDeleted = @ShowDeleted)
END

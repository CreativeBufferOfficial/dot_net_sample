-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to get customer details by guid
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetCustomerDetailsByGuid]
	@CustomerGuid UNIQUEIDENTIFIER
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT  CustomerId, CustomerGuid, CustomerName, PhoneNumber, Email, 
		[Address], City, [State], Country, Zip, IsDeleted
	FROM [dbo].[Customers] WITH (NOLOCK)
	WHERE CustomerGuid = @CustomerGuid AND IsDeleted = 0
END

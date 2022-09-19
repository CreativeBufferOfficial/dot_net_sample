-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to delete customer
-- =============================================
CREATE PROCEDURE usp_DeleteCustomer
	@CustomerGuid UNIQUEIDENTIFIER,
	@ModifiedBy INT,
	@ModifiedDateUtc DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[Customers]
	SET IsDeleted = 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDateUtc = @ModifiedDateUtc
	WHERE CustomerGuid = @CustomerGuid

	SELECT @@ROWCOUNT;
END

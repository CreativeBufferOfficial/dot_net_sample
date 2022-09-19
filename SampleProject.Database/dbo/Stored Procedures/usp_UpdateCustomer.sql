-- =============================================
-- Author:		Umesh
-- Create date: September 19, 2022
-- Description:	procedure to update an existing customer
-- =============================================
CREATE PROCEDURE usp_UpdateCustomer
	@CustomerGuid UNIQUEIDENTIFIER,
	@CustomerName VARCHAR(100),
	@PhoneNumber VARCHAR(50),
	@Email VARCHAR(200),
	@Address VARCHAR(2000),
	@City VARCHAR(100),
	@State VARCHAR(100),
	@Country VARCHAR(100),
	@Zip VARCHAR(10),
	@ModifiedBy INT,
	@ModifiedDateUtc DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[Customers]
	SET [CustomerName] = @CustomerName,
		[PhoneNumber] = @PhoneNumber,
		[Email] = @Email,
		[Address] = @Address,
		[City] = @City,
		[State] = @State,
		[Country] = @Country,
		[Zip] = @Zip,
		[ModifiedBy] = @ModifiedBy,
		[ModifiedDateUtc] = @ModifiedDateUtc
	WHERE [CustomerGuid] = @CustomerGuid

	SELECT @@ROWCOUNT;
END

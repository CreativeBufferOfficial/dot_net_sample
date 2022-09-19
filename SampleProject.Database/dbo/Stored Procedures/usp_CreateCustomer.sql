-- =============================================
-- Author:		Umesh 
-- Create date: September 19, 2022
-- Description:	procedure to create a new customer
-- =============================================
CREATE PROCEDURE usp_CreateCustomer 
	@CustomerGuid UNIQUEIDENTIFIER,
	@CustomerName VARCHAR(100),
	@PhoneNumber VARCHAR(50),
	@Email VARCHAR(200),
	@Address VARCHAR(2000),
	@City VARCHAR(100),
	@State VARCHAR(100),
	@Country VARCHAR(100),
	@Zip VARCHAR(10),
	@IsDeleted BIT,
	@CreatedBy INT,
	@CreatedDateUtc DATETIME
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @CustomerGuid = ISNULL(@CustomerGuid, NEWID());
	SET @CreatedDateUtc = ISNULL(@CreatedDateUtc, GETUTCDATE());

	INSERT INTO [dbo].[Customers]
		([CustomerGuid], [CustomerName], [PhoneNumber], [Email], 
			[Address], [City], [State], [Country], [Zip],
			[IsDeleted], [CreatedBy], [CreatedDateUtc], [ModifiedBy], [ModifiedDateUtc])
     VALUES
		(@CustomerGuid, @CustomerName, @PhoneNumber, @Email,
			@Address, @City, @State, @Country, @Zip, 
			@IsDeleted, @CreatedBy, @CreatedDateUtc, @CreatedBy, @CreatedDateUtc)

	SELECT SCOPE_IDENTITY();
END

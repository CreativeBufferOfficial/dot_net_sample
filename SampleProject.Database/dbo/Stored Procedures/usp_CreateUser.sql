-- =============================================
-- Author:		Umesh
-- Create date: September 12, 2022
-- Description:	procedure to create a specific user
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateUser]
	@UserGuid UNIQUEIDENTIFIER,
	@FirstName VARCHAR(100),
	@LastName VARCHAR(100),
	@PhoneNumber VARCHAR(50),
	@Email VARCHAR(200),
	@PasswordHash VARCHAR(500),
	@RoleId INT,
	@IsActive BIT,
	@IsDeleted BIT,
	@CreatedBy INT,
	@CreatedDateUtc DATETIME
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @UserGuid = ISNULL(@UserGuid, NEWID());
	SET @CreatedDateUtc = ISNULL(@CreatedDateUtc, GETUTCDATE());

    INSERT INTO [dbo].[Users]
		([UserGuid], [FirstName], [LastName], [PhoneNumber], [Email], [PasswordHash], [RoleId],
		[IsActive], [IsDeleted], [CreatedBy], [CreatedDateUtc], [ModifiedBy], [ModifiedDateUtc])
	VALUES
		(@UserGuid, @FirstName, @LastName, @PhoneNumber, @Email, @PasswordHash, @RoleId,
		@IsActive, @IsDeleted, @CreatedBy, @CreatedDateUtc, @CreatedBy, @CreatedDateUtc)

	SELECT SCOPE_IDENTITY();
END

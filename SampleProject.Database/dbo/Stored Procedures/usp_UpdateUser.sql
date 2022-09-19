-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to update user details
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateUser]
	@UserGuid UNIQUEIDENTIFIER,
	@FirstName VARCHAR(100),
	@LastName VARCHAR(100),
	@PhoneNumber VARCHAR(50),
	@RoleId INT,
	@IsActive BIT,
	@ModifiedBy INT,
	@ModifiedDateUtc DATETIME
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[Users]
	SET [FirstName] = @FirstName,
		[LastName] = @LastName,
		[PhoneNumber] = @PhoneNumber,
		[RoleId] = @RoleId,
		[IsActive] = @IsActive,
		[ModifiedBy] = @ModifiedBy,
		[ModifiedDateUtc] = @ModifiedDateUtc
	WHERE [UserGuid] = @UserGuid

	SELECT @@ROWCOUNT;
END

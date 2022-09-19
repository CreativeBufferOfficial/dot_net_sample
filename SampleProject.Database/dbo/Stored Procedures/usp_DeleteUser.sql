-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to soft delete an existing user
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteUser]
	@UserGuid UNIQUEIDENTIFIER,
	@ModifiedBy INT,
	@ModifiedDateUtc DATETIME
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[Users]
	SET IsDeleted = 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDateUtc = @ModifiedDateUtc
	WHERE UserGuid = @UserGuid

	SELECT @@ROWCOUNT;
END

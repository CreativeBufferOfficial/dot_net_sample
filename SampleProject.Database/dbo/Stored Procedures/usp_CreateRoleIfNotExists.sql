-- =============================================
-- Author:		Umesh	
-- Create date: September 12, 2022
-- Description:	create a specific role if not already exists
--				chechking existance based on role name or role id
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateRoleIfNotExists]
	@RoleId INT,
	@RoleGuid UNIQUEIDENTIFIER,
	@RoleName VARCHAR(100),
	@CreatedDateUtc DATETIME 
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [dbo].[Roles] WITH (NOLOCK) WHERE RoleName = @RoleName OR RoleId = @RoleId)
	BEGIN
		INSERT INTO [dbo].[Roles]
			([RoleId], [RoleGuid], [RoleName], [CreatedDateUtc])
		VALUES
			(@RoleId, @RoleGuid, @RoleName, @CreatedDateUtc)

		SELECT @@ROWCOUNT;
	END
END

-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to get user details by user guid
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetailsByGuid]
	@UserGuid UNIQUEIDENTIFIER
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT UserId, UserGuid, FirstName, LastName, PhoneNumber, Email,
		IsActive, u.RoleId, r.RoleName, PasswordHash
	FROM [dbo].[Users] u WITH (NOLOCK)
	INNER JOIN [dbo].[Roles] r WITH (NOLOCK) ON u.RoleId = r.RoleId
	WHERE UserGuid = @UserGuid AND IsDeleted = 0
END

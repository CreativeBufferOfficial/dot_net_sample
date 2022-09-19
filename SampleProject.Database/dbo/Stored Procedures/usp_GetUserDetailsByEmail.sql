-- =============================================
-- Author:		Umesh
-- Create date: September 09, 2022
-- Description:	procedure to get user details by email
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetailsByEmail]
	@Email VARCHAR(200)
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
	WHERE Email = @Email AND IsDeleted = 0
END

-- =============================================
-- Author:		Umesh
-- Create date: September 15, 2022
-- Description:	procedure to get list of all users
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllUsers]
	@ShowDeleted BIT
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT UserId, UserGuid, FirstName, LastName, PhoneNumber, Email,
		u.RoleId, r.RoleName, IsActive
	FROM [dbo].[Users] u WITH (NOLOCK)
	INNER JOIN [dbo].[Roles] r WITH (NOLOCK) ON u.RoleId = r.RoleId
	WHERE (@ShowDeleted IS NULL OR IsDeleted = @ShowDeleted)
END

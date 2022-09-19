-- =============================================
-- Author:		Umesh
-- Create date: September 16, 2022
-- Description:	procedure to get list of all roles
-- =============================================
CREATE PROCEDURE usp_GetAllRoles
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT RoleId, RoleGuid, RoleName
	FROM [dbo].[Roles] WITH (NOLOCK)
END

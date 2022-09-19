-- =============================================
-- Author:		Umesh
-- Create date: September 12, 2022
-- Description:	returns the count of users that exists with the given email
-- =============================================
CREATE PROCEDURE [dbo].[usp_CheckUserExistence]
	@Email VARCHAR(200)
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT COUNT(1)
	FROM [dbo].[Users] WITH (NOLOCK)
	WHERE Email = @Email

END

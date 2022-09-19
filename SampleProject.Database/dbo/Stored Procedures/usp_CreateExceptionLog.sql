-- =============================================
-- Author:		Umesh
-- Create date: September 12, 2022
-- Description:	procedure to exception log entry
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateExceptionLog]
	@Source VARCHAR(500),
	@StackTrace VARCHAR(MAX),
	@Message VARCHAR(500),
	@OccuredDateUtc DATETIME,
	@UserId INT = NULL
WITH RECOMPILE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[ExceptionLog]
		([Source], [StackTrace], [Message], [OccuredDateUtc], [UserId])
    VALUES
		(@Source, @StackTrace, @Message, @OccuredDateUtc, @UserId)    
END

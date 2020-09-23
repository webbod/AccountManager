-- =============================================
-- Description:	Finds the account associated with EmailAddress
-- =============================================
CREATE PROCEDURE [dbo].[AccountFind]
	@EmailAddress NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		[Id], 
		[EmailAddress], 
		[HashedPassword]
	FROM 
		[dbo].[Accounts]
	WHERE 
		[EmailAddress] = @EmailAddress

END

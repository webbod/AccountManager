-- =============================================
-- Description:	Deletes the account associated with EmailAddress
-- =============================================
CREATE PROCEDURE [dbo].[AccountDelete]
	@EmailAddress NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReturnValue INT
	DECLARE @Output TABLE ( [Id] INT )

	DELETE
	FROM 
		[dbo].[Accounts]
	OUTPUT 
		DELETED.[Id] INTO @Output
	WHERE 
		[EmailAddress] = @EmailAddress

	SELECT 
		@ReturnValue = [Id] 
	FROM 
		@Output

	RETURN @ReturnValue

END

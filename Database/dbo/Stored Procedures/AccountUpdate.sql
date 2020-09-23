-- =============================================
-- Description:	Inserts a new record if the Id is 0, otherwise updates its HashedPassword
-- =============================================
CREATE PROCEDURE [dbo].[AccountUpdate]
	@Id INT = 0, 
	@EmailAddress NVARCHAR(200),
	@HashedPassword NVARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @ReturnValue INT
	DECLARE @Output TABLE ( [Id] INT )

	IF  @Id = 0
		BEGIN
	
			INSERT INTO 
				[dbo].[Accounts]
				(
					[EmailAddress], 
					[HashedPassword]
				)
			OUTPUT 
				INSERTED.[Id] INTO @Output
			VALUES
			   (
					@EmailAddress, 
					@HashedPassword
				)
		END
	ELSE
		BEGIN

			UPDATE 
				[dbo].[Accounts]
			SET 
				[HashedPassword] = @HashedPassword
			OUTPUT 
				 INSERTED.[Id] INTO @Output
			WHERE 
				[EmailAddress] = @EmailAddress

			SET @ReturnValue = @Id
		END


	SELECT 
		@ReturnValue = [Id] 
	FROM 
		@Output

	RETURN @ReturnValue
END

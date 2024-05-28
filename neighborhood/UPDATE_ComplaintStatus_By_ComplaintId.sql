CREATE PROCEDURE [dbo].[UPDATE_ComplaintStatus_By_ComplaintId]
	@Id uniqueidentifier,
	@Status int,
	@OutputDateDeActivated datetime OUT
AS
	SET @OutputDateDeActivated = SYSUTCDATETIME()
	UPDATE Complaints 
	SET 
		Status = @Status,
		DateDeActivated = @OutputDateDeActivated
	WHERE Id = @Id;
RETURN 0

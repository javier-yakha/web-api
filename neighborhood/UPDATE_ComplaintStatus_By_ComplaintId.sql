CREATE PROCEDURE [dbo].[UPDATE_ComplaintStatus_By_ComplaintId]
	@Id uniqueidentifier,
	@Status int
AS
	UPDATE Complaints 
	SET 
		Status = @Status,
		DateDeActivated = SYSUTCDATETIME()
	WHERE Id = @Id;
RETURN 0

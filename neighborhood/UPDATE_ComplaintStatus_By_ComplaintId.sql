CREATE PROCEDURE [dbo].[UPDATE_ComplaintStatus_By_ComplaintId]
	@Id uniqueidentifier,
	@CurrentStatus int,
	@LastUpdated datetime
AS
	UPDATE Complaints 
	SET 
		CurrentStatus = @CurrentStatus,
		LastUpdated = @LastUpdated
	WHERE Id = @Id;
RETURN 0

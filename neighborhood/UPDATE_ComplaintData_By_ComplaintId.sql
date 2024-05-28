CREATE PROCEDURE [dbo].[UPDATE_ComplaintData_By_ComplaintId]
	@Id uniqueidentifier,
	@PersonName nvarchar(50),
	@PersonApartmentCode nvarchar(50),
	@Location int,
	@Category int,
	@Description nvarchar(MAX)
AS
	UPDATE Complaints
	SET 
		PersonName = @PersonName,
		PersonApartmentCode = @PersonApartmentCode,
		Location = @Location,
		Category = @Category,
		Description = @Description
	WHERE Id = @Id
RETURN 0

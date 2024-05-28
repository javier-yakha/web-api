CREATE PROCEDURE [dbo].[CREATE_Complaint]
	@PersonName nvarchar(50),
	@PersonApartmentCode nvarchar(50),
	@Location int,
	@Category int,
	@Description nvarchar(MAX),
	@OutputId uniqueidentifier OUT
AS
	SET @OutputId = NEWID();
	INSERT INTO Complaints
		(Id, PersonName, PersonApartmentCode,
		Location, Category, Description,
		Status, DateActivated)
	VALUES (@OutputId, @PersonName, @PersonApartmentCode,
		@Location, @Category, @Description,
		1, SYSUTCDATETIME())
RETURN

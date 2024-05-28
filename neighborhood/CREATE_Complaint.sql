CREATE PROCEDURE [dbo].[CREATE_Complaint]
	@PersonName nvarchar(50),
	@PersonApartmentCode nvarchar(50),
	@Location int,
	@Category int,
	@Description nvarchar(MAX)
AS
	INSERT INTO Complaints
		(Id, PersonName, PersonApartmentCode,
		Location, Category, Description,
		Status, DateActivated)
	VALUES (NEWID(), @PersonName, @PersonApartmentCode,
		@Location, @Category, @Description,
		1, SYSUTCDATETIME())
RETURN 0

CREATE PROCEDURE [dbo].[CREATE_Complaint]
	@Id uniqueidentifier,
	@PersonName nvarchar(50),
	@PersonApartmentCode nvarchar(50),
	@Location int,
	@Category int,
	@Description nvarchar(MAX),
	@Status int,
	@DateActivated datetime
AS
	INSERT INTO Complaints
		(Id, PersonName, PersonApartmentCode,
		Location, Category, Description,
		Status, DateActivated)
	VALUES (NEWID(), @PersonName, @PersonApartmentCode,
		@Location, @Category, @Description,
		@Status, SYSUTCDATETIME())
RETURN 0

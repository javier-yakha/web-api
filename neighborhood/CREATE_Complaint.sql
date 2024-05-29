CREATE PROCEDURE [dbo].[CREATE_Complaint]
	@Id uniqueidentifier,
	@PersonName nvarchar(50),
	@PersonApartmentCode nvarchar(50),
	@Location int,
	@Category int,
	@Description nvarchar(2000)
AS
	INSERT INTO Complaints
		(Id,
		PersonName,
		PersonApartmentCode,
		Location,
		Category,
		Description,
		CurrentStatus,
		DateActivated)
	VALUES 
		(@Id,
		@PersonName,
		@PersonApartmentCode,
		@Location,
		@Category,
		@Description,
		1,
		SYSUTCDATETIME())
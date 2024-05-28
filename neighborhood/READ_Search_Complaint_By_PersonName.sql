CREATE PROCEDURE [dbo].[READ_Search_Complaint_By_PersonName]
	@PersonName nvarchar(50)
AS
	SELECT Id, PersonName, PersonApartmentCode,
		Location, Category, Description,
		Status, DateActivated, DateDeActivated
	FROM Complaints
	WHERE PersonName like '%' + @PersonName + '%'
RETURN 0

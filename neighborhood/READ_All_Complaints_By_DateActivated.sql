CREATE PROCEDURE [dbo].[READ_All_Complaints_By_DateActivated]
AS
	SELECT 
		Id, PersonName, PersonApartmentCode,
		Location, Category, Description,
		Status, DateActivated, DateDeActivated
	FROM Complaints
	ORDER BY DateActivated
RETURN 0

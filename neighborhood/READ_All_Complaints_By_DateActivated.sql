﻿CREATE PROCEDURE [dbo].[READ_All_Complaints_By_DateActivated]
AS
SELECT 
	Id,
	PersonName,
	PersonApartmentCode,
	Location,
	Category,
	Description,
	CurrentStatus,
	DateActivated,
	LastUpdated
FROM Complaints
ORDER BY DateActivated
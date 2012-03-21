SELECT 
	dbo.Pharmacies.Id, dbo.Pharmacies.UPICode, dbo.Pharmacies.Phone, dbo.Credentials.Password, dbo.Pharmacies.Name, dbo.Pharmacies.Address, dbo.Pharmacies.Street, 
	dbo.Channels.Name AS ChannelName, 
	dbo.Salesmen.Name AS SalesmenName, dbo.Salesmen.Phone AS SalesmenPhone, dbo.Salesmen.Id AS SalesmenID
FROM 
	dbo.Pharmacies LEFT JOIN 
	dbo.PharmacyCredentials ON dbo.Pharmacies.Id = dbo.PharmacyCredentials.Pharmacy_Id LEFT JOIN 
	dbo.Credentials ON dbo.PharmacyCredentials.Credential_Id = dbo.Credentials.Id LEFT JOIN 
	dbo.Channels ON dbo.Pharmacies.ChannelId = dbo.Channels.Id LEFT JOIN 
	dbo.Salesmen ON dbo.Pharmacies.SalesmanId = dbo.Salesmen.Id
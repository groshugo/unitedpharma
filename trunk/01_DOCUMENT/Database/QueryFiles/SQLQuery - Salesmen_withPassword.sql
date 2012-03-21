SELECT     dbo.Salesmen.Name, dbo.Credentials.Password, dbo.SalesmanCredentials.Salesman_Id, dbo.Salesmen.Id, dbo.Salesmen.UPICode, 
                      dbo.Salesmen.Phone, dbo.Salesmen.RoleId
FROM         dbo.Credentials INNER JOIN
                      dbo.SalesmanCredentials ON dbo.Credentials.Id = dbo.SalesmanCredentials.Credential_Id INNER JOIN
                      dbo.Salesmen ON dbo.SalesmanCredentials.Salesman_Id = dbo.Salesmen.Id
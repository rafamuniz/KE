SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-05-20
-- Description:	Get MAX Contacts
-- =============================================
CREATE PROCEDURE GetMaxContact
	@customerId UNIQUEIDENTIFIER	
AS
BEGIN	
	SET NOCOUNT ON;
	    
	SELECT 
		MAX(c.RowVersion) AS MaxRowVersion
	FROM 
		Contacts c
	WHERE 
		c.CustomerId = @customerId		
END
GO

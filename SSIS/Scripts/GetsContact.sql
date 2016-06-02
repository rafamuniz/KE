SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-05-20
-- Description:	Gets Contacts
-- =============================================
ALTER PROCEDURE GetsContact
	@customerId UNIQUEIDENTIFIER
	, @rowVersion TIMESTAMP
AS
BEGIN	
	SET NOCOUNT ON;
	    
	SELECT 
		*
	FROM 
		Contacts c
	WHERE 
		c.CustomerId = @customerId
		AND c.RowVersion > @rowVersion
END
GO

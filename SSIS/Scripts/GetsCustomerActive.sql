SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-05-20
-- Description:	Get All active customers
-- =============================================
CREATE PROCEDURE GetsCustomerActive

AS
BEGIN	
	SET NOCOUNT ON;
	    
	SELECT 
		*
	FROM 
		Customers c
		INNER JOIN AspNetUsers u ON u.Id = c.Id
	WHERE 
		c.DeletedDate IS NOT NULL
END
GO

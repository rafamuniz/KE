SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-05-20
-- Description:	Get All SITES
-- =============================================
CREATE PROCEDURE GetsSite
	@customerId UNIQUEIDENTIFIER
AS
BEGIN	
	SET NOCOUNT ON;
	    
	SELECT 
		*
	FROM 
		Sites s		
	WHERE 
		s.CustomerId = @customerId
END
GO

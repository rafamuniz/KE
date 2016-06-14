SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets Customer User
-- =============================================
CREATE PROCEDURE GetsCustomerUser
	@siteId VARCHAR(15)	
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		*
	FROM
		CustomerUsers cu WITH(NOLOCK)
		JOIN CustomerUserSites cus WITH(NOLOCK) ON cus.CustomerUserId = cu.Id
	WHERE
		cus.SiteId = @siteId		
END
GO

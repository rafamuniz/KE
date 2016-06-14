SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Get Site
-- =============================================
CREATE PROCEDURE GetSite
	@siteId VARCHAR(15)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		*
	FROM
		Sites s WITH(NOLOCK)
	WHERE
		s.Id = @siteId
END
GO

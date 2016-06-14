SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets Country
-- =============================================
CREATE PROCEDURE GetsCountry
	
BEGIN
	SET NOCOUNT ON;

	SELECT
		*
	FROM
		Countries
	
END
GO

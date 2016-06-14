SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets Geometry
-- =============================================
CREATE PROCEDURE GetsGeometry
AS	
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		*
	FROM
		Geometries
END
GO

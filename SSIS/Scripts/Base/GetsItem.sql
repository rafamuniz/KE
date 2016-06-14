SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets Item
-- =============================================
CREATE PROCEDURE GetsItem
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		*
	FROM
		Items i WITH(NOLOCK)
			
END
GO

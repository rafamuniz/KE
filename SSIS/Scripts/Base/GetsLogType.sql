SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets LogType
-- =============================================
CREATE PROCEDURE GetsLogType
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		*
	FROM 
		LogTypes lt WITH(NOLOCK)
		
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Rafael Muniz
-- Create date: 2016-06-13
-- Description:	Gets OperatorType
-- =============================================
CREATE PROCEDURE GetsOperatorType
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		*
	FROM
		OperatorTypes ot WITH(NOLOCK)
		
END
GO

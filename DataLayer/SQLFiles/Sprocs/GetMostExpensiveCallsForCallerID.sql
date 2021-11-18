USE [CDR]
GO
/****** Object:  StoredProcedure [dbo].[GetMostExpensiveCallsForCallerID]    Script Date: 18/11/2021 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bob Ginn
-- Create date: 18/11/21
-- Description:	Returns Count of calls along with their duration for date range and specified caller id, type id is optional
-- =============================================
ALTER PROCEDURE [dbo].[GetMostExpensiveCallsForCallerID]
	-- Add the parameters for the stored procedure here
@Start DATETIME
,@End DATETIME
,@CallerId VARCHAR(14)
,@ReturnCount INT
,@TypeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @CallerID = REPLACE(REPLACE(@CallerID,' ',''), '44', '07')
	  DECLARE @MaxPrice DECIMAL(18,2)
  SET @MaxPrice = (SELECT MAX([Cost]) FROM [CallDetailRecord]);

  IF(@TypeId <> NULL)
	(
		SELECT TOP (@ReturnCount)* FROM [CallDetailRecord]	
		WHERE [Cost] = @MaxPrice AND TypeId = @TypeId AND Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
	--	ORDER BY Call_Date ASC
  
	)
  ELSE
	(
		SELECT TOP (@ReturnCount) * FROM [CallDetailRecord]	
		WHERE [Cost] = @MaxPrice AND TypeId = @TypeId AND Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
		--ORDER BY Call_Date ASC
	)
END

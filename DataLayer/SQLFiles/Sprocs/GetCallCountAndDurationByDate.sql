USE [CDR]
GO
/****** Object:  StoredProcedure [dbo].[GetCallCountAndDurationByDate]    Script Date: 18/11/2021 16:29:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bob Ginn
-- Create date: 18/11/21
-- Description:	Returns Count of calls along with their duration for date range and specified caller id, type id is optional
-- =============================================
ALTER PROCEDURE [dbo].[GetCallCountAndDurationByDate]
	-- Add the parameters for the stored procedure here
@Start DATETIME
,@End DATETIME
,@CallerID VARCHAR(14)
,@TypeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @CallerID = REPLACE(@CallerID,' ','')
	IF(@TypeId <> NULL)
	(
	SELECT COUNT(Caller_id) AS NumberOfCalls, Call_Date
	FROM [dbo].[CallDetailRecord]
	
  WHERE TypeId = @TypeId AND Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
  GROUP BY Call_Date
  )
  ELSE
  (
  SELECT COUNT(Caller_id) AS NumberOfCalls, Call_Date
	FROM [dbo].[CallDetailRecord]
	
  WHERE Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
  GROUP BY Call_Date
  )
END

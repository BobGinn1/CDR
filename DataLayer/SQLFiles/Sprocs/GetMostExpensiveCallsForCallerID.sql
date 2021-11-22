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
	SET @CallerID = REPLACE(@CallerID,' ','')
	  DECLARE @MaxPrice DECIMAL(18,3)
  SET @MaxPrice = (SELECT MAX([Cost]) FROM [CallDetailRecord]);

  IF(@TypeId <> NULL)
	(
		SELECT TOP (@ReturnCount)
		cdr.ID
	  ,[Caller_Id] AS CallerID
      ,[Recipient]
      ,[Call_Date] AS CallDate
      ,[End_Time] AS EndTime
      ,[Duration] 
      ,[Cost]
      ,[Reference]
      ,[Currency]
      ,t.ID AS [TypeId]
  FROM [CDR].[dbo].[CallDetailRecord] cdr
  INNER JOIN [Type] t on cdr.TypeId = t.Id
		
		WHERE [Cost] = @MaxPrice AND TypeId = @TypeId AND Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
  
	)
  ELSE
	(
		SELECT TOP (@ReturnCount)
		cdr.ID
	  ,[Caller_Id] AS CallerID
      ,[Recipient]
      ,[Call_Date] AS CallDate
      ,[End_Time] AS EndTime
      ,[Duration] 
      ,[Cost]
      ,[Reference]
      ,[Currency]
      ,t.Id AS [TypeId]
  FROM [CDR].[dbo].[CallDetailRecord] cdr
  INNER JOIN [Type] t on cdr.TypeId = t.Id
		WHERE [Cost] = @MaxPrice AND TypeId = @TypeId AND Caller_Id = LTRIM(RTRIM(@CallerID)) AND Call_Date BETWEEN @Start AND @End
	)
END

USE [CDR]
GO
/****** Object:  StoredProcedure [dbo].[GetCDRByID]    Script Date: 19/11/2021 15:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bob Ginn
-- Create date: 18/11/21
-- Description:	Returns Individual CDR record for known ID
-- =============================================
ALTER PROCEDURE [dbo].[GetCDRByID]
	-- Add the parameters for the stored procedure here
@CDRId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
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
  WHERE cdr.ID = @CDRID
END

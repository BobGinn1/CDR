USE [CDR]
GO
/****** Object:  StoredProcedure [dbo].[GetCDRByID]    Script Date: 18/11/2021 16:29:35 ******/
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
	SELECT [Caller_Id]
      ,[Recipient]
      ,[Call_Date]
      ,[End_Time]
      ,[Duration]
      ,[Cost]
      ,[Reference]
      ,[Currency]
      ,[TypeId]
  FROM [CDR].[dbo].[CallDetailRecord]
  WHERE ID = @CDRID
END

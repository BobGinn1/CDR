USE [CDR]
GO
/****** Object:  StoredProcedure [dbo].[InsertCDRRecords]    Script Date: 18/11/2021 16:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bob Ginn
-- Create date: 18/11/21
-- Description:	Writes CDR records to DB
-- =============================================
ALTER PROCEDURE [dbo].[InsertCDRRecords]
	-- Add the parameters for the stored procedure here
@CallerId VARCHAR(14)
,@Recipient VARCHAR(255)
,@CallDate DATETIME
,@EndTime TIME
,@Duration INT
,@Cost DECIMAL(18,3)
,@Reference VARCHAR(255)
,@Currency VARCHAR(5)
,@TypeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[CallDetailRecord]
	(
	[Caller_Id]
      ,[Recipient]
      ,[Call_Date]
      ,[End_Time]
      ,[Duration]
      ,[Cost]
      ,[Reference]
      ,[Currency]
      ,[TypeId]
	)
	VALUES
	(
	@CallerId
	,@Recipient
	,@CallDate
	,@EndTime
	,@Duration
	,@Cost
	,@Reference
	,@Currency
	,@TypeId
	)
END

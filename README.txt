INSTRUCTIONS
-------------

1.Open app settings.json

1a.Change connection string to reflect your database
1b.Change File, Done, and Log directories

2.Run all SQL files that exist in the SQLFiles folder of the DataLayer. NB: run type table, then CDR table, then all sprocs + data insert

3.To upload a file place the CSV in the File directory as specified in the appsettings.json, then use curl to hit the upload endpoint, example below.

NB:
Once complete the file will move to the "done" folder
Any failed records will be printed into a text file in the "log" folder


To hit the endpoints you can send curl commands via command line with the sln running. Change params as desired. Please note you will need to update the example url to match your own. Example commands are as follows.

No Params
---------
Upload: curl https://localhost:44356/write/upload


cdrId as Param - Single CDR
------------------------------
curl https://localhost:44356/read/GetSingleCDR?cdrId=5


startDate, endDate, callerId, typeId as Params - Call count/duration by date for caller id with optional type
---------------------------------------------------------------------------------------------------------------
curl https://localhost:44356/read/GetCallCountAndDurationByDateForCallerId/2016-08-01/2016-09-01/441269000000/2


startDate, endDate, callerId, noRecordsToReturn, typeId as Params - Most expensive call by date for caller id with optional type
--------------------------------------------------------------------------------------------------------------------------------------
curl https://localhost:44356/read/GetMostExpensiveCallCountByDateForCallerId/2016-08-01/2016-09-01/10/441269000000/2
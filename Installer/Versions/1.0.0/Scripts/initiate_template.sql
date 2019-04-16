USE [ihstemplate]
GO

/****** Object:  StoredProcedure [dbo].[initiate_template]    Script Date: 2019-04-08 21:28:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[initiate_template]
as
begin
create table [General].Settings
(
	databaseId uniqueIdentifier
		DEFAULT NEWID(),
	versionNumber varchar(10),
);

create table [General].VersionHistory
(
	versionNumber varchar(10),
	timeOfUpgrade DATETIME2
);
end
GO


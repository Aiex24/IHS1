USE [ihs1]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentVersion]    Script Date: 2019-02-23 21:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCurrentVersion]

AS
	SELECT MAX(VersionNumber) as 'VersionNumber' FROM [General].[Settings]
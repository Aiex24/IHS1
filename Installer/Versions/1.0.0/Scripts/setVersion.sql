CREATE PROC setVersion
@databaseId uniqueIdentifier,
@versionNumber varchar(10)
AS
BEGIN
BEGIN TRAN;
-- if row exists where databaseid = @databaseId, then update this row. Else, insert new row.
UPDATE General.Settings SET VersionNumber = @versionNumber WHERE databaseId = @databaseId;
IF @@ROWCOUNT = 0
	INSERT INTO [General].[Settings] VALUES(@databaseId, @versionNumber);

	INSERT INTO [General].VersionHistory VALUES(@versionNumber, GETDATE());
COMMIT TRAN;
END;
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816021254_Initial')
BEGIN
    CREATE TABLE [Feedings] (
        [Id] int NOT NULL IDENTITY,
        [StartingWeight] int NOT NULL,
        [EndWeight] int NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [KittenId] int NOT NULL,
        CONSTRAINT [PK_Feedings] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816021254_Initial')
BEGIN
    CREATE TABLE [Fosters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Cincinnati] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        CONSTRAINT [PK_Fosters] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816021254_Initial')
BEGIN
    CREATE TABLE [Kittens] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [CurrentWeight] int NOT NULL,
        [Sex] nvarchar(max) NULL,
        [FosterId] int NOT NULL,
        CONSTRAINT [PK_Kittens] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816021254_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816021254_Initial', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816030348_UpdateTables')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fosters]') AND [c].[name] = N'Cincinnati');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Fosters] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Fosters] DROP COLUMN [Cincinnati];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816030348_UpdateTables')
BEGIN
    ALTER TABLE [Fosters] ADD [City] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816030348_UpdateTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816030348_UpdateTables', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816171230_AllowNullableFosterId')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Kittens]') AND [c].[name] = N'FosterId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Kittens] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Kittens] ALTER COLUMN [FosterId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816171230_AllowNullableFosterId')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fosters]') AND [c].[name] = N'Name');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Fosters] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Fosters] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816171230_AllowNullableFosterId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816171230_AllowNullableFosterId', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816192324_UpdatedFostersTable')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Kittens]') AND [c].[name] = N'Name');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Kittens] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Kittens] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816192324_UpdatedFostersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816192324_UpdatedFostersTable', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816212522_Updates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816212522_Updates', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200816221115_Updates2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200816221115_Updates2', N'3.1.7');
END;

GO


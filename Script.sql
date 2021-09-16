IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [TransactionDetails] (
    [Id] int NOT NULL IDENTITY,
    [PaymentId] uniqueidentifier NOT NULL,
    [Amount] decimal(18, 2) NOT NULL,
    [Currency] nvarchar(max) NOT NULL,
    [CardholderNumber] nvarchar(max) NOT NULL,
    [HolderName] nvarchar(max) NOT NULL,
    [ExpirationMonth] int NOT NULL,
    [ExpirationYear] int NOT NULL,
    [CVV] int NOT NULL,
    [OrderReference] nvarchar(50) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_TransactionDetails] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210915022449_initial', N'3.1.19');

GO


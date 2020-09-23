CREATE TABLE [dbo].[Accounts] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress]   NVARCHAR (200) NULL,
    [HashedPassword] NVARCHAR (32)  NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Accounts_ByEmailAddress]
    ON [dbo].[Accounts]([EmailAddress] ASC) WITH (IGNORE_DUP_KEY = ON);


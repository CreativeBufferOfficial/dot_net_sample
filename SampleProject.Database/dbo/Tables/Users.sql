CREATE TABLE [dbo].[Users] (
    [UserId]          INT              IDENTITY (1, 1) NOT NULL,
    [UserGuid]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName]       VARCHAR (100)    NOT NULL,
    [LastName]        VARCHAR (100)    NULL,
    [PhoneNumber]     VARCHAR (50)     NULL,
    [Email]           VARCHAR (200)    NOT NULL,
    [PasswordHash]    VARCHAR (500)    NOT NULL,
    [RoleId]          INT              NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [IsDeleted]       BIT              CONSTRAINT [DF_Users_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]       INT              NOT NULL,
    [CreatedDateUtc]  DATETIME         NOT NULL,
    [ModifiedBy]      INT              NOT NULL,
    [ModifiedDateUtc] DATETIME         NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);


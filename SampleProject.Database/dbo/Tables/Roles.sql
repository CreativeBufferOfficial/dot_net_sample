CREATE TABLE [dbo].[Roles] (
    [RoleId]         INT              NOT NULL,
    [RoleGuid]       UNIQUEIDENTIFIER NOT NULL,
    [RoleName]       VARCHAR (100)    NOT NULL,
    [CreatedDateUtc] DATETIME         NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


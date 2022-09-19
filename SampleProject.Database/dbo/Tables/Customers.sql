CREATE TABLE [dbo].[Customers] (
    [CustomerId]      INT              IDENTITY (1, 1) NOT NULL,
    [CustomerGuid]    UNIQUEIDENTIFIER NOT NULL,
    [CustomerName]    VARCHAR (100)    NOT NULL,
    [PhoneNumber]     VARCHAR (50)     NULL,
    [Email]           VARCHAR (200)    NOT NULL,
    [Address]         VARCHAR (2000)   NOT NULL,
    [City]            VARCHAR (100)    NOT NULL,
    [State]           VARCHAR (100)    NOT NULL,
    [Country]         VARCHAR (100)    NOT NULL,
    [Zip]             VARCHAR (10)     NOT NULL,
    [IsDeleted]       BIT              NOT NULL,
    [CreatedBy]       INT              NOT NULL,
    [CreatedDateUtc]  DATETIME         NOT NULL,
    [ModifiedBy]      INT              NOT NULL,
    [ModifiedDateUtc] DATETIME         NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);


CREATE TABLE [dbo].[ExceptionLog] (
    [ExceptionLogId] INT           IDENTITY (1, 1) NOT NULL,
    [Source]         VARCHAR (500) NULL,
    [StackTrace]     VARCHAR (MAX) NULL,
    [Message]        VARCHAR (500) NULL,
    [OccuredDateUtc] DATETIME      NOT NULL,
    [UserId]         INT           NULL,
    CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED ([ExceptionLogId] ASC),
    CONSTRAINT [FK_ExceptionLog_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


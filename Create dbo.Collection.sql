USE [ArogyaPortal20230506112748_db]
GO

/****** Object: Table [dbo].[Payment] Script Date: 16-05-2023 19:25:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Collection] (
    [Patientcode] VARCHAR (50) NOT NULL,
    [PayDate]     DATE         NOT NULL,
    [Amount]      INT          NOT NULL
);

ALTER TABLE [Collection] ADD CONSTRAINT DF_Collection DEFAULT GETDATE() FOR PayDate;

USE [MvcMovieContext]
GO

/****** Object:  Table [dbo].[DayItem]    Script Date: 7/1/2018 2:44:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[DayItems];
DROP TABLE [dbo].[Items];
DROP TABLE [dbo].[HistoryItems];
GO

CREATE TABLE [dbo].[DayItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemDay] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DueBy] [datetime2](7) NULL,
	[IsDone] [bit] NOT NULL,
	[IconPath] [nvarchar](255) NULL
 CONSTRAINT [PK_DayItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DueBy] [datetime2](7) NULL,	
	[IsDaily] [bit] NOT NULL default 1,
	[SpecificDate] [datetime2](7) NULL,	
	[IconPath] [nvarchar](255) NULL
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[HistoryItems](
	[HistoryItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[DayItem] [datetime2](7) NOT NULL default GETDATE(),
	[IsDone] [bit] NOT NULL default 0,
	[IconPath] [nvarchar](255) NULL,
 CONSTRAINT [PK_HistotyItem] PRIMARY KEY CLUSTERED 
(
	[HistoryItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HistoryItems]  WITH CHECK ADD  CONSTRAINT [FK_HistoryItems_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ItemId])
GO

ALTER TABLE [dbo].[HistoryItems] CHECK CONSTRAINT [FK_HistoryItems_Items]
GO

DELETE FROM [dbo].[DayItems];
DELETE FROM [dbo].[Items];

DELETE FROM [dbo].[HistoryItems];

INSERT INTO [dbo].[Items]([Name], [DueBy]) VALUES(N'Почистить зубки', N'2018-07-01 08:00:00.0000000');
INSERT INTO [dbo].[Items]([Name], [DueBy]) VALUES(N'Почитать', NULL);
INSERT INTO [dbo].[Items]([Name], [DueBy]) VALUES(N'Почистить зубки', N'2018-07-01 20:00:00.0000000');




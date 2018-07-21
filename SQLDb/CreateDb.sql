USE [MvcMovieContext]
GO

/****** Object:  Table [dbo].[DayItem]    Script Date: 7/1/2018 2:44:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Pocket];
DROP TABLE [dbo].[Items];
DROP TABLE [dbo].[HistoryItems];
DROP TABLE [dbo].[Rewards];
DROP TABLE [dbo].[RewardsHistory];
GO

CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DueBy] [datetime2](7) NULL,
	[IsDaily] [bit] NOT NULL,
	[SpecificDate] [datetime2](7) NULL,
	[IconPath] [nvarchar](255) NULL,
	[IsGood] [bit] NULL,
	[IsOneTime] [bit] NULL,
	[Weight] [int] NOT NULL,
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

CREATE TABLE [dbo].[Pocket](
	[PocketId] [int] IDENTITY(1,1) NOT NULL,
	[Balance] [int] NOT NULL,0
 CONSTRAINT [PK_PocketId] PRIMARY KEY CLUSTERED 
(
	[PocketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Rewards](
	[RewardId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IconPath] [nvarchar](max) NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_RewardId] PRIMARY KEY CLUSTERED 
(
	[RewardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RewardsHistory](
	[RewardHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[RewardId] [int],
	[Date] [datetime2](7) NULL,
 CONSTRAINT [PK_RewardHistoryId] PRIMARY KEY CLUSTERED 
(
	[RewardHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RewardsHistory]  WITH CHECK ADD  CONSTRAINT [FK_RewardsHistory_Rewards] FOREIGN KEY([RewardId])
REFERENCES [dbo].[Rewards] ([RewardId])
GO

ALTER TABLE [dbo].[RewardsHistory] CHECK CONSTRAINT [FK_RewardsHistory_Rewards]
GO
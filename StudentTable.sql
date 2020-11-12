/****** Object:  Table [dbo].[Student]    Script Date: 11/2/2020 7:01:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[CollegeId] [varchar](15) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[GateRank] [smallint] NOT NULL,
	[GateScore] [smallint] NOT NULL,
	[BtechScore] [float] NOT NULL,
	[Supervisor] [tinyint] NULL,
	[Preference] [varchar](150) NULL,
	[ChoiceFilled] [tinyint] NULL,
	[ChoiceAlloted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CollegeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Student] ADD  DEFAULT ((0)) FOR [ChoiceFilled]
GO

ALTER TABLE [dbo].[Student] ADD  DEFAULT ((0)) FOR [ChoiceAlloted]
GO


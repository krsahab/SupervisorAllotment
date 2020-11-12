/****** Object:  Table [dbo].[Supervisor]    Script Date: 11/2/2020 7:00:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Supervisor](
	[ID] [tinyint] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](20) NULL,
	[StudentsAlloted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Supervisor] ADD  DEFAULT ((0)) FOR [StudentsAlloted]
GO


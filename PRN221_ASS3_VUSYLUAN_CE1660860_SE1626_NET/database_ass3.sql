USE [SignalRAssignment3]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/3/2023 6:18:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 11/3/2023 6:18:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategories]    Script Date: 11/3/2023 6:18:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PostCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 11/3/2023 6:18:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorID] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[PublicStatus] [bit] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231030063718_inittial', N'6.0.24')
GO
SET IDENTITY_INSERT [dbo].[AppUsers] ON 

INSERT [dbo].[AppUsers] ([UserID], [FullName], [Address], [Email], [Password]) VALUES (2, N'HH', N'HH', N'admin@gmail.com', N'123456')
INSERT [dbo].[AppUsers] ([UserID], [FullName], [Address], [Email], [Password]) VALUES (3, N'Sữa chua', N'HH', N'luanvsce160860@fpt.edu.vn', N'1234567')
SET IDENTITY_INSERT [dbo].[AppUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[PostCategories] ON 

INSERT [dbo].[PostCategories] ([CategoryID], [CategoryName], [Description]) VALUES (1, N'Pizza', N'ngon')
SET IDENTITY_INSERT [dbo].[PostCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([PostID], [AuthorID], [CreatedDate], [UpdatedDate], [Title], [Content], [PublicStatus], [CategoryID]) VALUES (2, 2, CAST(N'2023-11-18T06:02:00.0000000' AS DateTime2), CAST(N'2023-12-02T06:02:00.0000000' AS DateTime2), N'Home', N'Thom ngon', 0, 1)
INSERT [dbo].[Posts] ([PostID], [AuthorID], [CreatedDate], [UpdatedDate], [Title], [Content], [PublicStatus], [CategoryID]) VALUES (3, 2, CAST(N'2023-11-15T06:07:00.0000000' AS DateTime2), CAST(N'2023-11-15T06:07:00.0000000' AS DateTime2), N'Home', N'Thom ngon', 1, 1)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_AppUsers_AuthorID] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[AppUsers] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_AppUsers_AuthorID]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostCategories_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[PostCategories] ([CategoryID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostCategories_CategoryID]
GO

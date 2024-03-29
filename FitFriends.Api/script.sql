CREATE DATABASE [FitFriends]
GO
USE [FitFriends]
GO
/****** Object:  Schema [Types]    Script Date: 28.01.2024 16:54:35 ******/
CREATE SCHEMA [Types]
GO
/****** Object:  Schema [User]    Script Date: 28.01.2024 16:54:35 ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Types].[UserTrainingTypes]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Types].[UserTrainingTypes](
	[UserTrainingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[TrainingType] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_UserTrainingTypes] PRIMARY KEY CLUSTERED 
(
	[UserTrainingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[Certificates]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Certificates](
	[CertificateId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CertificateName] [nvarchar](255) NOT NULL,
	[Image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CertificateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [User].[TrainerSurveys]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[TrainerSurveys](
	[UserId] [uniqueidentifier] NOT NULL,
	[TrainerAchievements] [nvarchar](140) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[Users]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Password] [nvarchar](12) NOT NULL,
	[Gender] [nvarchar](12) NOT NULL,
	[DateBirth] [date] NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](140) NULL,
	[Station] [nvarchar](50) NOT NULL,
	[ImageForPage] [nvarchar](max) NOT NULL,
	[LevelPreparation] [nvarchar](12) NOT NULL,
	[IsReady] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [User].[UserSurveys]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[UserSurveys](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserCaloriesToLose] [int] NOT NULL,
	[UserDailyCaloriesToBurn] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Types].[UserTrainingTypes]  WITH CHECK ADD  CONSTRAINT [FK_UserTrainingTypes_Users] FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [Types].[UserTrainingTypes] CHECK CONSTRAINT [FK_UserTrainingTypes_Users]
GO
ALTER TABLE [User].[Certificates]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [User].[TrainerSurveys]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
GO
ALTER TABLE [User].[TrainerSurveys]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
GO
ALTER TABLE [User].[UserSurveys]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
GO
ALTER TABLE [User].[UserSurveys]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([UserId])
GO
/****** Object:  StoredProcedure [User].[usp_Delete_Certificate]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [User].[usp_Delete_Certificate]
    @CertificateId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [User].[Certificates]
    WHERE CertificateId = @CertificateId;
END;
GO
/****** Object:  StoredProcedure [User].[usp_Delete_User]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [User].[usp_Delete_User]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM [User].[Users]
    WHERE [UserId] = @UserId;
END
GO
/****** Object:  StoredProcedure [User].[usp_Get_All_Certificates_By_User]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [User].[usp_Get_All_Certificates_By_User]
    @UserId uniqueidentifier
AS
BEGIN
    SELECT * 
	FROM [User].[Certificates]
	WHERE UserId = @UserId
END;
GO
/****** Object:  StoredProcedure [User].[usp_Get_All_Users]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [User].[usp_Get_All_Users]
    @PageSize INT,
    @Offset INT
AS
BEGIN
    SELECT   UserId,
             Name,
             Email,
             Avatar,
             Password,
             Gender,
             DateBirth,
             Role,
             Description,
             Station,
             ImageForPage,
             CreatedAt,
             LevelPreparation,
             IsReady

    FROM     [User].Users
    ORDER BY CreatedAt
    OFFSET @Offset 
    ROWS FETCH NEXT @PageSize 
    ROWS ONLY;
END;
GO
/****** Object:  StoredProcedure [User].[usp_Get_Certificate_By_Id]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [User].[usp_Get_Certificate_By_Id]
    @CertificateId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM [User].[Certificates]
    WHERE CertificateId = @CertificateId;
END;
GO
/****** Object:  StoredProcedure [User].[usp_Get_User_By_Id]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [User].[usp_Get_User_By_Id]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT   UserId,
             Name,
             Email,
             Avatar,
             Password,
             Gender,
             DateBirth,
             Role,
             Description,
             Station,
             ImageForPage,
             CreatedAt,
             LevelPreparation,
             IsReady

    FROM     [User].Users

    WHERE    UserId = @UserId;
END;
GO
/****** Object:  StoredProcedure [User].[usp_Insert_Certificate]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [User].[usp_Insert_Certificate]
    @CertificateId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @CertificateName NVARCHAR(255),
    @Image NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO [User].[Certificates] (CertificateId, UserId, CertificateName, Image)
    VALUES (@CertificateId, @UserId, @CertificateName, @Image);
END;
GO
/****** Object:  StoredProcedure [User].[usp_Insert_User]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [User].[usp_Insert_User]
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(15),
    @Email NVARCHAR(50),
    @Avatar NVARCHAR(MAX),
    @Password NVARCHAR(12),
    @Gender NVARCHAR(12),
    @DateBirth DATETIME,
    @Role NVARCHAR(50),
    @Description NVARCHAR(140),
    @Station NVARCHAR(50),
    @ImageForPage NVARCHAR(12),
    @LevelPreparation NVARCHAR (12),
    @IsReady BIT,
    @CreatedAt DATETIME

AS
BEGIN
    INSERT INTO [User].[Users]
        ([UserId], [Name], [Email], [Avatar], [Password], [Gender], [DateBirth], [Role],
         [Description], [Station], [ImageForPage], [LevelPreparation], [IsReady], [CreatedAt])
    VALUES
        (@UserId, @Name, @Email, @Avatar, @Password, @Gender, @DateBirth, @Role,
         @Description, @Station, @ImageForPage, @LevelPreparation, @IsReady, @CreatedAt);
END;
GO
/****** Object:  StoredProcedure [User].[usp_Update_Certificate]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [User].[usp_Update_Certificate]
    @CertificateId UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @CertificateName NVARCHAR(255),
    @Image NVARCHAR(MAX)
AS
BEGIN
    UPDATE [User].[Certificates]
    SET UserId = @UserId,
        CertificateName = @CertificateName,
        Image = @Image
    WHERE CertificateId = @CertificateId;
END;
GO
/****** Object:  StoredProcedure [User].[usp_Update_User]    Script Date: 28.01.2024 16:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [User].[usp_Update_User]
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(15),
    @Email NVARCHAR(50),
    @Avatar NVARCHAR(MAX),
    @Password NVARCHAR(12),
    @Gender NVARCHAR(12),
    @DateBirth DATETIME,
    @Role NVARCHAR(50),
    @Description NVARCHAR(140),
    @Station NVARCHAR(50),
    @ImageForPage NVARCHAR(12),
    @LevelPreparation NVARCHAR (12),
    @IsReady BIT
AS
BEGIN
    UPDATE [User].[Users]
    SET
        [Name] = @Name,
        [Email] = @Email,
        [Avatar] = @Avatar,
        [Password] = @Password,
        [Gender] = @Gender,
        [DateBirth] = @DateBirth,
        [Role] = @Role,
        [Description] = @Description,
        [Station] = @Station,
        [ImageForPage] = @ImageForPage,
        [LevelPreparation] = @LevelPreparation,
        [IsReady] = @IsReady
    WHERE [UserId] = @UserId;
END;
GO

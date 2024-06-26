USE [StudentRegistration]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Mobile] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[NIC] [varchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Address] [varchar](255) NULL,
	[ProfileImageUrl] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students_Archive]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students_Archive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[FirstName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Mobile] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[NIC] [varchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Address] [varchar](255) NULL,
	[ProfileImageUrl] [varchar](max) NULL,
	[dtArchived] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentById]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Puravi Asirini
-- Create date: 2024/06/15
-- Description:	Delete student record by student id
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStudentById]
@studentId int
AS
BEGIN

	SET NOCOUNT ON;

	--Insert record to archive table before delete from original table

	INSERT INTO Students_Archive (StudentId,FirstName,LastName,Mobile,Email,NIC,DateOfBirth,Address,ProfileImageUrl,dtArchived)
	SELECT StudentId,FirstName,LastName,Mobile,Email,NIC,DateOfBirth,Address,ProfileImageUrl,getdate()
	FROM Students
	WHERE StudentId = @studentId;



	DELETE FROM Students
	WHERE StudentId = @studentId;


END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudents]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Puravi Asirini
-- Create date: 2024/06/15
-- Description:	Get all student records in table
-- =============================================
CREATE PROCEDURE [dbo].[GetAllStudents]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT * from Students
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDetailsById]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Puravi Asirini
-- Create date: 2024/06/15
-- Description:	Get student details by student id
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentDetailsById]
@studentId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * from Students
	WHERE StudentId = @studentId

END
GO
/****** Object:  StoredProcedure [dbo].[InsertStudent]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Puravi Asirini
-- Create date: 2024/06/15
-- Description:	Add student record to table
-- =============================================
CREATE PROCEDURE [dbo].[InsertStudent] 
@firstName varchar(255),
@lastName varchar(255),
@mobile varchar(255),
@email varchar(255),
@nic varchar(50),
@dateOfBirth datetime,
@address varchar(255),
@profileImageUrl varchar(max)

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Students (FirstName, LastName, Mobile, Email, NIC, DateOfBirth, Address, ProfileImageUrl)
	VALUES (@firstName, @lastName, @mobile, @email, @nic, @dateOfBirth, @address, @profileImageUrl);
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 6/16/2024 10:08:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Puravi Asirini
-- Create date: 2024/06/15
-- Description:	Update a student record
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStudent]
@studentId int, 
@firstName varchar(255), 
@lastName varchar(255), 
@mobile varchar(255), 
@email varchar(255), 
@nic varchar(50), 
@dateOfBirth datetime, 
@address varchar(255), 
@profileImageUrl varchar(max)
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE Students
	SET FirstName = @firstName,
    LastName = @lastName,
    Mobile = @mobile,
	Email = @email,
	NIC = @nic,
	DateOfBirth = @dateOfBirth,
	Address = @address,
	ProfileImageUrl = @profileImageUrl
	WHERE StudentId = @studentId;
 
END
GO

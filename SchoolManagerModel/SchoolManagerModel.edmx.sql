
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/05/2016 14:11:28
-- Generated from EDMX file: C:\Users\phemmybode\Documents\Development\Languages\C#\Exercises\Potters\SchoolManager\SchoolManagerModel\SchoolManagerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SchoolDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_AccountTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_ParentStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_ParentStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_StudentAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_AcademicSessionTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_AcademicSessionTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentStudentClass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_StudentStudentClass];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentClassStudentClassBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentClasses] DROP CONSTRAINT [FK_StudentClassStudentClassBill];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Parents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parents];
GO
IF OBJECT_ID(N'[dbo].[StudentClassBills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentClassBills];
GO
IF OBJECT_ID(N'[dbo].[StudentClasses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentClasses];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[AcademicSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicSessions];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccountNo] int IDENTITY(1,1) NOT NULL,
    [CreditBalance] float  NOT NULL,
    [Fees] float  NOT NULL,
    [DebitBalance] float  NOT NULL,
    [BalanceLastTerm] float  NOT NULL,
    [Rebate] float  NOT NULL,
    [FullyPaid] bit  NOT NULL,
    [AccountNumber] varchar(9)  NULL,
    [SchoolBusFees] float  NOT NULL,
    [Student_AdmissionNo] int  NOT NULL
);
GO

-- Creating table 'Parents'
CREATE TABLE [dbo].[Parents] (
    [ParentId] int IDENTITY(1,1) NOT NULL,
    [FatherTitle] nvarchar(max)  NOT NULL,
    [FatherFirstName] nvarchar(max)  NOT NULL,
    [FatherLastName] nvarchar(max)  NOT NULL,
    [FatherPhone] nvarchar(max)  NOT NULL,
    [MotherTitle] nvarchar(max)  NOT NULL,
    [MotherFirstName] nvarchar(max)  NOT NULL,
    [MotherLastName] nvarchar(max)  NOT NULL,
    [MotherPhone] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [ParentIdentificationNo] varchar(max)  NULL
);
GO

-- Creating table 'StudentClassBills'
CREATE TABLE [dbo].[StudentClassBills] (
    [StudentClassBillId] int IDENTITY(1,1) NOT NULL,
    [SchoolFees] float  NOT NULL,
    [ExamFees] float  NOT NULL,
    [LessonFees] float  NOT NULL,
    [FirstAidFees] float  NOT NULL,
    [ComputerFees] float  NULL,
    [MusicFees] float  NULL,
    [TextBookFees] float  NOT NULL,
    [ExerciseBookFees] float  NOT NULL,
    [FormFees] float  NOT NULL,
    [UniformFees] float  NOT NULL,
    [CardiganFees] float  NOT NULL,
    [SportwearFees] float  NOT NULL,
    [AnniversaryTshirtFees] float  NOT NULL,
    [DevelopmentLevyFees] float  NOT NULL,
    [Total] float  NOT NULL,
    [StudentClass_StudentClassId] int  NOT NULL
);
GO

-- Creating table 'StudentClasses'
CREATE TABLE [dbo].[StudentClasses] (
    [StudentClassId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Count] int  NOT NULL,
    [StudentClassBill_StudentClassBillId] int  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [AdmissionNo] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Class] nvarchar(max)  NOT NULL,
    [AdmissionDate] datetime  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Age] int  NULL,
    [PreviousSchool] nvarchar(max)  NOT NULL,
    [PreviousClassPreviousSchool] nvarchar(max)  NOT NULL,
    [ParentParentId] int  NOT NULL,
    [Religion] nvarchar(max)  NOT NULL,
    [IsOnSchoolBus] bit  NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [AcademicSessionEnrolled] nvarchar(max)  NOT NULL,
    [ClassDivision] nvarchar(max)  NULL,
    [AdmissionStatus] nvarchar(max)  NOT NULL,
    [StudentClassStudentClassId] int  NOT NULL,
    [AdmissionNumber] varchar(max)  NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [TransactionId] int IDENTITY(1,1) NOT NULL,
    [Amount] float  NOT NULL,
    [Date] datetime  NOT NULL,
    [TellerNo] nvarchar(max)  NOT NULL,
    [AccountAccountNo] int  NOT NULL,
    [AcademicSessionAcademicSessionId] int  NOT NULL,
    [TransactionIdentificationNo] varchar(max)  NULL
);
GO

-- Creating table 'AcademicSessions'
CREATE TABLE [dbo].[AcademicSessions] (
    [AcademicSessionId] int IDENTITY(1,1) NOT NULL,
    [AcademicYear] nvarchar(max)  NOT NULL,
    [Term] nvarchar(max)  NOT NULL,
    [FeesPayable] float  NOT NULL,
    [FeesPaid] float  NOT NULL,
    [FeesUnpaid] float  NOT NULL,
    [NewStudentCount] int  NOT NULL,
    [IsInSession] bit  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Id] char(200)  NULL,
    [Username] nvarchar(200)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountNo] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AccountNo] ASC);
GO

-- Creating primary key on [ParentId] in table 'Parents'
ALTER TABLE [dbo].[Parents]
ADD CONSTRAINT [PK_Parents]
    PRIMARY KEY CLUSTERED ([ParentId] ASC);
GO

-- Creating primary key on [StudentClassBillId] in table 'StudentClassBills'
ALTER TABLE [dbo].[StudentClassBills]
ADD CONSTRAINT [PK_StudentClassBills]
    PRIMARY KEY CLUSTERED ([StudentClassBillId] ASC);
GO

-- Creating primary key on [StudentClassId] in table 'StudentClasses'
ALTER TABLE [dbo].[StudentClasses]
ADD CONSTRAINT [PK_StudentClasses]
    PRIMARY KEY CLUSTERED ([StudentClassId] ASC);
GO

-- Creating primary key on [AdmissionNo] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([AdmissionNo] ASC);
GO

-- Creating primary key on [TransactionId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [AcademicSessionId] in table 'AcademicSessions'
ALTER TABLE [dbo].[AcademicSessions]
ADD CONSTRAINT [PK_AcademicSessions]
    PRIMARY KEY CLUSTERED ([AcademicSessionId] ASC);
GO

-- Creating primary key on [Username] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountAccountNo] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_AccountTransaction]
    FOREIGN KEY ([AccountAccountNo])
    REFERENCES [dbo].[Accounts]
        ([AccountNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountTransaction'
CREATE INDEX [IX_FK_AccountTransaction]
ON [dbo].[Transactions]
    ([AccountAccountNo]);
GO

-- Creating foreign key on [ParentParentId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_ParentStudent]
    FOREIGN KEY ([ParentParentId])
    REFERENCES [dbo].[Parents]
        ([ParentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ParentStudent'
CREATE INDEX [IX_FK_ParentStudent]
ON [dbo].[Students]
    ([ParentParentId]);
GO

-- Creating foreign key on [Student_AdmissionNo] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_StudentAccount]
    FOREIGN KEY ([Student_AdmissionNo])
    REFERENCES [dbo].[Students]
        ([AdmissionNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentAccount'
CREATE INDEX [IX_FK_StudentAccount]
ON [dbo].[Accounts]
    ([Student_AdmissionNo]);
GO

-- Creating foreign key on [AcademicSessionAcademicSessionId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_AcademicSessionTransaction]
    FOREIGN KEY ([AcademicSessionAcademicSessionId])
    REFERENCES [dbo].[AcademicSessions]
        ([AcademicSessionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicSessionTransaction'
CREATE INDEX [IX_FK_AcademicSessionTransaction]
ON [dbo].[Transactions]
    ([AcademicSessionAcademicSessionId]);
GO

-- Creating foreign key on [StudentClassStudentClassId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StudentStudentClass]
    FOREIGN KEY ([StudentClassStudentClassId])
    REFERENCES [dbo].[StudentClasses]
        ([StudentClassId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentClass'
CREATE INDEX [IX_FK_StudentStudentClass]
ON [dbo].[Students]
    ([StudentClassStudentClassId]);
GO

-- Creating foreign key on [StudentClassBill_StudentClassBillId] in table 'StudentClasses'
ALTER TABLE [dbo].[StudentClasses]
ADD CONSTRAINT [FK_StudentClassStudentClassBill]
    FOREIGN KEY ([StudentClassBill_StudentClassBillId])
    REFERENCES [dbo].[StudentClassBills]
        ([StudentClassBillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentClassStudentClassBill'
CREATE INDEX [IX_FK_StudentClassStudentClassBill]
ON [dbo].[StudentClasses]
    ([StudentClassBill_StudentClassBillId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
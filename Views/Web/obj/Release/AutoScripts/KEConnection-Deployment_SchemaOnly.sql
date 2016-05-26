SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE Latin1_General_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE Latin1_General_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumberCountryCode] [nvarchar](3) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumber] [nvarchar](16) COLLATE Latin1_General_CI_AS NULL,
	[MobileNumberCountryCode] [nvarchar](3) COLLATE Latin1_General_CI_AS NULL,
	[MobileNumber] [nvarchar](16) COLLATE Latin1_General_CI_AS NULL,
	[AddressLine1] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[AddressLine2] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[City] [nvarchar](128) COLLATE Latin1_General_CI_AS NULL,
	[State] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[Country] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[ZipCode] [nvarchar](16) COLLATE Latin1_General_CI_AS NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlarmHistories](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[CalculatedValue] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[AckUserId] [uniqueidentifier] NOT NULL,
	[AckDate] [datetime] NOT NULL,
	[AlarmId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.AlarmHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alarms](
	[Id] [uniqueidentifier] NOT NULL,
	[TriggerId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[Value] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[SensorItemEventId] [uniqueidentifier] NOT NULL,
	[CalculatedValue] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[LastAckUserId] [uniqueidentifier] NULL,
	[LastAckDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Alarms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[RoleId] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) COLLATE Latin1_General_CI_AS NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[CountryId] [smallint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[IconFilename] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.CustomerSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.CustomerUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerUserSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE Latin1_General_CI_AS NOT NULL,
	[CustomerUserId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.CustomerUserSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Geometries](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[HasHeight] [bit] NOT NULL,
	[HasWidth] [bit] NOT NULL,
	[HasLength] [bit] NOT NULL,
	[HasFaceLength] [bit] NOT NULL,
	[HasBottomWidth] [bit] NOT NULL,
	[HasDimension1] [bit] NOT NULL,
	[HasDimension2] [bit] NOT NULL,
	[HasDimension3] [bit] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DimensionTitle1] [nvarchar](32) COLLATE Latin1_General_CI_AS NULL,
	[DimensionTitle2] [nvarchar](32) COLLATE Latin1_General_CI_AS NULL,
	[DimensionTitle3] [nvarchar](32) COLLATE Latin1_General_CI_AS NULL,
	[DeletedDate] [datetime] NULL,
	[HasRadius] [bit] NOT NULL,
	[HasDiagonal] [bit] NOT NULL,
	[FormulaVolume] [nvarchar](128) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_dbo.Geometries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[SiteId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) COLLATE Latin1_General_CI_AS NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[UnitTypeId] [smallint] NOT NULL,
	[SensorTypeId] [smallint] NOT NULL,
 CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](16) COLLATE Latin1_General_CI_AS NULL,
	[Message] [nvarchar](4000) COLLATE Latin1_General_CI_AS NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorGroups](
	[Id] [uniqueidentifier] NOT NULL,
	[Weight] [int] NOT NULL,
	[SensorId] [uniqueidentifier] NOT NULL,
	[GroupId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.SensorGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorItemEvents](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[SensorItemId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[CalculatedValue] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_dbo.SensorItemEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorItems](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[ItemId] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[SensorId] [uniqueidentifier] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[UnitId] [smallint] NOT NULL,
 CONSTRAINT [PK_dbo.SensorItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensors](
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[SensorTypeId] [smallint] NOT NULL,
	[TankId] [uniqueidentifier] NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Reference] [nvarchar](8) COLLATE Latin1_General_CI_AS NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[SpotGPS] [nvarchar](128) COLLATE Latin1_General_CI_AS NULL,
	[DeletedDate] [datetime] NULL,
	[SiteId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Sensors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorTypes](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.SensorTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Severities](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Severities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[IPAddress] [nvarchar](64) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[Latitude] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[Longitude] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Sites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StickConversions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[FromUnitId] [smallint] NOT NULL,
	[ToUnitId] [smallint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.StickConversions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StickConversionValues](
	[Id] [uniqueidentifier] NOT NULL,
	[ToValue] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[FromValue] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[StickConversionId] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.StickConversionValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TankModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[ImageFileName] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[Image] [varbinary](max) NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[Height] [decimal](18, 2) NULL,
	[Width] [decimal](18, 2) NULL,
	[Length] [decimal](18, 2) NULL,
	[FaceLength] [decimal](18, 2) NULL,
	[BottomWidth] [decimal](18, 2) NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[GeometryId] [smallint] NOT NULL,
	[WaterVolumeCapacity] [float] NULL,
	[DimensionValue1] [decimal](18, 2) NULL,
	[DimensionValue2] [decimal](18, 2) NULL,
	[DimensionValue3] [decimal](18, 2) NULL,
	[DeletedDate] [datetime] NULL,
	[Radius] [decimal](18, 2) NULL,
	[Diagonal] [decimal](18, 2) NULL,
 CONSTRAINT [PK_dbo.TankModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tanks](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Description] [nvarchar](4000) COLLATE Latin1_General_CI_AS NULL,
	[WaterVolumeCapacity] [decimal](18, 2) NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[Latitude] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[Longitude] [nvarchar](64) COLLATE Latin1_General_CI_AS NULL,
	[SiteId] [uniqueidentifier] NOT NULL,
	[TankModelId] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Height] [decimal](18, 2) NULL,
	[Width] [decimal](18, 2) NULL,
	[Length] [decimal](18, 2) NULL,
	[MinimumDistance] [int] NULL,
	[MaximumDistance] [int] NULL,
	[Reference] [nvarchar](8) COLLATE Latin1_General_CI_AS NOT NULL,
	[Dimension1] [decimal](18, 2) NULL,
	[Dimension2] [decimal](18, 2) NULL,
	[Dimension3] [decimal](18, 2) NULL,
	[DeletedDate] [datetime] NULL,
	[FaceLength] [decimal](18, 2) NULL,
	[BottomWidth] [decimal](18, 2) NULL,
	[Radius] [decimal](18, 2) NULL,
	[Diagonal] [decimal](18, 2) NULL,
	[StickConversionId] [int] NULL,
 CONSTRAINT [PK_dbo.Tanks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TriggerContacts](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[TriggerId] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.TriggerContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Triggers](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[SensorItemId] [uniqueidentifier] NOT NULL,
	[SeverityId] [smallint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[MinValue] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
	[MaxValue] [nvarchar](256) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_dbo.Triggers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[NamePlural] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
	[Symbol] [nvarchar](8) COLLATE Latin1_General_CI_AS NOT NULL,
	[UnitTypeId] [smallint] NOT NULL,
 CONSTRAINT [PK_dbo.Units] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitTypes](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) COLLATE Latin1_General_CI_AS NOT NULL,
	[Status] [char](1) COLLATE Latin1_General_CI_AS NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.UnitTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
CREATE NONCLUSTERED INDEX [IX_TriggerId] ON [dbo].[Alarms]
(
	[TriggerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CountryId] ON [dbo].[Cities]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_AddressId] ON [dbo].[Contacts]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CustomerId] ON [dbo].[Contacts]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_AddressId] ON [dbo].[Customers]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CustomerId] ON [dbo].[CustomerSettings]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_AddressId] ON [dbo].[CustomerUsers]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CustomerId] ON [dbo].[CustomerUsers]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CustomerUserId] ON [dbo].[CustomerUserSettings]
(
	[CustomerUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SiteId] ON [dbo].[Groups]
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorTypeId] ON [dbo].[Items]
(
	[SensorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_UnitTypeId] ON [dbo].[Items]
(
	[UnitTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_GroupId] ON [dbo].[SensorGroups]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorId] ON [dbo].[SensorGroups]
(
	[SensorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorItemId] ON [dbo].[SensorItemEvents]
(
	[SensorItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_ItemId] ON [dbo].[SensorItems]
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorId] ON [dbo].[SensorItems]
(
	[SensorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_UnitId] ON [dbo].[SensorItems]
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorTypeId] ON [dbo].[Sensors]
(
	[SensorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SiteId] ON [dbo].[Sensors]
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_TankId] ON [dbo].[Sensors]
(
	[TankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_AddressId] ON [dbo].[Sites]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_CustomerId] ON [dbo].[Sites]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FromUnitId] ON [dbo].[StickConversions]
(
	[FromUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_ToUnitId] ON [dbo].[StickConversions]
(
	[ToUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_StickConversionId] ON [dbo].[StickConversionValues]
(
	[StickConversionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_GeometryId] ON [dbo].[TankModels]
(
	[GeometryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SiteId] ON [dbo].[Tanks]
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_StickConversionId] ON [dbo].[Tanks]
(
	[StickConversionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_TankModelId] ON [dbo].[Tanks]
(
	[TankModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_TriggerId] ON [dbo].[TriggerContacts]
(
	[TriggerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SensorItemId] ON [dbo].[Triggers]
(
	[SensorItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_SeverityId] ON [dbo].[Triggers]
(
	[SeverityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_UnitTypeId] ON [dbo].[Units]
(
	[UnitTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_AddressId] ON [dbo].[Users]
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[Addresses] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[AlarmHistories] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Alarms] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Alarms] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [StartDate]
GO
ALTER TABLE [dbo].[Alarms] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [SensorItemEventId]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Cities] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Contacts] ADD  CONSTRAINT [DF_Contacts_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [AddressId]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [CustomerId]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [AddressId]
GO
ALTER TABLE [dbo].[CustomerUsers] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [AddressId]
GO
ALTER TABLE [dbo].[Geometries] ADD  DEFAULT ((0)) FOR [HasRadius]
GO
ALTER TABLE [dbo].[Geometries] ADD  DEFAULT ((0)) FOR [HasDiagonal]
GO
ALTER TABLE [dbo].[Groups] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Groups] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [SiteId]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((1)) FOR [UnitTypeId]
GO
ALTER TABLE [dbo].[Items] ADD  DEFAULT ((1)) FOR [SensorTypeId]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [CustomerId]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [UserId]
GO
ALTER TABLE [dbo].[SensorGroups] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[SensorItemEvents] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[SensorItemEvents] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [EventDate]
GO
ALTER TABLE [dbo].[SensorItems] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[SensorItems] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [SensorId]
GO
ALTER TABLE [dbo].[SensorItems] ADD  DEFAULT ((0)) FOR [UnitId]
GO
ALTER TABLE [dbo].[Sensors] ADD  DEFAULT ('') FOR [Reference]
GO
ALTER TABLE [dbo].[Sensors] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Sites] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Sites] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [AddressId]
GO
ALTER TABLE [dbo].[TankModels] ADD  CONSTRAINT [DF__TankModel__Geome__72910220]  DEFAULT ((0)) FOR [GeometryId]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ((0)) FOR [Height]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ((0)) FOR [Width]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ((0)) FOR [Length]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ((0)) FOR [MinimumDistance]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ((0)) FOR [MaximumDistance]
GO
ALTER TABLE [dbo].[Tanks] ADD  DEFAULT ('') FOR [Reference]
GO
ALTER TABLE [dbo].[TriggerContacts] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Triggers] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF__Units__Symbol__10E07F16]  DEFAULT ('') FOR [Symbol]
GO
ALTER TABLE [dbo].[Units] ADD  CONSTRAINT [DF__Units__UnitTypeI__11D4A34F]  DEFAULT ((0)) FOR [UnitTypeId]
GO
ALTER TABLE [dbo].[Alarms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Alarms_dbo.SensorItemAlarms_SensorItemAlarmId] FOREIGN KEY([TriggerId])
REFERENCES [dbo].[Triggers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Alarms] CHECK CONSTRAINT [FK_dbo.Alarms_dbo.SensorItemAlarms_SensorItemAlarmId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cities_dbo.Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_dbo.Cities_dbo.Countries_CountryId]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contacts_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_dbo.Contacts_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contacts_dbo.Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_dbo.Contacts_dbo.Customers_CustomerId]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Customers_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_dbo.Customers_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[CustomerSettings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSettings_dbo.Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerSettings] CHECK CONSTRAINT [FK_dbo.CustomerSettings_dbo.Customers_CustomerId]
GO
ALTER TABLE [dbo].[CustomerUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerUsers_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[CustomerUsers] CHECK CONSTRAINT [FK_dbo.CustomerUsers_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[CustomerUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerUsers_dbo.Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerUsers] CHECK CONSTRAINT [FK_dbo.CustomerUsers_dbo.Customers_CustomerId]
GO
ALTER TABLE [dbo].[CustomerUserSettings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerUserSettings_dbo.CustomerUsers_CustomerUserId] FOREIGN KEY([CustomerUserId])
REFERENCES [dbo].[CustomerUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerUserSettings] CHECK CONSTRAINT [FK_dbo.CustomerUserSettings_dbo.CustomerUsers_CustomerUserId]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Groups_dbo.Sites_SiteId] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_dbo.Groups_dbo.Sites_SiteId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Items_dbo.SensorTypes_SensorTypeId] FOREIGN KEY([SensorTypeId])
REFERENCES [dbo].[SensorTypes] ([Id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_dbo.Items_dbo.SensorTypes_SensorTypeId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Items_dbo.UnitTypes_UnitTypeId] FOREIGN KEY([UnitTypeId])
REFERENCES [dbo].[UnitTypes] ([Id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_dbo.Items_dbo.UnitTypes_UnitTypeId]
GO
ALTER TABLE [dbo].[SensorGroups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorGroups_dbo.Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorGroups] CHECK CONSTRAINT [FK_dbo.SensorGroups_dbo.Groups_GroupId]
GO
ALTER TABLE [dbo].[SensorGroups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorGroups_dbo.Sensors_SensorId] FOREIGN KEY([SensorId])
REFERENCES [dbo].[Sensors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorGroups] CHECK CONSTRAINT [FK_dbo.SensorGroups_dbo.Sensors_SensorId]
GO
ALTER TABLE [dbo].[SensorItemEvents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItemEvents_dbo.SensorItems_SensorItemId] FOREIGN KEY([SensorItemId])
REFERENCES [dbo].[SensorItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorItemEvents] CHECK CONSTRAINT [FK_dbo.SensorItemEvents_dbo.SensorItems_SensorItemId]
GO
ALTER TABLE [dbo].[SensorItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItems_dbo.Item_ItemId] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorItems] CHECK CONSTRAINT [FK_dbo.SensorItems_dbo.Item_ItemId]
GO
ALTER TABLE [dbo].[SensorItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItems_dbo.Sensors_SensorId] FOREIGN KEY([SensorId])
REFERENCES [dbo].[Sensors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorItems] CHECK CONSTRAINT [FK_dbo.SensorItems_dbo.Sensors_SensorId]
GO
ALTER TABLE [dbo].[SensorItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItems_dbo.Units_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SensorItems] CHECK CONSTRAINT [FK_dbo.SensorItems_dbo.Units_UnitId]
GO
ALTER TABLE [dbo].[Sensors]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sensors_dbo.SensorTypes_SensorTypeId] FOREIGN KEY([SensorTypeId])
REFERENCES [dbo].[SensorTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sensors] CHECK CONSTRAINT [FK_dbo.Sensors_dbo.SensorTypes_SensorTypeId]
GO
ALTER TABLE [dbo].[Sensors]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sensors_dbo.Sites_SiteId] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
GO
ALTER TABLE [dbo].[Sensors] CHECK CONSTRAINT [FK_dbo.Sensors_dbo.Sites_SiteId]
GO
ALTER TABLE [dbo].[Sensors]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sensors_dbo.Tanks_TankId] FOREIGN KEY([TankId])
REFERENCES [dbo].[Tanks] ([Id])
GO
ALTER TABLE [dbo].[Sensors] CHECK CONSTRAINT [FK_dbo.Sensors_dbo.Tanks_TankId]
GO
ALTER TABLE [dbo].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sites_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sites] CHECK CONSTRAINT [FK_dbo.Sites_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sites_dbo.Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sites] CHECK CONSTRAINT [FK_dbo.Sites_dbo.Customers_CustomerId]
GO
ALTER TABLE [dbo].[StickConversions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StickConversions_dbo.Units_FromUnitId] FOREIGN KEY([FromUnitId])
REFERENCES [dbo].[Units] ([Id])
GO
ALTER TABLE [dbo].[StickConversions] CHECK CONSTRAINT [FK_dbo.StickConversions_dbo.Units_FromUnitId]
GO
ALTER TABLE [dbo].[StickConversions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StickConversions_dbo.Units_ToUnitId] FOREIGN KEY([ToUnitId])
REFERENCES [dbo].[Units] ([Id])
GO
ALTER TABLE [dbo].[StickConversions] CHECK CONSTRAINT [FK_dbo.StickConversions_dbo.Units_ToUnitId]
GO
ALTER TABLE [dbo].[StickConversionValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StickConversionValues_dbo.StickConversions_StickConversionId] FOREIGN KEY([StickConversionId])
REFERENCES [dbo].[StickConversions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StickConversionValues] CHECK CONSTRAINT [FK_dbo.StickConversionValues_dbo.StickConversions_StickConversionId]
GO
ALTER TABLE [dbo].[TankModels]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TankModels_dbo.Geometries_GeometryId] FOREIGN KEY([GeometryId])
REFERENCES [dbo].[Geometries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TankModels] CHECK CONSTRAINT [FK_dbo.TankModels_dbo.Geometries_GeometryId]
GO
ALTER TABLE [dbo].[Tanks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tanks_dbo.Sites_SiteId] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tanks] CHECK CONSTRAINT [FK_dbo.Tanks_dbo.Sites_SiteId]
GO
ALTER TABLE [dbo].[Tanks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tanks_dbo.StickConversions_StickConversionId] FOREIGN KEY([StickConversionId])
REFERENCES [dbo].[StickConversions] ([Id])
GO
ALTER TABLE [dbo].[Tanks] CHECK CONSTRAINT [FK_dbo.Tanks_dbo.StickConversions_StickConversionId]
GO
ALTER TABLE [dbo].[Tanks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tanks_dbo.TankModels_TankModelId] FOREIGN KEY([TankModelId])
REFERENCES [dbo].[TankModels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tanks] CHECK CONSTRAINT [FK_dbo.Tanks_dbo.TankModels_TankModelId]
GO
ALTER TABLE [dbo].[TriggerContacts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TriggerContacts_dbo.Triggers_TriggerId] FOREIGN KEY([TriggerId])
REFERENCES [dbo].[Triggers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TriggerContacts] CHECK CONSTRAINT [FK_dbo.TriggerContacts_dbo.Triggers_TriggerId]
GO
ALTER TABLE [dbo].[Triggers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItemAlarms_dbo.SensorItems_SensorItemId] FOREIGN KEY([SensorItemId])
REFERENCES [dbo].[SensorItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Triggers] CHECK CONSTRAINT [FK_dbo.SensorItemAlarms_dbo.SensorItems_SensorItemId]
GO
ALTER TABLE [dbo].[Triggers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SensorItemAlarms_dbo.Severities_SeverityId] FOREIGN KEY([SeverityId])
REFERENCES [dbo].[Severities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Triggers] CHECK CONSTRAINT [FK_dbo.SensorItemAlarms_dbo.Severities_SeverityId]
GO
ALTER TABLE [dbo].[Units]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Units_dbo.UnitTypes_UnitTypeId] FOREIGN KEY([UnitTypeId])
REFERENCES [dbo].[UnitTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Units] CHECK CONSTRAINT [FK_dbo.Units_dbo.UnitTypes_UnitTypeId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Addresses_AddressId]
GO

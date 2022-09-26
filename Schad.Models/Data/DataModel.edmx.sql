USE [master]
GO
/****** Object:  Database [SchadTest]    Script Date: 26/09/2022 19:21:37 ******/
CREATE DATABASE [SchadTest]
GO
ALTER DATABASE [SchadTest] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchadTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchadTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchadTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchadTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchadTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchadTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchadTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchadTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchadTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchadTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchadTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchadTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchadTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchadTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchadTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchadTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchadTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchadTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchadTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchadTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchadTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchadTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchadTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchadTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchadTest] SET  MULTI_USER 
GO
ALTER DATABASE [SchadTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchadTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchadTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchadTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchadTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchadTest] SET QUERY_STORE = OFF
GO
USE [SchadTest]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [SchadTest]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 26/09/2022 19:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustName] [varchar](150) NOT NULL,
	[Address] [varchar](150) NULL,
	[Status] [bit] NOT NULL,
	[CustomerTypeId] [int] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerTypes]    Script Date: 26/09/2022 19:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CustomerTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetails]    Script Date: 26/09/2022 19:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[Qty] [int] NOT NULL,
	[Price] [decimal](8, 3) NOT NULL,
	[ProductId] [int] NOT NULL,
	[TotalItebis] [decimal](18, 0) NULL,
	[Subtotal] [decimal](18, 0) NULL,
	[Total] [decimal](18, 0) NULL,
	[DeteCreated] [datetime] NULL,
 CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 26/09/2022 19:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [bigint] NOT NULL,
	[CustomerId] [int] NULL,
	[Subtotal] [decimal](18, 0) NOT NULL,
	[TotalItbis] [decimal](18, 0) NOT NULL,
	[Total] [decimal](18, 0) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 26/09/2022 19:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[In_Stock] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [CustName], [Address], [Status], [CustomerTypeId]) VALUES (1, N'RUBEN LUCIANO', N'CCCC', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerTypes] ON 
GO
INSERT [dbo].[CustomerTypes] ([Id], [Description]) VALUES (1, N'Comercial')
GO
INSERT [dbo].[CustomerTypes] ([Id], [Description]) VALUES (2, N'Persona')
GO
INSERT [dbo].[CustomerTypes] ([Id], [Description]) VALUES (3, N'Tipo Dos')
GO
SET IDENTITY_INSERT [dbo].[CustomerTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[InvoiceDetails] ON 
GO
INSERT [dbo].[InvoiceDetails] ([Id], [InvoiceId], [Qty], [Price], [ProductId], [TotalItebis], [Subtotal], [Total], [DeteCreated]) VALUES (4, 100000, 1, CAST(200.000 AS Decimal(8, 3)), 1, CAST(36 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(236 AS Decimal(18, 0)), NULL)
GO
INSERT [dbo].[InvoiceDetails] ([Id], [InvoiceId], [Qty], [Price], [ProductId], [TotalItebis], [Subtotal], [Total], [DeteCreated]) VALUES (5, 100000, 1, CAST(47.000 AS Decimal(8, 3)), 3, CAST(8 AS Decimal(18, 0)), CAST(47 AS Decimal(18, 0)), CAST(55 AS Decimal(18, 0)), NULL)
GO
INSERT [dbo].[InvoiceDetails] ([Id], [InvoiceId], [Qty], [Price], [ProductId], [TotalItebis], [Subtotal], [Total], [DeteCreated]) VALUES (6, 100001, 1, CAST(200.000 AS Decimal(8, 3)), 1, CAST(36 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(236 AS Decimal(18, 0)), NULL)
GO
SET IDENTITY_INSERT [dbo].[InvoiceDetails] OFF
GO
INSERT [dbo].[Invoices] ([Id], [CustomerId], [Subtotal], [TotalItbis], [Total], [DateCreated]) VALUES (100000, NULL, CAST(247 AS Decimal(18, 0)), CAST(44 AS Decimal(18, 0)), CAST(291 AS Decimal(18, 0)), CAST(N'2022-09-26T17:07:15.297' AS DateTime))
GO
INSERT [dbo].[Invoices] ([Id], [CustomerId], [Subtotal], [TotalItbis], [Total], [DateCreated]) VALUES (100001, 1, CAST(200 AS Decimal(18, 0)), CAST(36 AS Decimal(18, 0)), CAST(236 AS Decimal(18, 0)), CAST(N'2022-09-26T18:14:39.447' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Description], [Price], [In_Stock]) VALUES (1, N'PAPA', CAST(200 AS Decimal(18, 0)), 1000)
GO
INSERT [dbo].[Products] ([Id], [Description], [Price], [In_Stock]) VALUES (2, N'MAIZ', CAST(100 AS Decimal(18, 0)), 1000)
GO
INSERT [dbo].[Products] ([Id], [Description], [Price], [In_Stock]) VALUES (3, N'YUCA', CAST(47 AS Decimal(18, 0)), 1000)
GO
INSERT [dbo].[Products] ([Id], [Description], [Price], [In_Stock]) VALUES (4, N'LIMON', CAST(100 AS Decimal(18, 0)), 1000)
GO
INSERT [dbo].[Products] ([Id], [Description], [Price], [In_Stock]) VALUES (5, N'AHUYAMA', CAST(60 AS Decimal(18, 0)), 1000)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
/****** Object:  Index [IX_FK_Customers_CustomerTypes]    Script Date: 26/09/2022 19:21:37 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Customers_CustomerTypes] ON [dbo].[Customers]
(
	[CustomerTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_InvoiceDetails_Products]    Script Date: 26/09/2022 19:21:37 ******/
CREATE NONCLUSTERED INDEX [IX_FK_InvoiceDetails_Products] ON [dbo].[InvoiceDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_InvoiceInvoiceDetails]    Script Date: 26/09/2022 19:21:37 ******/
CREATE NONCLUSTERED INDEX [IX_FK_InvoiceInvoiceDetails] ON [dbo].[InvoiceDetails]
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Invoice_Customers]    Script Date: 26/09/2022 19:21:37 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Invoice_Customers] ON [dbo].[Invoices]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_CustomerTypes] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerTypes] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_CustomerTypes]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceDetails_Products]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceInvoiceDetails] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceInvoiceDetails]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoice_Customers]
GO
USE [master]
GO
ALTER DATABASE [SchadTest] SET  READ_WRITE 
GO


-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2022 14:35:08
-- Generated from EDMX file: C:\Users\ruben\source\repos\Schad\Schad.Models\Data\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SchadTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Customers_CustomerTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_Customers_CustomerTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Invoice_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceDetails] DROP CONSTRAINT [FK_InvoiceDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceInvoiceDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceDetails] DROP CONSTRAINT [FK_InvoiceInvoiceDetails];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[CustomerTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerTypes];
GO
IF OBJECT_ID(N'[dbo].[Invoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoice];
GO
IF OBJECT_ID(N'[dbo].[InvoiceDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceDetails];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustName] varchar(150)  NOT NULL,
    [Address] varchar(150)  NULL,
    [Status] bit  NOT NULL,
    [CustomerTypeId] int  NULL
);
GO

-- Creating table 'CustomerTypes'
CREATE TABLE [dbo].[CustomerTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'Invoice'
CREATE TABLE [dbo].[Invoice] (
    [Id] int  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Subtotal] decimal(18,0)  NOT NULL,
    [TotalItbis] decimal(18,0)  NOT NULL,
    [Total] decimal(18,0)  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'InvoiceDetails'
CREATE TABLE [dbo].[InvoiceDetails] (
    [Id] int  NOT NULL,
    [InvoiceId] int  NOT NULL,
    [Qty] int  NOT NULL,
    [Price] decimal(8,3)  NOT NULL,
    [ProductId] int  NOT NULL,
    [TotalItebis] decimal(18,0)  NULL,
    [Subtotal] decimal(18,0)  NULL,
    [Total] decimal(18,0)  NULL,
    [DeteCreated] datetime  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NULL,
    [Price] decimal(18,0)  NULL,
    [In_Stock] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerTypes'
ALTER TABLE [dbo].[CustomerTypes]
ADD CONSTRAINT [PK_CustomerTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invoice'
ALTER TABLE [dbo].[Invoice]
ADD CONSTRAINT [PK_Invoice]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceDetails'
ALTER TABLE [dbo].[InvoiceDetails]
ADD CONSTRAINT [PK_InvoiceDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerTypeId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customers_CustomerTypes]
    FOREIGN KEY ([CustomerTypeId])
    REFERENCES [dbo].[CustomerTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Customers_CustomerTypes'
CREATE INDEX [IX_FK_Customers_CustomerTypes]
ON [dbo].[Customers]
    ([CustomerTypeId]);
GO

-- Creating foreign key on [CustomerId] in table 'Invoice'
ALTER TABLE [dbo].[Invoice]
ADD CONSTRAINT [FK_Invoice_Customers]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Invoice_Customers'
CREATE INDEX [IX_FK_Invoice_Customers]
ON [dbo].[Invoice]
    ([CustomerId]);
GO

-- Creating foreign key on [InvoiceId] in table 'InvoiceDetails'
ALTER TABLE [dbo].[InvoiceDetails]
ADD CONSTRAINT [FK_InvoiceInvoiceDetails]
    FOREIGN KEY ([InvoiceId])
    REFERENCES [dbo].[Invoice]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceInvoiceDetails'
CREATE INDEX [IX_FK_InvoiceInvoiceDetails]
ON [dbo].[InvoiceDetails]
    ([InvoiceId]);
GO

-- Creating foreign key on [ProductId] in table 'InvoiceDetails'
ALTER TABLE [dbo].[InvoiceDetails]
ADD CONSTRAINT [FK_InvoiceDetails_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceDetails_Products'
CREATE INDEX [IX_FK_InvoiceDetails_Products]
ON [dbo].[InvoiceDetails]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
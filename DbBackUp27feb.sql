USE [Association]
GO
ALTER TABLE [dbo].[Page] DROP CONSTRAINT [FK_Page_CustomMenu]
GO
ALTER TABLE [dbo].[Container] DROP CONSTRAINT [FK_Container_Component]
GO
ALTER TABLE [dbo].[ComponentProperty] DROP CONSTRAINT [FK_ComponentProperty_Component]
GO
ALTER TABLE [dbo].[BlogImages] DROP CONSTRAINT [FK_BlogImages_Tenants]
GO
ALTER TABLE [dbo].[BlogImages] DROP CONSTRAINT [FK_BlogImages_Blog]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type in (N'U'))
DROP TABLE [dbo].[Tenants]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Page]') AND type in (N'U'))
DROP TABLE [dbo].[Page]
GO
/****** Object:  Table [dbo].[CustomMenu]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomMenu]') AND type in (N'U'))
DROP TABLE [dbo].[CustomMenu]
GO
/****** Object:  Table [dbo].[Container]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Container]') AND type in (N'U'))
DROP TABLE [dbo].[Container]
GO
/****** Object:  Table [dbo].[ComponentProperty]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComponentProperty]') AND type in (N'U'))
DROP TABLE [dbo].[ComponentProperty]
GO
/****** Object:  Table [dbo].[Component]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Component]') AND type in (N'U'))
DROP TABLE [dbo].[Component]
GO
/****** Object:  Table [dbo].[BlogImages]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlogImages]') AND type in (N'U'))
DROP TABLE [dbo].[BlogImages]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 2/27/2024 9:14:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blog]') AND type in (N'U'))
DROP TABLE [dbo].[Blog]
GO
USE [master]
GO
/****** Object:  Database [Association]    Script Date: 2/27/2024 9:14:55 PM ******/
DROP DATABASE [Association]
GO
/****** Object:  Database [Association]    Script Date: 2/27/2024 9:14:55 PM ******/
CREATE DATABASE [Association]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Association', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Association.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Association_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Association_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Association] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Association].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Association] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Association] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Association] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Association] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Association] SET ARITHABORT OFF 
GO
ALTER DATABASE [Association] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Association] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Association] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Association] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Association] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Association] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Association] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Association] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Association] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Association] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Association] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Association] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Association] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Association] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Association] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Association] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Association] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Association] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Association] SET  MULTI_USER 
GO
ALTER DATABASE [Association] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Association] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Association] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Association] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Association] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Association] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Association] SET QUERY_STORE = OFF
GO
USE [Association]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[BlogName] [varchar](500) NULL,
	[HeaderBackgroundColor] [varchar](50) NULL,
	[HeaderFontColor] [varchar](50) NULL,
	[BrandName] [varchar](500) NULL,
	[Logo] [varchar](max) NULL,
	[Banner] [varchar](max) NULL,
	[Heading] [varchar](500) NULL,
	[TextContent] [varchar](max) NULL,
	[HeaderFontStyle] [varchar](50) NULL,
	[ContentFontStyle] [varchar](50) NULL,
	[FooterFontStyle] [varchar](50) NULL,
	[ContentFontSize] [varchar](50) NULL,
	[ContentFontColor] [varchar](50) NULL,
	[MainMenuColor] [varchar](50) NULL,
	[MainMenuFontSize] [varchar](50) NULL,
	[MainMenuFontStyle] [varchar](50) NULL,
	[SubMenuColor] [varchar](50) NULL,
	[SubMenuFontSize] [varchar](50) NULL,
	[SubMenuFontStyle] [varchar](50) NULL,
	[SubChildMenuColor] [varchar](50) NULL,
	[SubChildMenuFontSize] [varchar](50) NULL,
	[SubChildFontStyle] [varchar](50) NULL,
	[FooterBackgroundColor] [varchar](50) NULL,
	[FooterText] [varchar](500) NULL,
	[FooterFontColor] [varchar](50) NULL,
	[Facebook] [varchar](max) NULL,
	[LinkedIn] [varchar](max) NULL,
	[Twitter] [varchar](max) NULL,
	[Instagram] [varchar](max) NULL,
	[WhatsApp] [varchar](max) NULL,
	[PublishedDateTime] [datetime] NULL,
	[UpdatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogImages]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogImages](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[BlogId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[ImageUrl] [varchar](max) NULL,
	[Text] [varchar](max) NULL,
	[Position] [int] NOT NULL,
	[UDF] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_BlogImages] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Component]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[ComponentType] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Component] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponentProperty]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentProperty](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[ComponentId] [int] NOT NULL,
	[PropertyKey] [varchar](500) NULL,
	[Value] [varchar](max) NULL,
 CONSTRAINT [PK_ComponentProperty] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Container]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Container](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowNumber] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Name] [varchar](150) NULL,
	[ComponentId] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Container] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomMenu]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomMenu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[MenuName] [varchar](50) NOT NULL,
	[ParentMenuId] [int] NULL,
	[PageUrl] [varchar](500) NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_CustomMenu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[PageTitle] [varchar](150) NOT NULL,
	[IsHomePage] [bit] NULL,
	[ResourcePath] [varchar](500) NULL,
	[MenuId] [int] NOT NULL,
	[PaddingWidth] [int] NULL,
	[PublishStartDate] [datetime] NULL,
	[PublishEndDate] [datetime] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 2/27/2024 9:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[TenantId] [int] NOT NULL,
	[LastName] [varchar](255) NULL,
	[FirstName] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[PhoneNumber] [varchar](255) NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (1, 1, N'sample', N'#983434', N'#FEDA3B', N'CSK', N'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAH8AAAB/CAMAAADxY+0hAAABAlBMVEX///8AAAAAl9Xg4ODo6Ojt6uj39PEAkdOjzerPQETo7OwAldQAk9SfzOnY6vbo6+nx9fX1zQDm6OzptM/20zp+uuLpra714Or20Szw8ffNLzT89fiWyenOOT0Ai9Hy+PzNRZP656LRWJv55JXKGyL878T31UnfjI7++/HlpKX99dvPUJfD3vEAgs7++en88s/32Fz139/y1NXRTlHYeazZdXfVbaXvzt754YfW1NP43HD767MAf7Pgl72Hh4fBwcFCQkLsu7zprrmww+IZGRlRUVLIAAfcibXM1+pBoNaqq652dnbWZ2kvLy9jY2RksN0AYosAO1PJhrYAU3sAbZ+6pczHWLkMAAAFWUlEQVRoge2Wa3faRhCGR4ZGCAkVRcIIyVwkBIiLuRljDCYurWtK4gS3cf//X+k7AhGD8iE9p/WnfY61WgmW2Zl3ZtZEAoFAIBAIBAKBQCAQCAQCgUAgEAgEgv+LbPrtyCfNp6U35DZp//Yt7aeSEfggSffv36fT3y6MfKWz2Sxfx/yc5Kd/QSoRgV8kiRYDosWCyPM6NPC8AXl4Gp2fr+j8hF/NJL/lIjKZTC6e5Q6zI3L6u9SJ+dSD9DsZsG/MqWMYHRraRmNg2E1qBUFrHDjO42g0agfO1Wg0LjtFZt31a5NoVlzWut0/dKZ3KcubaBbf8a4iH3Gpp07jfy9JHztWSA1jQZ49DMmwrH7dtj1SFGfcdpTyE761cpw20ThoR2vWvm/G65eTeHahyb1oUpEz8bucdvYaje5Pw/8JSbkYEtWNPs3tGQ1sa0hNyx64gXLlPpUVNgzLzpio7YyiNZPu+rDeLMazqrqp7u3r8buM/Nq8uk2EnyTpM83rRM1hGNp2g+B6PTSsZrhy4HmgKGUYplFw5RJdXe/WTLtw35xOpjfwn6i02W5gsVe52H1cwT50vMN0qx65n0mlT+WXpC8E1UNjxvKHNLeMTsO25tQqO+djB/b5a24A0+7jeLfI78Lpie/X8M5kw6oMzwtyaReHypYd12C/ujkKv5yU/yOqr2MQ5GfXhxTacH1m2Z57pTijVlkpRz67j1BhpSAG+KPaFGPN9/1ut3bDTqqwSBoP2Ef1EvIXNBnR0OVj+9+VP1WfE83YdbtO7Ho4RAwO8q+i7z1C+RZnAideDUEvTieTKbbgwknY7/V6l6w6TOuX2MTmTNVP5dcKlKh+CdU3XFA4bFLfgvwzZH4fMaBzp3ztcvhhGFsI8N0yvC7CtFvjTbhg6iP7dWisorhY70yPnzEpVTkXN0fyy73b0/BD/k+EzO8bdWQ+dGDXF7Y1o+uys2L5Oe9aRCw/F+LSZPsc/0gWTkR2UlVVLccGOQ8L8a9vjqtP1RPuc/MdwOzCYNeb1LEtGyloLyB/4O7lH19HIUA3hGnoXaz50/36m0j+M3Wz2VRYfQ595lD9F0f21U0y/F8kqTSbEaRHBaLnsOth0zJCuA75lUj+86co7RAFKk6Q+Tdd35+u1zw18V5GXSHqnH0ZHi64Frbb0g/In35A9bH8zSF1WH52HR3IQqspt0Zl2If8109RrFfxqhv0XGSeX4yqv6edaVD9ohd/nOES1M6qe/nVu2/yn7qPs/dD3+7TwIhcD8my7L5now0+Qf4VN184GPBAo1G8ylyaJmpvvav+nKaq7PHhRzkF5KgiK7B693z3HGWheqYnqg/y3zYQeg8H0BzVP2D50XwbrqIESEGljKCPHM5BGh9WcQksl8ubXfNF9SPz9czr3y2oco53AbNf717unnfNN//ds3cO+YfNkPjIiVzHHlh+BQeQsuv6V4d0r9U4/of1phvPevBar1Q20QM6QqW6T7/nlz939rXCu1P7KZYfqof2HK7bncj1Bld/C+fdGPIHMIAw7EOPxOPT7nD2uZPYvs6tP7eLOncA9Yz2zf/5Tn2J7FeS1Xf/8PljB/I3IL9nIAaGbfPZWyfFwdkblOG56+JfgDE3GxfnXneCvrM2o0fX7Nb+0qtM4ZLvqiYXosdMRd7iVtGA/Pz1RYuoJuTPZ7O3DQ/Nf9Ynb+ZRZzarU30267itVsttY2i12+3dCEyIjtQzoyG6L/8uXIBCIb5F99fP38jlE2cvCiBdKmWzpVIeVymb5wFX/sd59+Pcnp69IJt6O75jXiAQCAQCgUAgEAgEAoFAIBAIBAKBQPAf8Q8e/L8DY6yYtwAAAABJRU5ErkJggg==', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#fe3939', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-02T00:54:41.347' AS DateTime), CAST(N'2023-11-04T15:08:57.867' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (2, 1, N'sample', N'#987834', N'#39d7fe', N'MG', N'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAH8AAAB/CAMAAADxY+0hAAABAlBMVEX///8AAAAAl9Xg4ODo6Ojt6uj39PEAkdOjzerPQETo7OwAldQAk9SfzOnY6vbo6+nx9fX1zQDm6OzptM/20zp+uuLpra714Or20Szw8ffNLzT89fiWyenOOT0Ai9Hy+PzNRZP656LRWJv55JXKGyL878T31UnfjI7++/HlpKX99dvPUJfD3vEAgs7++en88s/32Fz139/y1NXRTlHYeazZdXfVbaXvzt754YfW1NP43HD767MAf7Pgl72Hh4fBwcFCQkLsu7zprrmww+IZGRlRUVLIAAfcibXM1+pBoNaqq652dnbWZ2kvLy9jY2RksN0AYosAO1PJhrYAU3sAbZ+6pczHWLkMAAAFWUlEQVRoge2Wa3faRhCGR4ZGCAkVRcIIyVwkBIiLuRljDCYurWtK4gS3cf//X+k7AhGD8iE9p/WnfY61WgmW2Zl3ZtZEAoFAIBAIBAKBQCAQCAQCgUAgEAgEgv+LbPrtyCfNp6U35DZp//Yt7aeSEfggSffv36fT3y6MfKWz2Sxfx/yc5Kd/QSoRgV8kiRYDosWCyPM6NPC8AXl4Gp2fr+j8hF/NJL/lIjKZTC6e5Q6zI3L6u9SJ+dSD9DsZsG/MqWMYHRraRmNg2E1qBUFrHDjO42g0agfO1Wg0LjtFZt31a5NoVlzWut0/dKZ3KcubaBbf8a4iH3Gpp07jfy9JHztWSA1jQZ49DMmwrH7dtj1SFGfcdpTyE761cpw20ThoR2vWvm/G65eTeHahyb1oUpEz8bucdvYaje5Pw/8JSbkYEtWNPs3tGQ1sa0hNyx64gXLlPpUVNgzLzpio7YyiNZPu+rDeLMazqrqp7u3r8buM/Nq8uk2EnyTpM83rRM1hGNp2g+B6PTSsZrhy4HmgKGUYplFw5RJdXe/WTLtw35xOpjfwn6i02W5gsVe52H1cwT50vMN0qx65n0mlT+WXpC8E1UNjxvKHNLeMTsO25tQqO+djB/b5a24A0+7jeLfI78Lpie/X8M5kw6oMzwtyaReHypYd12C/ujkKv5yU/yOqr2MQ5GfXhxTacH1m2Z57pTijVlkpRz67j1BhpSAG+KPaFGPN9/1ut3bDTqqwSBoP2Ef1EvIXNBnR0OVj+9+VP1WfE83YdbtO7Ho4RAwO8q+i7z1C+RZnAideDUEvTieTKbbgwknY7/V6l6w6TOuX2MTmTNVP5dcKlKh+CdU3XFA4bFLfgvwzZH4fMaBzp3ztcvhhGFsI8N0yvC7CtFvjTbhg6iP7dWisorhY70yPnzEpVTkXN0fyy73b0/BD/k+EzO8bdWQ+dGDXF7Y1o+uys2L5Oe9aRCw/F+LSZPsc/0gWTkR2UlVVLccGOQ8L8a9vjqtP1RPuc/MdwOzCYNeb1LEtGyloLyB/4O7lH19HIUA3hGnoXaz50/36m0j+M3Wz2VRYfQ595lD9F0f21U0y/F8kqTSbEaRHBaLnsOth0zJCuA75lUj+86co7RAFKk6Q+Tdd35+u1zw18V5GXSHqnH0ZHi64Frbb0g/In35A9bH8zSF1WH52HR3IQqspt0Zl2If8109RrFfxqhv0XGSeX4yqv6edaVD9ohd/nOES1M6qe/nVu2/yn7qPs/dD3+7TwIhcD8my7L5now0+Qf4VN184GPBAo1G8ylyaJmpvvav+nKaq7PHhRzkF5KgiK7B693z3HGWheqYnqg/y3zYQeg8H0BzVP2D50XwbrqIESEGljKCPHM5BGh9WcQksl8ubXfNF9SPz9czr3y2oco53AbNf717unnfNN//ds3cO+YfNkPjIiVzHHlh+BQeQsuv6V4d0r9U4/of1phvPevBar1Q20QM6QqW6T7/nlz939rXCu1P7KZYfqof2HK7bncj1Bld/C+fdGPIHMIAw7EOPxOPT7nD2uZPYvs6tP7eLOncA9Yz2zf/5Tn2J7FeS1Xf/8PljB/I3IL9nIAaGbfPZWyfFwdkblOG56+JfgDE3GxfnXneCvrM2o0fX7Nb+0qtM4ZLvqiYXosdMRd7iVtGA/Pz1RYuoJuTPZ7O3DQ/Nf9Ynb+ZRZzarU30267itVsttY2i12+3dCEyIjtQzoyG6L/8uXIBCIb5F99fP38jlE2cvCiBdKmWzpVIeVymb5wFX/sd59+Pcnp69IJt6O75jXiAQCAQCgUAgEAgEAoFAIBAIBAKBQPAf8Q8e/L8DY6yYtwAAAABJRU5ErkJggg==', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#FEDA3B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-02T01:45:54.903' AS DateTime), NULL)
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (3, 1, N'sample', N'#768f76', N'#dfdfd2', N'YBMG4', N'https://blog.hubspot.com/hubfs/image8-2.jpg', N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'Welcome Data Science', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum', NULL, NULL, NULL, N'20px', N'#fe3939', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#d1d8d0', N'CopyRight SA@2023sdsdsd', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-02T01:51:28.377' AS DateTime), CAST(N'2023-11-03T19:05:53.827' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (4, 0, N'sample', N'#983434', N'#FEDA3B', N'StellaZella', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#fe7439', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#983434', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-03T23:10:35.617' AS DateTime), NULL)
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (5, 2, N'sample', N'#345298', N'#FEDA3B', N'SDF', N'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAH8AAAB/CAMAAADxY+0hAAABAlBMVEX///8AAAAAl9Xg4ODo6Ojt6uj39PEAkdOjzerPQETo7OwAldQAk9SfzOnY6vbo6+nx9fX1zQDm6OzptM/20zp+uuLpra714Or20Szw8ffNLzT89fiWyenOOT0Ai9Hy+PzNRZP656LRWJv55JXKGyL878T31UnfjI7++/HlpKX99dvPUJfD3vEAgs7++en88s/32Fz139/y1NXRTlHYeazZdXfVbaXvzt754YfW1NP43HD767MAf7Pgl72Hh4fBwcFCQkLsu7zprrmww+IZGRlRUVLIAAfcibXM1+pBoNaqq652dnbWZ2kvLy9jY2RksN0AYosAO1PJhrYAU3sAbZ+6pczHWLkMAAAFWUlEQVRoge2Wa3faRhCGR4ZGCAkVRcIIyVwkBIiLuRljDCYurWtK4gS3cf//X+k7AhGD8iE9p/WnfY61WgmW2Zl3ZtZEAoFAIBAIBAKBQCAQCAQCgUAgEAgEgv+LbPrtyCfNp6U35DZp//Yt7aeSEfggSffv36fT3y6MfKWz2Sxfx/yc5Kd/QSoRgV8kiRYDosWCyPM6NPC8AXl4Gp2fr+j8hF/NJL/lIjKZTC6e5Q6zI3L6u9SJ+dSD9DsZsG/MqWMYHRraRmNg2E1qBUFrHDjO42g0agfO1Wg0LjtFZt31a5NoVlzWut0/dKZ3KcubaBbf8a4iH3Gpp07jfy9JHztWSA1jQZ49DMmwrH7dtj1SFGfcdpTyE761cpw20ThoR2vWvm/G65eTeHahyb1oUpEz8bucdvYaje5Pw/8JSbkYEtWNPs3tGQ1sa0hNyx64gXLlPpUVNgzLzpio7YyiNZPu+rDeLMazqrqp7u3r8buM/Nq8uk2EnyTpM83rRM1hGNp2g+B6PTSsZrhy4HmgKGUYplFw5RJdXe/WTLtw35xOpjfwn6i02W5gsVe52H1cwT50vMN0qx65n0mlT+WXpC8E1UNjxvKHNLeMTsO25tQqO+djB/b5a24A0+7jeLfI78Lpie/X8M5kw6oMzwtyaReHypYd12C/ujkKv5yU/yOqr2MQ5GfXhxTacH1m2Z57pTijVlkpRz67j1BhpSAG+KPaFGPN9/1ut3bDTqqwSBoP2Ef1EvIXNBnR0OVj+9+VP1WfE83YdbtO7Ho4RAwO8q+i7z1C+RZnAideDUEvTieTKbbgwknY7/V6l6w6TOuX2MTmTNVP5dcKlKh+CdU3XFA4bFLfgvwzZH4fMaBzp3ztcvhhGFsI8N0yvC7CtFvjTbhg6iP7dWisorhY70yPnzEpVTkXN0fyy73b0/BD/k+EzO8bdWQ+dGDXF7Y1o+uys2L5Oe9aRCw/F+LSZPsc/0gWTkR2UlVVLccGOQ8L8a9vjqtP1RPuc/MdwOzCYNeb1LEtGyloLyB/4O7lH19HIUA3hGnoXaz50/36m0j+M3Wz2VRYfQ595lD9F0f21U0y/F8kqTSbEaRHBaLnsOth0zJCuA75lUj+86co7RAFKk6Q+Tdd35+u1zw18V5GXSHqnH0ZHi64Frbb0g/In35A9bH8zSF1WH52HR3IQqspt0Zl2If8109RrFfxqhv0XGSeX4yqv6edaVD9ohd/nOES1M6qe/nVu2/yn7qPs/dD3+7TwIhcD8my7L5now0+Qf4VN184GPBAo1G8ylyaJmpvvav+nKaq7PHhRzkF5KgiK7B693z3HGWheqYnqg/y3zYQeg8H0BzVP2D50XwbrqIESEGljKCPHM5BGh9WcQksl8ubXfNF9SPz9czr3y2oco53AbNf717unnfNN//ds3cO+YfNkPjIiVzHHlh+BQeQsuv6V4d0r9U4/of1phvPevBar1Q20QM6QqW6T7/nlz939rXCu1P7KZYfqof2HK7bncj1Bld/C+fdGPIHMIAw7EOPxOPT7nD2uZPYvs6tP7eLOncA9Yz2zf/5Tn2J7FeS1Xf/8PljB/I3IL9nIAaGbfPZWyfFwdkblOG56+JfgDE3GxfnXneCvrM2o0fX7Nb+0qtM4ZLvqiYXosdMRd7iVtGA/Pz1RYuoJuTPZ7O3DQ/Nf9Ynb+ZRZzarU30267itVsttY2i12+3dCEyIjtQzoyG6L/8uXIBCIb5F99fP38jlE2cvCiBdKmWzpVIeVymb5wFX/sd59+Pcnp69IJt6O75jXiAQCAQCgUAgEAgEAoFAIBAIBAKBQPAf8Q8e/L8DY6yYtwAAAABJRU5ErkJggg==', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome  Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#fe3939', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#983434', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-03T23:23:55.873' AS DateTime), CAST(N'2023-11-03T23:32:54.413' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (6, 3, N'sample', N'#3B9935', N'#FEDA3B', N'StellaZella', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#FEDA3B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-03T23:33:26.000' AS DateTime), NULL)
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (7, 3, N'sample', N'#3B9935', N'#FEDA3B', N'StellaZella', N'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAH8AAAB/CAMAAADxY+0hAAABAlBMVEX///8AAAAAl9Xg4ODo6Ojt6uj39PEAkdOjzerPQETo7OwAldQAk9SfzOnY6vbo6+nx9fX1zQDm6OzptM/20zp+uuLpra714Or20Szw8ffNLzT89fiWyenOOT0Ai9Hy+PzNRZP656LRWJv55JXKGyL878T31UnfjI7++/HlpKX99dvPUJfD3vEAgs7++en88s/32Fz139/y1NXRTlHYeazZdXfVbaXvzt754YfW1NP43HD767MAf7Pgl72Hh4fBwcFCQkLsu7zprrmww+IZGRlRUVLIAAfcibXM1+pBoNaqq652dnbWZ2kvLy9jY2RksN0AYosAO1PJhrYAU3sAbZ+6pczHWLkMAAAFWUlEQVRoge2Wa3faRhCGR4ZGCAkVRcIIyVwkBIiLuRljDCYurWtK4gS3cf//X+k7AhGD8iE9p/WnfY61WgmW2Zl3ZtZEAoFAIBAIBAKBQCAQCAQCgUAgEAgEgv+LbPrtyCfNp6U35DZp//Yt7aeSEfggSffv36fT3y6MfKWz2Sxfx/yc5Kd/QSoRgV8kiRYDosWCyPM6NPC8AXl4Gp2fr+j8hF/NJL/lIjKZTC6e5Q6zI3L6u9SJ+dSD9DsZsG/MqWMYHRraRmNg2E1qBUFrHDjO42g0agfO1Wg0LjtFZt31a5NoVlzWut0/dKZ3KcubaBbf8a4iH3Gpp07jfy9JHztWSA1jQZ49DMmwrH7dtj1SFGfcdpTyE761cpw20ThoR2vWvm/G65eTeHahyb1oUpEz8bucdvYaje5Pw/8JSbkYEtWNPs3tGQ1sa0hNyx64gXLlPpUVNgzLzpio7YyiNZPu+rDeLMazqrqp7u3r8buM/Nq8uk2EnyTpM83rRM1hGNp2g+B6PTSsZrhy4HmgKGUYplFw5RJdXe/WTLtw35xOpjfwn6i02W5gsVe52H1cwT50vMN0qx65n0mlT+WXpC8E1UNjxvKHNLeMTsO25tQqO+djB/b5a24A0+7jeLfI78Lpie/X8M5kw6oMzwtyaReHypYd12C/ujkKv5yU/yOqr2MQ5GfXhxTacH1m2Z57pTijVlkpRz67j1BhpSAG+KPaFGPN9/1ut3bDTqqwSBoP2Ef1EvIXNBnR0OVj+9+VP1WfE83YdbtO7Ho4RAwO8q+i7z1C+RZnAideDUEvTieTKbbgwknY7/V6l6w6TOuX2MTmTNVP5dcKlKh+CdU3XFA4bFLfgvwzZH4fMaBzp3ztcvhhGFsI8N0yvC7CtFvjTbhg6iP7dWisorhY70yPnzEpVTkXN0fyy73b0/BD/k+EzO8bdWQ+dGDXF7Y1o+uys2L5Oe9aRCw/F+LSZPsc/0gWTkR2UlVVLccGOQ8L8a9vjqtP1RPuc/MdwOzCYNeb1LEtGyloLyB/4O7lH19HIUA3hGnoXaz50/36m0j+M3Wz2VRYfQ595lD9F0f21U0y/F8kqTSbEaRHBaLnsOth0zJCuA75lUj+86co7RAFKk6Q+Tdd35+u1zw18V5GXSHqnH0ZHi64Frbb0g/In35A9bH8zSF1WH52HR3IQqspt0Zl2If8109RrFfxqhv0XGSeX4yqv6edaVD9ohd/nOES1M6qe/nVu2/yn7qPs/dD3+7TwIhcD8my7L5now0+Qf4VN184GPBAo1G8ylyaJmpvvav+nKaq7PHhRzkF5KgiK7B693z3HGWheqYnqg/y3zYQeg8H0BzVP2D50XwbrqIESEGljKCPHM5BGh9WcQksl8ubXfNF9SPz9czr3y2oco53AbNf717unnfNN//ds3cO+YfNkPjIiVzHHlh+BQeQsuv6V4d0r9U4/of1phvPevBar1Q20QM6QqW6T7/nlz939rXCu1P7KZYfqof2HK7bncj1Bld/C+fdGPIHMIAw7EOPxOPT7nD2uZPYvs6tP7eLOncA9Yz2zf/5Tn2J7FeS1Xf/8PljB/I3IL9nIAaGbfPZWyfFwdkblOG56+JfgDE3GxfnXneCvrM2o0fX7Nb+0qtM4ZLvqiYXosdMRd7iVtGA/Pz1RYuoJuTPZ7O3DQ/Nf9Ynb+ZRZzarU30267itVsttY2i12+3dCEyIjtQzoyG6L/8uXIBCIb5F99fP38jlE2cvCiBdKmWzpVIeVymb5wFX/sd59+Pcnp69IJt6O75jXiAQCAQCgUAgEAgEAoFAIBAIBAKBQPAf8Q8e/L8DY6yYtwAAAABJRU5ErkJggg==', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#FEDA3B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-03T23:34:02.160' AS DateTime), NULL)
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (8, 4, N'sample', N'#3B9935', N'#bfd71d', N'StellaZella', N'data:image/png;base64,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', N'https://previews.123rf.com/images/junce/junce1711/junce171100299/90592799-white-line-on-artifact-grass-sport-field-for-texture-background.jpg', N'Welcome Data Science', N'Ipsum1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', NULL, NULL, NULL, N'20px', N'#7efe39', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#34985f', N'CopRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-03T23:36:43.200' AS DateTime), CAST(N'2023-11-05T22:25:44.057' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (9, 5, N'sample', N'#91886e', N'#fe3939', N'Tenant', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome AnalyticsScience', N'Hello everyone ,sa hope you are doing gooddssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss', NULL, NULL, NULL, N'20px', N'#3940fe', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#919834', N'Copy1Right 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-04T15:22:33.737' AS DateTime), CAST(N'2023-11-04T15:24:42.393' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (10, 6, N'sample', N'#983434', N'#3998fe', N'tenant6', N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'Welcome DSSDSD Science', N'Hello everyone , hdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddope you are doing good', NULL, NULL, NULL, N'20px', N'#fe3939', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#987834', N'Copy1Right 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-04T15:31:36.540' AS DateTime), CAST(N'2023-11-04T15:33:15.453' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (11, 7, N'sample', N'#5171a4', N'#FEDA3B', N'StellaZella1', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'WelcomScience', N'Hello everyohhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhne , hope you are doing good', NULL, NULL, NULL, N'20px', N'#FEDA3B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#345a98', N'Copy0Right 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-04T19:58:36.793' AS DateTime), CAST(N'2023-11-04T20:00:00.210' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (12, 8, N'sample', N'#348498', N'#fe3939', N'ASDF', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Analytics', N's simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centur', NULL, NULL, NULL, N'18px', N'#5039fe', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#346898', N'Copy1Right 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-04T22:17:12.740' AS DateTime), CAST(N'2023-11-04T22:18:08.513' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (13, 9, N'sample', N'#3B9935', N'#FEDA3B', N'StellaZella', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'18px', N'#923535', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight@2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-05T23:18:29.747' AS DateTime), CAST(N'2023-11-05T23:34:02.863' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (14, 10, N'sample', N'#3B9935', N'#FEDA3B', N'StellaZella', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Data science is the study of data to extract meaningful insights for business.  It is a multidisciplinary approach that combines principles and practices from   the fields of mathematics, statistics, artificial intelligence, and computer engineering   to analyze large amounts of data', NULL, NULL, NULL, N'20px', N'#b02727', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-11-06T12:11:26.003' AS DateTime), CAST(N'2023-11-06T12:13:02.740' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (15, 11, N'sample', N'#4177e1', N'#be2727', N'StellaZella1', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome1 Data Science', N' Data1 science is the study of data to extract meaningful insights for business.  It is a multidisciplinary approach that combines principles and practices from   the fields of mathematics, statistics, artificial intelligence, and computer engineering   to analyze large amounts of data', N'Calibri', N'Calibri', N'Calibri', N'22px', N'#8f3232', N'#39db44', N'18px', N'Arial', N'#1ecc81', N'16px', N'Times New Roman', N'#db2956', N'14px', N'Arial', N'#b7c9a6', N'Co1pyRight 2024', N'#b437e1', N'www.2facebook.com', N'www.linkedin.com', N'', N'www.1instagram.cin', N'www.2whatsapp.vom', CAST(N'2023-11-06T12:42:45.457' AS DateTime), CAST(N'2024-02-27T13:03:01.777' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (1011, 12, N'sample', N'#145c1d', N'#4374a3', N'ssdsd', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'sssd', N'Hello everyone , hope you are doing good', NULL, NULL, NULL, N'16px', N'#FEDA3B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'#3B9935', N'CopyRight 2023', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-12-28T13:15:39.200' AS DateTime), CAST(N'2023-12-28T13:17:37.470' AS DateTime))
GO
INSERT [dbo].[Blog] ([BlogId], [TenantId], [BlogName], [HeaderBackgroundColor], [HeaderFontColor], [BrandName], [Logo], [Banner], [Heading], [TextContent], [HeaderFontStyle], [ContentFontStyle], [FooterFontStyle], [ContentFontSize], [ContentFontColor], [MainMenuColor], [MainMenuFontSize], [MainMenuFontStyle], [SubMenuColor], [SubMenuFontSize], [SubMenuFontStyle], [SubChildMenuColor], [SubChildMenuFontSize], [SubChildFontStyle], [FooterBackgroundColor], [FooterText], [FooterFontColor], [Facebook], [LinkedIn], [Twitter], [Instagram], [WhatsApp], [PublishedDateTime], [UpdatedDateTime]) VALUES (2012, 13, NULL, N'#41e144', N'#895d5d', N'StellaZella', N'data:image/png;base64,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', N'https://img.freepik.com/free-vector/stylish-glowing-digital-red-lines-banner_1017-23964.jpg', N'Welcome Data Science', N'Hello everyone , hope you are doing good', N'MS Serif', N'Cambria', N'Palatino Linotype', N'22px', N'#8f3232', N'#3954db', N'18px', N'Arial', N'#1ecc81', N'16px', N'Times New Roman', N'#db2956', N'14px', N'Arial', N'#b7c9a6', N'CopyRight 2024', N'#b437e1', N'www.2facebook.com', N'www.linkedin.com', N'', N'www.1instagram.cin', N'www.2whatsapp.vom', CAST(N'2024-01-30T21:59:55.087' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogImages] ON 
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (1, 3, 1, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'asdf', 1, NULL, CAST(N'2023-11-02T22:21:31.827' AS DateTime), NULL)
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (2, 3, 1, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'asdf', 1, NULL, CAST(N'2023-11-02T23:21:10.737' AS DateTime), NULL)
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (3, 3, 1, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'aqwwesdsdsdsd', 0, NULL, CAST(N'2023-11-03T18:13:53.430' AS DateTime), NULL)
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (4, 3, 1, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsdsd', 0, NULL, CAST(N'2023-11-03T19:05:58.943' AS DateTime), CAST(N'2023-11-03T19:05:58.943' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (12, 8, 4, N'https://www.shutterstock.com/image-photo/new-car-sedan-type-modern-260nw-2295122863.jpg', N'sdsdd', 0, NULL, CAST(N'2023-11-04T00:21:58.813' AS DateTime), CAST(N'2023-11-04T00:21:58.813' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (16, 1, 4, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsd', 0, NULL, CAST(N'2023-11-04T00:27:13.053' AS DateTime), CAST(N'2023-11-04T00:27:13.053' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (21, 1, 4, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'sdsdsdsd', 0, NULL, CAST(N'2023-11-04T15:05:41.287' AS DateTime), CAST(N'2023-11-04T15:05:41.287' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (22, 1, 4, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'ssdsdsd', 0, NULL, CAST(N'2023-11-04T15:09:36.063' AS DateTime), CAST(N'2023-11-04T15:09:36.063' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (28, 10, 6, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'ds', 0, NULL, CAST(N'2023-11-04T15:32:26.813' AS DateTime), CAST(N'2023-11-04T15:32:26.813' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (29, 10, 6, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'ssd', 0, NULL, CAST(N'2023-11-04T15:32:26.813' AS DateTime), CAST(N'2023-11-04T15:32:26.813' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (30, 10, 6, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'ds', 0, NULL, CAST(N'2023-11-04T15:33:15.570' AS DateTime), CAST(N'2023-11-04T15:33:15.570' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (31, 10, 6, N'https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais', N'ssd', 0, NULL, CAST(N'2023-11-04T15:33:15.570' AS DateTime), CAST(N'2023-11-04T15:33:15.570' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (32, 10, 6, N'https://blog.hubspot.com/hubfs/image8-2.jpg', N'sdsds', 0, NULL, CAST(N'2023-11-04T15:33:15.570' AS DateTime), CAST(N'2023-11-04T15:33:15.570' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (34, 8, 4, N'https://www.shutterstock.com/image-photo/new-car-sedan-type-modern-260nw-2295122863.jpg', N'jgugigig', 0, NULL, CAST(N'2023-11-04T19:56:32.967' AS DateTime), CAST(N'2023-11-04T19:56:32.967' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (35, 11, 7, N'https://www.shutterstock.com/image-photo/new-car-sedan-type-modern-260nw-2295122863.jpg', N'dhfhf', 0, NULL, CAST(N'2023-11-04T20:00:00.267' AS DateTime), CAST(N'2023-11-04T20:00:00.267' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (36, 11, 7, N'https://www.shutterstock.com/image-photo/new-car-sedan-type-modern-260nw-2295122863.jpg', N'gugu', 0, NULL, CAST(N'2023-11-04T20:00:00.267' AS DateTime), CAST(N'2023-11-04T20:00:00.267' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (37, 8, 4, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsdsdsdsd', 0, NULL, CAST(N'2023-11-04T22:12:12.987' AS DateTime), CAST(N'2023-11-04T22:12:12.987' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (38, 8, 4, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsdsdsdsd', 0, NULL, CAST(N'2023-11-04T22:12:51.317' AS DateTime), CAST(N'2023-11-04T22:12:51.317' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (40, 12, 8, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsd', 0, NULL, CAST(N'2023-11-04T22:18:08.563' AS DateTime), CAST(N'2023-11-04T22:18:08.563' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (41, 12, 8, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsd', 0, NULL, CAST(N'2023-11-04T22:18:08.563' AS DateTime), CAST(N'2023-11-04T22:18:08.563' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (42, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdssdsdsd', 0, NULL, CAST(N'2023-11-05T23:19:23.613' AS DateTime), CAST(N'2023-11-05T23:19:23.613' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (43, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsdsdsd', 0, NULL, CAST(N'2023-11-05T23:19:23.613' AS DateTime), CAST(N'2023-11-05T23:19:23.613' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (44, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdssdsdsd', 0, NULL, CAST(N'2023-11-05T23:19:44.280' AS DateTime), CAST(N'2023-11-05T23:19:44.280' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (54, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsd', 0, NULL, CAST(N'2023-11-05T23:33:38.870' AS DateTime), CAST(N'2023-11-05T23:33:38.870' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (55, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsd', 0, NULL, CAST(N'2023-11-05T23:34:02.913' AS DateTime), CAST(N'2023-11-05T23:34:02.913' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (56, 13, 9, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsdsd', 0, NULL, CAST(N'2023-11-05T23:34:02.913' AS DateTime), CAST(N'2023-11-05T23:34:02.913' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (57, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'casd', 0, NULL, CAST(N'2023-11-06T12:12:20.353' AS DateTime), CAST(N'2023-11-06T12:12:20.353' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (58, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'ssdsd', 0, NULL, CAST(N'2023-11-06T12:12:20.357' AS DateTime), CAST(N'2023-11-06T12:12:20.357' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (60, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsd', 0, NULL, CAST(N'2023-11-06T12:12:45.800' AS DateTime), CAST(N'2023-11-06T12:12:45.800' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (61, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsd', 0, NULL, CAST(N'2023-11-06T12:12:45.800' AS DateTime), CAST(N'2023-11-06T12:12:45.800' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (62, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdssd', 0, NULL, CAST(N'2023-11-06T12:13:02.830' AS DateTime), CAST(N'2023-11-06T12:13:02.830' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (63, 14, 10, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdsd', 0, NULL, CAST(N'2023-11-06T12:13:02.830' AS DateTime), CAST(N'2023-11-06T12:13:02.830' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (64, 15, 11, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'AshadZella', 0, NULL, CAST(N'2023-11-06T12:47:00.343' AS DateTime), CAST(N'2023-11-06T12:47:00.343' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (65, 15, 11, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'ssdsdsd', 0, NULL, CAST(N'2023-11-06T12:47:00.343' AS DateTime), CAST(N'2023-11-06T12:47:00.343' AS DateTime))
GO
INSERT [dbo].[BlogImages] ([ImageId], [BlogId], [TenantId], [ImageUrl], [Text], [Position], [UDF], [CreatedDate], [UpdatedDate]) VALUES (67, 15, 11, N'https://t3.ftcdn.net/jpg/01/23/52/24/360_F_123522471_XZe5ebqil1DFJRgOUJ6taDP4DnmHjtL7.jpg', N'sdssd', 0, NULL, CAST(N'2023-11-06T12:47:41.733' AS DateTime), CAST(N'2023-11-06T12:47:41.733' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[BlogImages] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomMenu] ON 
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (1, 11, N'Home', 0, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4, 11, N'Teams', 0, NULL, 3)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (10, 11, N'News', 1, NULL, 2)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (1014, 11, N'Content Section', 1015, N'/ContentSection', 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (1015, 11, N'AboutUs', 0, N'ContentSection', 2)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (1016, 11, N'Home1sub1', 10, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (2019, 11, N'Home3', 2020, NULL, 2)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (2020, 11, N'TestMenu', 2020, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (2024, 11, N'Content With HeaderImage', 1015, N'/ContentWithHeaderImage', 2)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (2025, 11, N'Content With InlineImage', 1015, N'/ContentWithInlineImage', 3)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3030, 11, N'H1', 3028, NULL, 2)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3032, 11, N's1', 3028, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3038, 11, N'D1', 3028, NULL, 3)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3040, 11, N'sdsdsd', 3045, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3045, 11, N'D1', 3040, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3047, 11, N's1', 3047, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3048, 12, N'HomeT', 0, N'HomeT', 5)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (3049, 13, N'Home13', 0, N'/Home13', 4)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4049, 11, N'Blog Three ColumnImages', 4051, NULL, 3)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4050, 11, N'Blog With SingleColumn', 4051, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4051, 11, N'Blogs', 1, NULL, 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4052, 11, N'Teams LargeImages', 4, N'/TeamsLargeImages', 1)
GO
INSERT [dbo].[CustomMenu] ([MenuId], [TenantId], [MenuName], [ParentMenuId], [PageUrl], [Position]) VALUES (4053, 11, N'TeamWithRoundImage', 4, N'/TeamWithRoundImage', 2)
GO
SET IDENTITY_INSERT [dbo].[CustomMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[Page] ON 
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (1, 11, N'Tolle Neuigkeiten!', 1, N'/tolle-neuigkeiten', 1, 10, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (2, 11, N'Kalender', 0, N'/Kalender', 1, 10, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (3, 11, N'Kalender', 0, N'/Kalender', 1, 10, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (4, 11, N'Über uns', 0, N'/Über uns', 1015, NULL, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (5, 11, N'abv', 0, N'/abv', 1, 20, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (6, 11, N'neue seite', 0, N'/neue seite', 1014, 45, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (7, 11, N'sdsdsd', 0, N'sdsdsd', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (8, 11, N'sdsdsd', 1, N'sdsd', 1015, 23, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (9, 11, N'sdsdsd', 0, N'sdsd', 1014, 12, NULL, NULL)
GO
INSERT [dbo].[Page] ([Id], [TenantId], [PageTitle], [IsHomePage], [ResourcePath], [MenuId], [PaddingWidth], [PublishStartDate], [PublishEndDate]) VALUES (10, 11, N'Tolle Neuigkeiten!', NULL, N'/tolle-neuigkeiten', 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Page] OFF
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (1, N'VS', N'Nascar', N'@naskar@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (2, N'A', N'ASD', N'asd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (3, N'B', N'BSD', N'bsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (4, N'C', N'CSD', N'csd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (5, N'D', N'DSD', N'dsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (6, N'E', N'ESD', N'esd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (7, N'F', N'FSD', N'fsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (8, N'G', N'GSD', N'gsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (9, N'H', N'HSD', N'hsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (10, N'J', N'ASD', N'asd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (11, N'K', N'BSD', N'bsd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (12, N'L', N'CSD', N'csd@gmail.com', N'9035012303')
GO
INSERT [dbo].[Tenants] ([TenantId], [LastName], [FirstName], [Email], [PhoneNumber]) VALUES (13, N'M', N'DSD', N'dsd@gmail.com', N'9035012303')
GO
ALTER TABLE [dbo].[BlogImages]  WITH CHECK ADD  CONSTRAINT [FK_BlogImages_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([BlogId])
GO
ALTER TABLE [dbo].[BlogImages] CHECK CONSTRAINT [FK_BlogImages_Blog]
GO
ALTER TABLE [dbo].[BlogImages]  WITH CHECK ADD  CONSTRAINT [FK_BlogImages_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[BlogImages] CHECK CONSTRAINT [FK_BlogImages_Tenants]
GO
ALTER TABLE [dbo].[ComponentProperty]  WITH CHECK ADD  CONSTRAINT [FK_ComponentProperty_Component] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([Id])
GO
ALTER TABLE [dbo].[ComponentProperty] CHECK CONSTRAINT [FK_ComponentProperty_Component]
GO
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_Component] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([Id])
GO
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_Component]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_CustomMenu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[CustomMenu] ([MenuId])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_CustomMenu]
GO
USE [master]
GO
ALTER DATABASE [Association] SET  READ_WRITE 
GO

USE [master]
GO

IF DB_ID('JsonDiffer') IS NOT NULL
  set noexec on 

CREATE DATABASE [JsonDiffer];
GO

USE [JsonDiffer]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE LOGIN [json_differ_user] WITH PASSWORD = 'Abc@12345'
GO

EXEC sp_addrolemember N'db_owner', N'json_differ_user'
GO
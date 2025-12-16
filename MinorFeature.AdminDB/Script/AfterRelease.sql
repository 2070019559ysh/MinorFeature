/*
后期部署脚本模板							
--------------------------------------------------------------------------------------
 此文件包含将附加到生成脚本中的 SQL 语句。		
 使用 SQLCMD 语法将文件包含到后期部署脚本中。			
 示例:      :r .\myfile.sql								
 使用 SQLCMD 语法引用后期部署脚本中的变量。		
 示例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS(SELECT Id FROM AdminUser WHERE [LgAccount]='MF_Admin')
	INSERT INTO AdminUser([LgAccount],[Email],[PasswordHash],[NickName],[Gender],[Phone])
	VALUES('MF_Admin','admin@qq.com','E10ADC3949BA59ABBE56E057F20F883E',N'管理员','M','17390695275')
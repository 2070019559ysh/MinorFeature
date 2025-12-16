CREATE TABLE [dbo].[AdminUser]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LgAccount] VARCHAR(255) NOT NULL, 
	[Email] VARCHAR(255) NOT NULL,
	[PasswordHash] VARCHAR(32) NOT NULL,
	[NickName] NVARCHAR(50) NOT NULL DEFAULT N'昵称',
	[Gender] VARCHAR(1) NULL CHECK(Gender IN('M','F','0') OR Gender IS NULL) DEFAULT '0',
    [Phone] VARCHAR(11) NULL,
	[Status] INT NOT NULL DEFAULT 0,
	[CreateTime] DATETIME NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT UC_LgAccount UNIQUE (LgAccount)
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'使用整型ID自增',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户设置登录账号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'LgAccount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'需要验证过的邮箱',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'昵称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'NickName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'手机号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'Phone'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别M=男，F=女，空是不知道',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = 'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'-1=被注销，0=正常，1=拒绝访问',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户加密登录密码',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AdminUser',
    @level2type = N'COLUMN',
    @level2name = 'PasswordHash'
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](150) NULL,
	[EmployeeStatus] [varchar](50) NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[PayBasis] [varchar](50) NULL,
	[PositionTitle] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

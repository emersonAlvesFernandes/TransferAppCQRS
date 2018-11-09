

CREATE TABLE [dbo].[tbl_customer](	
	[uid_customer] [uniqueidentifier] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[birthdate] [datetime] NOT NULL,
	[email] [varchar](50) NOT NULL,

	PRIMARY KEY (uid_customer)
)
GO

create table tbl_account(	
	[uid_account] [uniqueidentifier] NOT NULL,	
	agency int NOT NULL, 
    number int not null,
    address varchar(100),
    uid_customer UNIQUEIDENTIFIER references tbl_customer(uid_customer)

	PRIMARY KEY (uid_account)
)
GO


select * from tbl_customer
select * from tbl_account

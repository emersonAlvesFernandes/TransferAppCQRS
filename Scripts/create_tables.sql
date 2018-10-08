
CREATE TABLE [dbo].[tbl_cliente](	
	[uid_cliente] [uniqueidentifier] NOT NULL,
	[nm_cliente] [nchar](100) NOT NULL,
	[dt_nascimento] [datetime] NOT NULL,
	[cd_documento] [varchar](11) NOT NULL,
	[ds_email] [varchar](50) NOT NULL,
	[dt_alteracao] [datetime] NOT NULL,
	[dt_criacao] [datetime] NOT NULL,
	[id_usuario] [int] NOT NULL,

	PRIMARY KEY (uid_cliente)
)
GO

CREATE TABLE tbl_agencia(	
	[uid_agencia] [uniqueidentifier] NOT NULL,
	cd_agencia		int UNIQUE NOT NULL, 
	ds_estado		varchar(2) NOT NULL,
	ds_cidade		varchar(100) NOT NULL, 
	ds_cep			varchar(8) NOT NULL, 
	ds_rua			varchar(100) NOT NULL, 
	ds_numero		varchar(20) NOT NULL, 
	ds_complemento	varchar(100) NOT NULL, 
	dt_alteracao	datetime NOT NULL, 
	dt_criacao		datetime NOT NULL, 
	id_usuario		int,

	PRIMARY KEY (uid_agencia)
)
GO

create table tbl_conta(	
	[uid_conta] [uniqueidentifier] NOT NULL,	
	uid_cliente [uniqueidentifier] FOREIGN KEY REFERENCES tbl_cliente(uid_cliente),	
	uid_agencia [uniqueidentifier] FOREIGN KEY REFERENCES tbl_agencia(uid_agencia),
	cd_conta varchar(4) NOT NULL, 
	dt_alteracao datetime, 
	dt_criacao datetime, 
	id_usuario int,

	PRIMARY KEY (uid_conta)
)
GO

create table tbl_movimentacao_transferencia (
	uid_movimentacao_transferencia [uniqueidentifier] NOT NULL,			
	uid_conta_origem	uniqueidentifier REFERENCES tbl_conta(uid_conta),
	uid_conta_destino	uniqueidentifier REFERENCES tbl_conta(uid_conta),
	ds_descricao		varchar(100),
	vl_transacao		float,
	dt_transacao		datetime,

	PRIMARY KEY (uid_movimentacao_transferencia)
)
GO

create table tbl_movimentacao_comum(
	uid_movimentacao_comum	uniqueidentifier NOT NULL,			
	uid_conta_origem		uniqueidentifier REFERENCES tbl_conta(uid_conta),
	vl_transacao			float,
	vl_saldo_anterior		float,
	dt_transacao			datetime,

	PRIMARY KEY (uid_movimentacao_comum)
)
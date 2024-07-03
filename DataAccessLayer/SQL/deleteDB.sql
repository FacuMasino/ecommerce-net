use master
go
alter database [ecommerce] set single_user with rollback immediate

drop database [ecommerce]
use master

go

alter database [ecommerce_net_db] set single_user with rollback immediate

drop database [ecommerce_net_db]
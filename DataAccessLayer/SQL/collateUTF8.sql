use ecommerce_net_db
-- Cambiar la base de datos a modo de usuario único y desconectar conexiones activas
ALTER DATABASE ecommerce_net_db
SET
    SINGLE_USER
WITH
    ROLLBACK IMMEDIATE;

-- Cambiar la collation de la base de datos
ALTER DATABASE ecommerce_net_db COLLATE Latin1_General_100_CI_AS_SC_UTF8;

-- Volver a poner la base de datos en modo multiusuario
ALTER DATABASE ecommerce_net_db
SET
    MULTI_USER;

-- Restaurar el modo de lectura/escritura (opcional, ya que READ_ONLY ya fue cambiado)
ALTER DATABASE ecommerce_net_db
SET
    READ_WRITE;

-- Verificar la collation
SELECT
    name,
    collation_name
FROM
    sys.databases
WHERE
    name = 'ecommerce_net_db';
USE ecommerce_net_db;

-- Eliminar todas las restricciones de clave externa
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
               ' DROP CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.foreign_keys;

EXEC sp_executesql @sql;

-- Eliminar todas las tablas
EXEC sp_MSForEachTable "DROP TABLE ?";

-- Verificación
SELECT * FROM sys.tables; -- Debería retornar 0 filas si todas las tablas fueron eliminadas.

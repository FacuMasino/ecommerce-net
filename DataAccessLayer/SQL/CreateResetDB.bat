@echo off
setlocal enabledelayedexpansion

:menu
echo Elija una cadena de conexion:
echo.
echo 1- ana/facu
echo 2- maxi_bangho
echo 3- maxi_mac_1
echo 4- maxi_mac_2
echo.

set /p choice=Ingrese su eleccion (1, 2, 3 o 4): 

if "%choice%"=="1" (
    set connection_string="localhost\SQLEXPRESS"
) else if "%choice%"=="2" (
    set connection_string="BANGHO\SQLEXPRESS"
) else if "%choice%"=="3" (
    set connection_string=192.168.0.221 -U SA -P Password1234
) else if "%choice%"=="4" (
    set connection_string=192.168.0.20 -U SA -P Password1234
) else (
    echo Opción no válida. Por favor, intente nuevamente.
    goto menu
)

cls
echo.
echo La cadena de conexion seleccionada es: %connection_string%
echo.
echo Esta a punto de escribir la base de datos DESDE 0.
echo SE PERDERAN LOS DATOS ACTUALES, seguro desea continuar? (S/N)
echo.
set /p confirm=Su eleccion: 

if /i "%confirm%" NEQ "S" (
    echo Operacion cancelada.
    pause
    goto menu
)

sqlcmd -S %connection_string% -i deleteDB.sql
sqlcmd -S %connection_string% -i database.sql -f 65001
sqlcmd -S %connection_string% -i storedProcedures.sql -f 65001
sqlcmd -S %connection_string% -i views.sql -f 65001
sqlcmd -S %connection_string% -i dummyData.sql -f 65001

echo.
pause
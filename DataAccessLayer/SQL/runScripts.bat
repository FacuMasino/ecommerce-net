@echo off
setlocal enabledelayedexpansion

:menu
echo Choose database host:
echo.
echo 1- Localhost
echo 2- Bangho
echo 3- Docker (IP 192.168.0.221)
echo 4- Docker (IP 192.168.0.20)
echo.

set /p choice=Enter your choice (1, 2, 3 or 4): 

if "%choice%"=="1" (
    set database_host="localhost\SQLEXPRESS"
) else if "%choice%"=="2" (
    set database_host="BANGHO\SQLEXPRESS"
) else if "%choice%"=="3" (
    set database_host=192.168.0.221 -U SA -P Password1234
) else if "%choice%"=="4" (
    set database_host=192.168.0.20 -U SA -P Password1234
) else (
    echo Invalid option. Please try again.
    goto menu
)

cls
echo.
echo The selected host data is: %database_host%
echo.
echo Do you really want to overwrite the current database?
echo.
set /p confirm=Enter your choice (Y/N):

if /i "%confirm%" NEQ "Y" (
    echo Cancelled by user.
    pause
    goto menu
)

sqlcmd -S %database_host% -i deleteDatabase.sql
sqlcmd -S %database_host% -i createDatabase.sql
sqlcmd -S %database_host% -i collateUTF8.sql
sqlcmd -S %database_host% -i createTables.sql -f 65001
sqlcmd -S %database_host% -i createFunctions.sql -f 65001
sqlcmd -S %database_host% -i createStoredProcedures.sql -f 65001
sqlcmd -S %database_host% -i createViews.sql -f 65001
sqlcmd -S %database_host% -i insertData.sql -f 65001

echo.
pause
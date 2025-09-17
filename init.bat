@echo off
setlocal

:: Путь к mysql.exe
set MYSQL_PATH="C:\Program Files\MySQL\MySQL Server 8.4\bin\mysql.exe"

:: Учетные данные и база данных
set DB_USER=root
set DB_PASS=пароль
set DB_NAME=installsdb

:: Имя файла с тестовыми данными
set BACKUP_FILE=backup.sql

:: Проверяем, существует ли файл
if not exist "%BACKUP_FILE%" (
    echo Ошибка: Файл "%BACKUP_FILE%" не найден в текущей папке!
    echo Поместите "%BACKUP_FILE%" в ту же папку, где находится этот .bat файл.
    pause
    exit /b 1
)

:: Загружаем в БД
echo Восстановление базы данных из "%BACKUP_FILE%"...
%MYSQL_PATH% -u %DB_USER% -p%DB_PASS% %DB_NAME% < "%BACKUP_FILE%"

if %ERRORLEVEL% equ 0 (
    echo Бэкап успешно восстановлен!
) else (
    echo Ошибка при восстановлении бэкапа! Проверьте логи.
)

pause
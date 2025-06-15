@echo off
echo ========================================
echo    SETUP DU AN WINFORMS - QUAN LY QUAN BIDA
echo ========================================

echo.
echo [1/5] Kiem tra .NET SDK...
dotnet --version
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK khong duoc cai dat!
    echo Vui long tai va cai dat .NET 8.0 SDK tu: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo.
echo [2/5] Restore dependencies...
dotnet restore WinFormsApp1.sln

echo.
echo [3/5] Tao database...
sqlcmd -S MAY-02\SQLEXPRESS -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BilliardsManagement_DB') CREATE DATABASE BilliardsManagement_DB"

echo.
echo [4/5] Chay migration...
cd Models
dotnet ef database update --startup-project ../WinFormsApp1
cd ..

echo.
echo [5/5] Build solution...
dotnet build WinFormsApp1.sln --configuration Debug

echo.
echo ========================================
echo    SETUP HOAN THANH!
echo ========================================
echo.
echo De chay ung dung, su dung lenh:
echo dotnet run --project WinFormsApp1/WinFormsApp1.csproj
echo.
pause

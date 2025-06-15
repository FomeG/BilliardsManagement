# PowerShell Setup Script for Billiards Management System
Write-Host "========================================" -ForegroundColor Green
Write-Host "   SETUP DU AN WINFORMS - QUAN LY QUAN BIDA" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green

Write-Host ""
Write-Host "[1/5] Kiem tra .NET SDK..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET SDK version: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ ERROR: .NET SDK khong duoc cai dat!" -ForegroundColor Red
    Write-Host "Vui long tai va cai dat .NET 8.0 SDK tu: https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Red
    Read-Host "Nhan Enter de thoat"
    exit 1
}

Write-Host ""
Write-Host "[2/5] Restore dependencies..." -ForegroundColor Yellow
dotnet restore WinFormsApp1.sln
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Loi khi restore dependencies!" -ForegroundColor Red
    Read-Host "Nhan Enter de thoat"
    exit 1
}
Write-Host "✓ Dependencies restored successfully!" -ForegroundColor Green

Write-Host ""
Write-Host "[3/5] Tao database..." -ForegroundColor Yellow
try {
    sqlcmd -S MAY-02\SQLEXPRESS -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BilliardsManagement_DB') CREATE DATABASE BilliardsManagement_DB"
    Write-Host "✓ Database created/verified successfully!" -ForegroundColor Green
} catch {
    Write-Host "✗ Loi khi tao database! Kiem tra SQL Server Express." -ForegroundColor Red
    Read-Host "Nhan Enter de thoat"
    exit 1
}

Write-Host ""
Write-Host "[4/5] Chay migration..." -ForegroundColor Yellow
Set-Location Models
dotnet ef database update --startup-project ../WinFormsApp1
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Loi khi chay migration!" -ForegroundColor Red
    Set-Location ..
    Read-Host "Nhan Enter de thoat"
    exit 1
}
Set-Location ..
Write-Host "✓ Migration completed successfully!" -ForegroundColor Green

Write-Host ""
Write-Host "[5/5] Build solution..." -ForegroundColor Yellow
dotnet build WinFormsApp1.sln --configuration Debug
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ Loi khi build solution!" -ForegroundColor Red
    Read-Host "Nhan Enter de thoat"
    exit 1
}
Write-Host "✓ Solution built successfully!" -ForegroundColor Green

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "    SETUP HOAN THANH!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "De chay ung dung, su dung lenh:" -ForegroundColor Cyan
Write-Host "dotnet run --project WinFormsApp1/WinFormsApp1.csproj" -ForegroundColor White
Write-Host ""
Read-Host "Nhan Enter de thoat"

@echo off

:: BatchGotAdmin
:-------------------------------------
REM  --> Check for permissions
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

REM --> If error flag set, we do not have admin.
if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params = %*:"=""
    echo UAC.ShellExecute "cmd.exe", "/c %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"
:--------------------------------------
:choice
echo This file will forcibly kill your sqlservr process and delete all files in directory WebApplication1\App_Data\ that end in _log.ldf
set /P c=Are you sure you want to continue[Y/N]?
if /I "%c%" EQU "Y" goto :yes
if /I "%c%" EQU "N" goto :no
goto :choice


:yes
cls
taskkill /F /IM sqlservr.exe && echo Process sqlserver.exe killed succesfully
del WebApplication1\App_Data\*_log.ldf
pause 
exit

:no
cls
echo Shutting Down...
pause 
exit
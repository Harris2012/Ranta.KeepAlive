@echo off
set /p var=�Ƿ�Ҫ��װ WCF ����(Y/N):
if "%var%" == "y" (goto install) else (goto batexit)

SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319

:install
call InstallUtil.exe Ranta.KeepAlive.exe
pause

:batexit
exit
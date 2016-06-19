@echo off
set /p var=是否要卸载 WCF服务(Y/N):
if "%var%" == "y" (goto uninstall) else (goto batexit)

SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319

:uninstall
call InstallUtil.exe /u Ranta.KeepAlive.exe
pause

:batexit
exit
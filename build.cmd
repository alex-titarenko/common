@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

msbuild TAlex.Common.sln /p:Configuration="%config%" /p:BuildPackage=true

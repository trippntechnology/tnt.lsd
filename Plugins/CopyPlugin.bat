@echo off

set library=%~1
set sourceDir=%~2
set destinationDir=%~3

if not exist %destinationDir% (
  echo Creating destination directory: %destinationDir%
  mkdir %destinationDir%
)

set sourcePath=%sourceDir%\%library%
set destinationPath=%destinationDir%\%library%

REM Copy the source file to the destination
echo Copying %sourcePath% %destinationPath%
copy %sourcePath% %destinationPath% >nul
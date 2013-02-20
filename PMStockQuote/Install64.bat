@echo off
echo NOTE: You MUST close Excel before installing the add-in!
echo.
echo Copying files...
copy PMStockQuote64.xll "%AppData%\Microsoft\AddIns\PMStockQuote64.xll" /y
echo.

if errorlevel 4 goto access
if errorlevel 2 goto abort
if errorlevel 0 goto success

:access
echo ERROR!
echo Low memory/access error. Is Excel open?
echo If so, please close the program and try again.
goto pressexit

:abort
echo ERROR!
echo You pressed CTRL+C to end the copy operation.
goto pressexit

:success
echo Copied successfully!
echo.
echo Now opening the add-in in Excel...
start /d "%AppData%\Microsoft\AddIns" PMStockQuote64.xll
echo.
echo This window will close in 5 seconds...
ping -n 5 localhost>nul 2>&1
goto exit

:pressexit
echo.
echo Press any key to exit...
pause>nul

:exit
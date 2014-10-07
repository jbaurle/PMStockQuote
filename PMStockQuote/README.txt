-------------
Quick Install
-------------

This should work for most users. If it doesn't, follow the extended
instructions below.

1. Unzip this folder to a safe location.
2. Close Excel.
3. Run Install.bat for 32-bit Excel or Install64.bat for 64-bit Excel.
4. If you want to check the installation, open the PMStockQuote.xlsx spreadsheet.

--------------
Manual Install
--------------

1. Close Excel.
2. Unzip and save the PMStockQuote.xll or PMStockQuote64.xll in %AppData%\Microsoft\AddIns.
3. If this is your first install, do the following in Excel:
   * Excel 2013
     - Click FILE > Options > Add-Ins.
     - In the Manage dropdown at the bottom, click Excel Add-ins and Go... 
     - Place a checkmark next to PMStockQuote.
4. If you want to check the installation, open the PMStockQuote.xlsx spreadsheet.

----------------------------------------------------------------
How to find out what Excel version (32 or 64-bit) you are using?
----------------------------------------------------------------

1. Open Excel, click FILE > Account > Info-Button
2. The dialog is showing the version type 32-bit or 64-bit

------------------------------------------------------------------
How to execute Excel 2013 with Ctrl-F5 in Visual Studio 2012/2013?
------------------------------------------------------------------

1. Open project properties and select Debug tab.
2. In the comand line arguments textbox define the xll file to you are generating with this project.

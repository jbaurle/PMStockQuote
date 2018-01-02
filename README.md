# IMPORTANT NOTICE

The new version is now using the Google Finance API and is not compatible with the old add-in. Google Finance uses other ticker symbols. A new search dialog is now available and more data values can be retrieved. The old Stock Quote add-in was using the Yahoo Finance service to retrieve the stock quotes. The service seems to be terminated by Yahoo without any notice on 11/01/2017.

----
# PMStockQuote

**RELEASES**

Stock Quote Add-In For Excel 2018 R1<br>
https://github.com/jbaurle/PMStockQuote/releases/tag/Stock_Quote_Add-In_For_Excel_2018_R1

**Stock Quote Add-In For Excel 2016 and 2013**

The Stock Quote Add-In For Excel 2016/2013 is a small add-in based on the fantastic Excel-DNA library to retrieve stock data from Google Finance using the PSQ function.

**Usage**

The add-in is providing an Excel function called **PSQ** to retrieve stock data like open, last, low, high or name for the passed symbol. See the following formula samples:

* **=PSQ(A1;"PRICE")**, **=PSQ(A1;"CLOSE")**, **=PSQ(A1;"LAST")** or just **=PSQ(A1)**
* **=PSQ(A1;"OPEN")**
* **=PSQ(A1;"LOW")**
* **=PSQ(A1;"HIGH")**
* **=PSQ(A1;"LOW52")**
* **=PSQ(A1;"HIGH52")**
* **=PSQ(A1;"VOLUME")**
* **=PSQ(A1;"CHANGE")**
* **=PSQ(A1;"CHANGEPERCENTAGE")** or **=PSQ(A1;"CP")**
* **=PSQ(A1;"NAME")**
* **=PSQ(A1;"DATE")** => NOT last trade date, just date of last query
* **=PSQ(A1;"TIME")** => NOT last trade time, just time of last query
* **=PSQ(A1;"TICKER")** or **=PSQ(A1;"SYMBOL")**
* **=PSQ(A1;"EXCHANGE")**

DATE and TIME are the timestamp of the last query, not the last trade date. Google Finance API is not returning the this kind of data anymore.

**Screenshots**

German Edition of Excel 2016:

![](docs/PMStockQuoteExcelAddIn2016.png)

German Edition of Excel 2013:

![](docs/PMStockQuoteExcelAddIn.png)

**Links**

This add-in is based on a previous version I wrote for Excel 2007 and Excel 2010. Below I have listed a couple of links to the old version:

Blog with Stock Quote Add-In posts<br>
[http://www.parago.de/tag/excel/](http://www.parago.de/tag/excel/)

Stock Quote Add-In for Excel 2016<br>
[http://www.parago.de/blog/2017/01/01/stock-quote-add-in-for-excel-2016](http://www.parago.de/blog/2017/01/01/stock-quote-add-in-for-excel-2016)

Installer for MSN-like Stock Quotes Add-In For Excel 2010 and 2007<br>
[http://www.parago.de/blog/2009/01/07/update-installer-for-msn-like-stock-quotes-add-in-for-excel-2007](http://www.parago.de/blog/2009/01/07/update-installer-for-msn-like-stock-quotes-add-in-for-excel-2007)

Stock Quote Add-In For Excel 2007 (CodeProject)<br>
[http://www.codeproject.com/Articles/67082/Stock-Quote-Add-In-For-Excel-2007](http://www.codeproject.com/Articles/67082/Stock-Quote-Add-In-For-Excel-2007)

Creating A MSN-like Stock Quotes Add-In For Excel 2007 Using User-Defined Functions And Ribbons (PDF)<br>
[http://content.parago.de/articles/Excel2007StockQuotesAddIn/CreatingAMSNlikeStockQuotesAddInForExcel2007UsingUserDefinedFunctionsAndRibbons.pdf](http://content.parago.de/articles/Excel2007StockQuotesAddIn/CreatingAMSNlikeStockQuotesAddInForExcel2007UsingUserDefinedFunctionsAndRibbons.pdf)

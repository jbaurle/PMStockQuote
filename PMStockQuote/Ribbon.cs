// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Runtime.InteropServices;
using System.Windows.Forms;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;

namespace PMStockQuote
{
    [ComVisible(true)]
    public class Ribbon : ExcelRibbon
    {
        public void OnUpdateButtonClick(IRibbonControl control)
        {
            var excel = (Microsoft.Office.Interop.Excel.Application)ExcelDnaUtil.Application;

            excel.CalculateFull();
        }

        public void OnSearchButtonClick(IRibbonControl control)
        {
            new SearchForm().ShowDialog();
        }

        public void OnAboutButtonClick(IRibbonControl control)
        {
            // Show Message Boxes with Excel-DNA
            // https://andysprague.com/2017/07/03/show-message-boxes-with-excel-dna/
            MessageBox.Show("To retrieve stock quotes see the following formula samples:\n\n\tA1=NASDAQ:GOOGL or A1=GOOGL\n\n\t=PSQ(A1;\"LAST\") or just =PSQ(A1)\n\t=PSQ(A1;\"OPEN\")\n\t=PSQ(A1;\"LOW\")\n\t=PSQ(A1;\"HIGH\")\n\t=PSQ(A1;\"LOW52\")\n\t=PSQ(A1;\"HIGH52\")\n\t=PSQ(A1;\"VOLUME\")\n\t=PSQ(A1;\"CHANGE\")\n\t=PSQ(A1;\"CHANGEPERCENTAGE\")\n\t=PSQ(A1;\"NAME\")\n\t=PSQ(A1;\"DATE\")\n\t=PSQ(A1;\"TIME\"\n\t=PSQ(A1;\"TICKER\")\n\t=PSQ(A1;\"EXCHANGE\")\n\nDATE and TIME are the timestamp of the last query, not the last trade date. Google Finance API is not returning the this kind of data anymore.\n\nThe PMStockQuote Add-In is developed by Jürgen Bäurle, http://www.parago.de.\n\n", "PMStockQuote Add-In", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
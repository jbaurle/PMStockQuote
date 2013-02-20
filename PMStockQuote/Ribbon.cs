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

		public void OnAboutButtonClick(IRibbonControl control)
		{
			MessageBox.Show("To retrieve stock quotes see the following formula samples:\n\n\t=PSQ(A1;\"PRICE\") or just =PSQ(A1)\n\t=PSQ(A1;\"DATE\")\n\t=PSQ(A1;\"TIME\"\n\t=PSQ(A1;\"NAME\")\n\nThe PMStockQuote Add-In is developed by Jürgen Bäurle, http://www.parago.de.\n\n", "PMStockQuote Add-In", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}

using System;
using ExcelDna.Integration;

namespace PMStockQuote
{
	public class AddIn : IExcelAddIn
	{
		public void AutoOpen()
		{
			ExcelAsyncUtil.Initialize();

			ExcelIntegration.RegisterUnhandledExceptionHandler(e => "ERROR: " + (e as Exception).Message);

			//var excel = (Microsoft.Office.Interop.Excel.Application)ExcelDnaUtil.Application;
			//var xllPath = (string)XlCall.Excel(XlCall.xlGetName);
			//excel.AddIns.Add(xllPath, false /* don't copy file */).Installed = true;
		}

		public void AutoClose()
		{
			ExcelAsyncUtil.Uninitialize();
		}
	}
}

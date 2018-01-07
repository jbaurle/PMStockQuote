// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Windows.Forms;

namespace PMStockQuote
{
    public partial class JsonForm : Form
    {
        #region Fields

        string _ticker;

        #endregion

        public JsonForm(string ticker)
        {
            Execute(() =>
            {
                InitializeComponent();

                NativeMethods.SetTextBoxPadding(UrlBox, new Padding(6, 3, 6, 3));

                _ticker = string.IsNullOrWhiteSpace(ticker) ? "?" : ticker;

                Text = string.Format(Constants.JsonFormTitle, _ticker);

                UrlBox.Text = string.Format(Constants.GoogleFinanceUrl, ticker);
            });
        }

        void OnLoad(object sender, EventArgs e)
        {
            Execute(() =>
            {
                JsonBox.Text = _ticker == "?" ? string.Empty : GoogleFinanceHelper.GetTickerData(_ticker);
                JsonBox.SelectionStart = 0;
                JsonBox.SelectionLength = 0;
                JsonBox.Focus();
            });
        }

        void OnResize(object sender, EventArgs e)
        {
            Execute(() =>
            {
                NativeMethods.SetTextBoxPadding(UrlBox, new Padding(6, 3, 6, 3));
            });
        }

        void OnCloseButtonClick(object sender, EventArgs e)
        {
            Execute(() =>
            {
                Close();
            });
        }

        #region Helper Methods

        void Execute(Action action, Action finalAction = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occuried during processing the request.\n\nError:\n{e.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                finalAction?.Invoke();
            }
        }

        #endregion
    }
}
// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Windows.Forms;

namespace PMStockQuote
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            Execute(() =>
            {
                InitializeComponent();

                NativeMethods.SetTextBoxPadding(SearchBox, new Padding(6, 3, 6, 3));

                Text = Constants.SearchFormTitle;

                SearchBox.SelectAll();
                SearchBox.Focus();
            });
        }

        void OnResize(object sender, EventArgs e)
        {
            Execute(() =>
            {
                NativeMethods.SetTextBoxPadding(SearchBox, new Padding(6, 3, 6, 3));
            });
        }

        void OnSearchBoxTextChanged(object sender, EventArgs e)
        {
            Execute(() =>
            {
                SearchButton.Enabled = SearchBox.Text.Length > 0;
            });
        }

        void OnSearchButtonClick(object sender, EventArgs e)
        {
            Execute(() =>
            {
                SearchBox.Enabled = false;
                SearchButton.Enabled = false;

                SearchResultsGrid.Rows.Clear();

                // TODO: May add query asnyc?
                var results = GoogleFinanceHelper.SearchTicker(SearchBox.Text);

                foreach (var item in results)
                    SearchResultsGrid.Rows.Add(item.Name, item.Symbol, item.Exchange);

                if (SearchResultsGrid.Rows.Count > 0)
                {
                    SearchResultsGrid.Select();
                    SearchResultsGrid.Focus();
                }
            }, () =>
            {
                SearchBox.Enabled = true;
                SearchButton.Enabled = true;

                if (SearchResultsGrid.Rows.Count == 0)
                {
                    SearchBox.SelectAll();
                    SearchBox.Focus();
                }
            });
        }

        void OnTickerDataButtonClick(object sender, EventArgs e)
        {
            Execute(() =>
            {
                if (SearchResultsGrid.SelectedRows.Count > 0)
                {
                    var ticker = SearchResultsGrid.SelectedRows[0].Cells[1].Value as string ?? string.Empty;
                    var exchange = SearchResultsGrid.SelectedRows[0].Cells[2].Value as string ?? string.Empty;

                    new JsonForm($"{exchange}:{ticker}").ShowDialog();
                }
            });
        }

        void OnCopyToClipboardButtonClick(object sender, EventArgs e)
        {
            Execute(() =>
            {
                if (SearchResultsGrid.SelectedRows.Count > 0)
                {
                    var ticker = SearchResultsGrid.SelectedRows[0].Cells[1].Value as string ?? string.Empty;
                    var exchange = SearchResultsGrid.SelectedRows[0].Cells[2].Value as string ?? string.Empty;

                    Clipboard.SetText($"{exchange}:{ticker}");
                }
            });
        }

        void OnSearchResultsGridSelectionChanged(object sender, EventArgs e)
        {
            Execute(() =>
            {
                TickerDataButton.Enabled = SearchResultsGrid.SelectedRows.Count > 0;
                CopyToClipboardButton.Enabled = SearchResultsGrid.SelectedRows.Count > 0;
            });
        }

        void OnSearchResultsGridDoubleClick(object sender, EventArgs e)
        {
            Execute(() =>
            {
                CopyToClipboardButton.PerformClick();
                CloseButton.PerformClick();
            });
        }

        void OnSearchResultsGridKeyDown(object sender, KeyEventArgs e)
        {
            Execute(() =>
            {
                if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
                {
                    CopyToClipboardButton.PerformClick();
                    CloseButton.PerformClick();
                }
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
                MessageBox.Show($"An error occuried during processing the request.\n\nError:\n{e.Message}", Constants.SearchFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                finalAction?.Invoke();
            }
        }

        #endregion
    }
}
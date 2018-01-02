namespace PMStockQuote
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchResultsGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TickerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExchangeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CopyToClipboardButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Location = new System.Drawing.Point(12, 12);
            this.SearchBox.Multiline = true;
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(479, 24);
            this.SearchBox.TabIndex = 0;
            this.SearchBox.TextChanged += new System.EventHandler(this.OnSearchBoxTextChanged);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(497, 12);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 24);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.OnSearchButtonClick);
            // 
            // SearchResultsGrid
            // 
            this.SearchResultsGrid.AllowUserToAddRows = false;
            this.SearchResultsGrid.AllowUserToDeleteRows = false;
            this.SearchResultsGrid.AllowUserToResizeColumns = false;
            this.SearchResultsGrid.AllowUserToResizeRows = false;
            this.SearchResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.TickerColumn,
            this.ExchangeColumn});
            this.SearchResultsGrid.Location = new System.Drawing.Point(12, 54);
            this.SearchResultsGrid.MultiSelect = false;
            this.SearchResultsGrid.Name = "SearchResultsGrid";
            this.SearchResultsGrid.ReadOnly = true;
            this.SearchResultsGrid.RowHeadersVisible = false;
            this.SearchResultsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SearchResultsGrid.Size = new System.Drawing.Size(560, 263);
            this.SearchResultsGrid.StandardTab = true;
            this.SearchResultsGrid.TabIndex = 4;
            this.SearchResultsGrid.SelectionChanged += new System.EventHandler(this.OnSearchResultsGridSelectionChanged);
            this.SearchResultsGrid.DoubleClick += new System.EventHandler(this.OnSearchResultsGridDoubleClick);
            this.SearchResultsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchResultsGridKeyDown);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 100;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // TickerColumn
            // 
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TickerColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.TickerColumn.HeaderText = "Ticker";
            this.TickerColumn.MinimumWidth = 70;
            this.TickerColumn.Name = "TickerColumn";
            this.TickerColumn.ReadOnly = true;
            // 
            // ExchangeColumn
            // 
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ExchangeColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ExchangeColumn.HeaderText = "Exchange";
            this.ExchangeColumn.Name = "ExchangeColumn";
            this.ExchangeColumn.ReadOnly = true;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 323);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(317, 32);
            this.DescriptionLabel.TabIndex = 5;
            this.DescriptionLabel.Text = "A maximum of 100 results are shown. Double-click on row or press Enter to copy ti" +
    "cker to clipboard and close dialog.";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(497, 323);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 24);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.OnCloseButtonClick);
            // 
            // CopyToClipboardButton
            // 
            this.CopyToClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyToClipboardButton.Enabled = false;
            this.CopyToClipboardButton.Location = new System.Drawing.Point(381, 323);
            this.CopyToClipboardButton.Name = "CopyToClipboardButton";
            this.CopyToClipboardButton.Size = new System.Drawing.Size(110, 24);
            this.CopyToClipboardButton.TabIndex = 7;
            this.CopyToClipboardButton.TabStop = false;
            this.CopyToClipboardButton.Text = "Copy to Clipboard";
            this.CopyToClipboardButton.UseVisualStyleBackColor = true;
            this.CopyToClipboardButton.Click += new System.EventHandler(this.OnCopyToClipboardButtonClick);
            // 
            // SearchForm
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.CopyToClipboardButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.SearchResultsGrid);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 250);
            this.Name = "SearchForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Resize += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.DataGridView SearchResultsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TickerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExchangeColumn;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button CopyToClipboardButton;
    }
}
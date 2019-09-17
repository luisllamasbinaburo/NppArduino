namespace Kbg.NppPluginNET
{
    partial class frmMain
    {

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.btRefresh = new System.Windows.Forms.Button();
            this.btCompile = new System.Windows.Forms.Button();
            this.btUpload = new System.Windows.Forms.Button();
            this.cbComPorts = new System.Windows.Forms.ComboBox();
            this.cbBoards = new System.Windows.Forms.ComboBox();
            this.txOutput = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txSerialPort = new System.Windows.Forms.RichTextBox();
            this.txSendText = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.cbBaudRates = new System.Windows.Forms.ComboBox();
            this.gvProperties = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(12, 330);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            // 
            // btCompile
            // 
            this.btCompile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCompile.Location = new System.Drawing.Point(172, 330);
            this.btCompile.Name = "btCompile";
            this.btCompile.Size = new System.Drawing.Size(75, 23);
            this.btCompile.TabIndex = 3;
            this.btCompile.Text = "Compile";
            this.btCompile.UseVisualStyleBackColor = true;
            // 
            // btUpload
            // 
            this.btUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpload.Location = new System.Drawing.Point(296, 330);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(75, 23);
            this.btUpload.TabIndex = 4;
            this.btUpload.Text = "Upload";
            this.btUpload.UseVisualStyleBackColor = true;
            // 
            // cbComPorts
            // 
            this.cbComPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComPorts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbComPorts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbComPorts.FormattingEnabled = true;
            this.cbComPorts.Location = new System.Drawing.Point(57, 14);
            this.cbComPorts.Name = "cbComPorts";
            this.cbComPorts.Size = new System.Drawing.Size(314, 21);
            this.cbComPorts.TabIndex = 2;
            // 
            // cbBoards
            // 
            this.cbBoards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoards.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbBoards.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbBoards.FormattingEnabled = true;
            this.cbBoards.Location = new System.Drawing.Point(57, 41);
            this.cbBoards.Name = "cbBoards";
            this.cbBoards.Size = new System.Drawing.Size(314, 21);
            this.cbBoards.TabIndex = 2;
            // 
            // txOutput
            // 
            this.txOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txOutput.Location = new System.Drawing.Point(11, 359);
            this.txOutput.Name = "txOutput";
            this.txOutput.Size = new System.Drawing.Size(359, 196);
            this.txOutput.TabIndex = 99;
            this.txOutput.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Boards:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Ports:";
            // 
            // txSerialPort
            // 
            this.txSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txSerialPort.Location = new System.Drawing.Point(12, 588);
            this.txSerialPort.Name = "txSerialPort";
            this.txSerialPort.Size = new System.Drawing.Size(359, 155);
            this.txSerialPort.TabIndex = 102;
            this.txSerialPort.Text = "";
            // 
            // txSendText
            // 
            this.txSendText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txSendText.Location = new System.Drawing.Point(12, 750);
            this.txSendText.Name = "txSendText";
            this.txSendText.Size = new System.Drawing.Size(271, 20);
            this.txSendText.TabIndex = 103;
            // 
            // btSend
            // 
            this.btSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSend.Location = new System.Drawing.Point(296, 747);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 23);
            this.btSend.TabIndex = 104;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 564);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Serial port:";
            // 
            // btConnect
            // 
            this.btConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btConnect.Location = new System.Drawing.Point(296, 559);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 106;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            // 
            // cbBaudRates
            // 
            this.cbBaudRates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBaudRates.FormattingEnabled = true;
            this.cbBaudRates.Location = new System.Drawing.Point(93, 561);
            this.cbBaudRates.Name = "cbBaudRates";
            this.cbBaudRates.Size = new System.Drawing.Size(190, 21);
            this.cbBaudRates.TabIndex = 108;
            // 
            // gvProperties
            // 
            this.gvProperties.AllowUserToAddRows = false;
            this.gvProperties.AllowUserToDeleteRows = false;
            this.gvProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Value});
            this.gvProperties.Location = new System.Drawing.Point(57, 68);
            this.gvProperties.Name = "gvProperties";
            this.gvProperties.RowHeadersVisible = false;
            this.gvProperties.ShowEditingIcon = false;
            this.gvProperties.ShowRowErrors = false;
            this.gvProperties.Size = new System.Drawing.Size(314, 243);
            this.gvProperties.TabIndex = 111;
            // 
            // Property
            // 
            this.Property.DataPropertyName = "Option_Label";
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 200;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "Optinos:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 789);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gvProperties);
            this.Controls.Add(this.cbBaudRates);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.txSendText);
            this.Controls.Add(this.txSerialPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbComPorts);
            this.Controls.Add(this.cbBoards);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.btCompile);
            this.Controls.Add(this.btUpload);
            this.Controls.Add(this.txOutput);
            this.Name = "frmMain";
            this.Text = "NppArduino";
            ((System.ComponentModel.ISupportInitialize)(this.gvProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txOutput;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btCompile;
        private System.Windows.Forms.Button btUpload;
        private System.Windows.Forms.ComboBox cbComPorts;
        private System.Windows.Forms.ComboBox cbBoards;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txSerialPort;
        private System.Windows.Forms.TextBox txSendText;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.ComboBox cbBaudRates;
        private System.Windows.Forms.DataGridView gvProperties;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewComboBoxColumn Value;
        private System.Windows.Forms.Label label4;
    }
}
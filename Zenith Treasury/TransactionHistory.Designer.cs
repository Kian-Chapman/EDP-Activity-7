namespace Zenith_Treasury
{
    partial class TransactionHistory
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionHistory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.depositChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.returnLogo = new System.Windows.Forms.PictureBox();
            this.exportButton1 = new System.Windows.Forms.PictureBox();
            this.transactionHistoryGrid = new System.Windows.Forms.DataGridView();
            this.withdrawChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.v = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.depositChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.returnLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.withdrawChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // userIdLabel
            // 
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.Font = new System.Drawing.Font("Kadwa", 19.8F, System.Drawing.FontStyle.Bold);
            this.userIdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(191)))), ((int)(((byte)(2)))));
            this.userIdLabel.Location = new System.Drawing.Point(970, 74);
            this.userIdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(363, 66);
            this.userIdLabel.TabIndex = 0;
            this.userIdLabel.Text = "ID: 2021-8978-12841";
            // 
            // depositChart
            // 
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.WallWidth = 5;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.depositChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.depositChart.Legends.Add(legend1);
            this.depositChart.Location = new System.Drawing.Point(248, 436);
            this.depositChart.Margin = new System.Windows.Forms.Padding(4);
            this.depositChart.Name = "depositChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.depositChart.Series.Add(series1);
            this.depositChart.Size = new System.Drawing.Size(500, 350);
            this.depositChart.TabIndex = 4;
            this.depositChart.Text = "chart2";
            // 
            // returnLogo
            // 
            this.returnLogo.Image = ((System.Drawing.Image)(resources.GetObject("returnLogo.Image")));
            this.returnLogo.Location = new System.Drawing.Point(136, 74);
            this.returnLogo.Name = "returnLogo";
            this.returnLogo.Size = new System.Drawing.Size(370, 100);
            this.returnLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.returnLogo.TabIndex = 28;
            this.returnLogo.TabStop = false;
            this.returnLogo.Click += new System.EventHandler(this.returnLogo_Click);
            // 
            // exportButton1
            // 
            this.exportButton1.Image = ((System.Drawing.Image)(resources.GetObject("exportButton1.Image")));
            this.exportButton1.Location = new System.Drawing.Point(1215, 328);
            this.exportButton1.Name = "exportButton1";
            this.exportButton1.Size = new System.Drawing.Size(118, 68);
            this.exportButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exportButton1.TabIndex = 29;
            this.exportButton1.TabStop = false;
            this.exportButton1.Click += new System.EventHandler(this.exportButton1_Click);
            // 
            // transactionHistoryGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Inter", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionHistoryGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.transactionHistoryGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.transactionHistoryGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.transactionHistoryGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(8)))), ((int)(((byte)(176)))));
            this.transactionHistoryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transactionHistoryGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Kadwa", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.transactionHistoryGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.transactionHistoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Inter", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.transactionHistoryGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.transactionHistoryGrid.EnableHeadersVisualStyles = false;
            this.transactionHistoryGrid.Location = new System.Drawing.Point(248, 246);
            this.transactionHistoryGrid.Margin = new System.Windows.Forms.Padding(4);
            this.transactionHistoryGrid.Name = "transactionHistoryGrid";
            this.transactionHistoryGrid.ReadOnly = true;
            this.transactionHistoryGrid.RowHeadersVisible = false;
            this.transactionHistoryGrid.RowHeadersWidth = 51;
            this.transactionHistoryGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.transactionHistoryGrid.Size = new System.Drawing.Size(783, 150);
            this.transactionHistoryGrid.TabIndex = 39;
            // 
            // withdrawChart
            // 
            chartArea2.Area3DStyle.Enable3D = true;
            chartArea2.Area3DStyle.WallWidth = 5;
            chartArea2.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.Name = "ChartArea1";
            this.withdrawChart.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.withdrawChart.Legends.Add(legend2);
            this.withdrawChart.Location = new System.Drawing.Point(833, 436);
            this.withdrawChart.Margin = new System.Windows.Forms.Padding(4);
            this.withdrawChart.Name = "withdrawChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.withdrawChart.Series.Add(series2);
            this.withdrawChart.Size = new System.Drawing.Size(500, 350);
            this.withdrawChart.TabIndex = 40;
            this.withdrawChart.Text = "chart2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(58, -5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 876);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // v
            // 
            this.v.Image = ((System.Drawing.Image)(resources.GetObject("v.Image")));
            this.v.Location = new System.Drawing.Point(-4, 180);
            this.v.Name = "v";
            this.v.Size = new System.Drawing.Size(1485, 10);
            this.v.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.v.TabIndex = 49;
            this.v.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(-4, 58);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1485, 10);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 52;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(120, -5);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(10, 876);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 53;
            this.pictureBox5.TabStop = false;
            // 
            // TransactionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(8)))), ((int)(((byte)(176)))));
            this.ClientSize = new System.Drawing.Size(1482, 853);
            this.Controls.Add(this.returnLogo);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.v);
            this.Controls.Add(this.withdrawChart);
            this.Controls.Add(this.transactionHistoryGrid);
            this.Controls.Add(this.depositChart);
            this.Controls.Add(this.userIdLabel);
            this.Controls.Add(this.exportButton1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TransactionHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZENITH";
            ((System.ComponentModel.ISupportInitialize)(this.depositChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.returnLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exportButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionHistoryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.withdrawChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.v)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart depositChart;
        private System.Windows.Forms.PictureBox returnLogo;
        private System.Windows.Forms.PictureBox exportButton1;
        private System.Windows.Forms.DataGridView transactionHistoryGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart withdrawChart;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox v;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}
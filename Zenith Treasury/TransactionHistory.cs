using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Zenith_Treasury
{
    public partial class TransactionHistory : Form
    {
        private SubordinateFunction subordinateFunction;

        public TransactionHistory()
        {
            InitializeComponent();
            subordinateFunction = new SubordinateFunction();
            userIdLabel.Text = "ID: " + SubordinateFunction.CurrentUserID;

            // Define columns for the DataGridView
            transactionHistoryGrid.Columns.Add("TransactionID", "Transaction ID");
            transactionHistoryGrid.Columns.Add("TransactionType", "Transaction Type");
            transactionHistoryGrid.Columns.Add("Amount", "Amount");
            transactionHistoryGrid.Columns.Add("TransactionDate", "Transaction Date");

            PopulateTransactionGrid();
            PopulateChart();
        }

        private void PopulateTransactionGrid()
        {
            // Clear existing rows
            transactionHistoryGrid.Rows.Clear();

            // Retrieve transactions for the current user
            List<Tuple<int, string, decimal, DateTime>> transactions = subordinateFunction.GetUserTransactionsWithID();

            // Populate the grid with transaction information
            foreach (var transaction in transactions)
            {
                // Format the date to only display the date portion without the time
                string transactionDate = transaction.Item4.ToString("dd/MM/yyyy");

                // Add the transaction to the grid with formatted date
                transactionHistoryGrid.Rows.Add(transaction.Item1, transaction.Item2, transaction.Item3, transactionDate);
            }
        }


        private void PopulateChart()
        {
            try
            {
                // Retrieve transactions for the current user
                List<Tuple<int, string, decimal, DateTime>> transactions = subordinateFunction.GetUserTransactionsWithID();

                // Check if there are no transactions
                if (transactions.Count == 0)
                {
                    MessageBox.Show("No transactions found.", "No Transactions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Group transactions by date and calculate the total amount for each date
                var withdrawTransactions = transactions.Where(t => t.Item2 == "Withdrawal")
                                                        .GroupBy(t => t.Item4.Date)
                                                        .Select(g => new { Date = g.Key, TotalAmount = g.Sum(t => t.Item3) })
                                                        .OrderBy(t => t.Date);

                var depositTransactions = transactions.Where(t => t.Item2 == "Deposit")
                                                        .GroupBy(t => t.Item4.Date)
                                                        .Select(g => new { Date = g.Key, TotalAmount = g.Sum(t => t.Item3) })
                                                        .OrderBy(t => t.Date);

                // Clear any existing series in the charts
                withdrawChart.Series.Clear();
                depositChart.Series.Clear();

                // Populate the withdrawal chart if there are withdrawal transactions
                if (withdrawTransactions.Any())
                {
                    PopulateChartSeries(withdrawChart, withdrawTransactions, "Withdraw", Color.Red);
                }
                else
                {
                    // If no withdrawal transactions, display a message
                    MessageBox.Show("No withdrawal transactions found.", "No Withdrawal Transactions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Populate the deposit chart if there are deposit transactions
                if (depositTransactions.Any())
                {
                    PopulateChartSeries(depositChart, depositTransactions, "Deposit", Color.Blue);
                }
                else
                {
                    // If no deposit transactions, display a message
                    MessageBox.Show("No deposit transactions found.", "No Deposit Transactions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
              
            }
        }

        private void PopulateChartSeries(Chart chart, IEnumerable<dynamic> transactions, string chartTitle, Color color)
        {
            // Calculate the maximum total amount among all transactions
            decimal maxTotalAmount = transactions.Max(t => t.TotalAmount);

            // Add data points to the chart
            Series series = new Series();
            series.ChartType = SeriesChartType.Bar; // Change chart type to bar
            series.Color = color; // Set color

            foreach (var transaction in transactions)
            {
                series.Points.AddXY(transaction.Date, transaction.TotalAmount);
            }

            chart.Series.Add(series);

            // Set the maximum value of the Y-axis to the maximum total amount
            chart.ChartAreas[0].AxisY.Maximum = (double)maxTotalAmount;

            // Set chart titles and axis labels
            chart.Titles.Add(chartTitle);
            chart.ChartAreas[0].AxisX.Title = "Transaction Date";
            chart.ChartAreas[0].AxisY.Title = "Amount";

            // Set X-axis date labels format to display month in two digits
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yy";

            // Set font for titles to "Kadwa"
            chart.Titles[0].Font = new Font("Kadwa", chart.Titles[0].Font.Size, FontStyle.Bold);

            // Set font for other texts to "Inter"
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Inter", chart.ChartAreas[0].AxisX.LabelStyle.Font.Size);
            chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Inter", chart.ChartAreas[0].AxisY.LabelStyle.Font.Size);
        }









        private void returnLogo_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu = new Main_Menu();
            main_Menu.Show();
            this.Close();
        }

        private void exportButton1_Click(object sender, EventArgs e)
        {
            subordinateFunction.ExportToExcel(transactionHistoryGrid, withdrawChart, depositChart, "Receipt");
        }
    }
}
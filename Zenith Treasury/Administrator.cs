using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Zenith_Treasury
{
    public partial class Administrator : Form
    {
        private SubordinateFunction subordinateFunction;

        public Administrator(string admin)
        {
            InitializeComponent();
            subordinateFunction = new SubordinateFunction();
            adminLabel.Text = "Welcome " + admin + "!";
            DefineTransactionGridColumns();
            DefineInitialDepositGridColumns();
            PopulateTransactionsGrid();
            PopulateInitialDepositGrid();
            CreateTransactionChart();
            CreateInitialDepositChart();
        }

        private void DefineTransactionGridColumns()
        {
            transactionsGrid.Columns.Add("TransactionType", "Transaction Type");
            transactionsGrid.Columns.Add("Amount", "Amount");
        }

        private void DefineInitialDepositGridColumns()
        {
            initialDepositGrid.Columns.Add("UserID", "User ID");
            initialDepositGrid.Columns.Add("InitialDeposit", "Initial Deposit");
        }


        private void PopulateTransactionsGrid()
        {
            List<Tuple<string, decimal>> transactions = subordinateFunction.GetAllTransactions();
            foreach (var transaction in transactions)
            {
                transactionsGrid.Rows.Add(transaction.Item1, transaction.Item2);
            }
        }

        private void PopulateInitialDepositGrid()
        {
            List<Tuple<string, decimal>> initialDeposits = subordinateFunction.GetAllInitialDeposits();
            foreach (var deposit in initialDeposits)
            {
                initialDepositGrid.Rows.Add(deposit.Item1, deposit.Item2);
            }
        }

        private void CreateTransactionChart()
        {
            transactionsChart.Series.Clear();

            // Add "Withdrawals" series
            Series withdrawalSeries = transactionsChart.Series.Add("Withdrawals");
            withdrawalSeries.ChartType = SeriesChartType.Bar;
            withdrawalSeries.Color = System.Drawing.Color.Red; // Set color to red

            // Add "Deposits" series
            Series depositSeries = transactionsChart.Series.Add("Deposits");
            depositSeries.ChartType = SeriesChartType.Bar;
            depositSeries.Color = System.Drawing.Color.Blue; // Set color to blue

            // Set chart titles
            transactionsChart.ChartAreas[0].AxisY.Title = "Amount"; // Set y-axis title

            List<Tuple<string, decimal>> transactions = subordinateFunction.GetAllTransactions();
            decimal totalWithdrawals = 0;
            decimal totalDeposits = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.Item1 == "Withdrawal")
                {
                    totalWithdrawals += transaction.Item2;
                }
                else if (transaction.Item1 == "Deposit")
                {
                    totalDeposits += transaction.Item2;
                }
            }

            // Add data points to the series
            withdrawalSeries.Points.AddXY("Transactions", totalWithdrawals);
            depositSeries.Points.AddXY("Amount", totalDeposits);
        }



        private void CreateInitialDepositChart()
        {
            initialDepositChart.Series.Clear();
            initialDepositChart.Series.Add("Initial Deposits");
            initialDepositChart.Series["Initial Deposits"].ChartType = SeriesChartType.Bar;

            initialDepositChart.ChartAreas[0].AxisY.Title = "Amount"; // Set y-axis title

            List<Tuple<string, decimal>> deposits = subordinateFunction.GetAllInitialDeposits();
            foreach (var deposit in deposits)
            {
                initialDepositChart.Series["Initial Deposits"].Points.AddXY(deposit.Item1, deposit.Item2);
            }
        }


        private void export1_Click(object sender, EventArgs e)
        {
            subordinateFunction.ExportToExcel(transactionsGrid, transactionsChart, null, "Zenith Transaction");
        }

        private void export2_Click(object sender, EventArgs e)
        {
            subordinateFunction.ExportToExcel(initialDepositGrid, initialDepositChart, null, "Zenith Initial Deposit");
        }

        private void returnLogo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Admin account logged out!");
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void logoutB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Admin account logged out!");
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}

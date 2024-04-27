using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class Main_Menu : Form
    {
        private SubordinateFunction SubordinateFunction;

        public Main_Menu()
        {
            InitializeComponent();
            SubordinateFunction = new SubordinateFunction();
            idLabel.Text = "ID: " + SubordinateFunction.CurrentUserID;
        }

        private void balanceB_Click(object sender, EventArgs e)
        {
            Balance balance = new Balance();
            this.Hide();
            balance.Show();
        }

        private void depositB_Click(object sender, EventArgs e)
        {
            Deposit deposit = new Deposit();
            this.Hide();
            deposit.Show();
        }

        private void withdrawB_Click(object sender, EventArgs e)
        {
            Withdraw withdraw = new Withdraw();
            this.Hide();
            withdraw.Show();
        }

        private void transactB_Click(object sender, EventArgs e)
        {
            TransactionHistory transactionHistory = new TransactionHistory();
            this.Hide();
            transactionHistory.Show();
        }

        private void logoutB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Account successfully logged out!");
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}

using System;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class Withdraw : Form
    {
        private decimal currentUserBalance;
        private SubordinateFunction subordinateFunction;

        public Withdraw()
        {
            InitializeComponent();
            subordinateFunction = new SubordinateFunction();
            UpdateCurrentUserAndBalance(); // Update the current user and balance
        }

        // Method to update currentUserBalance
        private void UpdateCurrentUserAndBalance()
        {
            if (!string.IsNullOrEmpty(SubordinateFunction.CurrentUserID))
            {
                // Get current user's balance
                currentUserBalance = subordinateFunction.GetUserBalance(SubordinateFunction.CurrentUserID);
            }
        }


        private void returnLogo_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu = new Main_Menu();
            main_Menu.Show();
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            // Check if a user is logged in
            if (!string.IsNullOrEmpty(SubordinateFunction.CurrentUserID))
            {
                // Check if PIN is correct
                string pin = pinBox.Text;

                // Use SubordinateFunction instance and its methods
                if (subordinateFunction.Login(SubordinateFunction.CurrentUserID, pin))
                {
                    // PIN is correct, proceed with withdrawal
                    decimal withdrawalAmount;
                    if (!decimal.TryParse(amountBox.Text, out withdrawalAmount))
                    {
                        MessageBox.Show("Invalid withdrawal amount.");
                        return;
                    }

                    if (withdrawalAmount > currentUserBalance)
                    {
                        MessageBox.Show("Insufficient funds.");
                        return;
                    }

                    // Perform withdrawal transaction
                    string transactionType = "Withdrawal";
                    if (subordinateFunction.PerformTransaction(SubordinateFunction.CurrentUserID, transactionType, withdrawalAmount))
                    {
                        // Update currentUserBalance after successful withdrawal
                        currentUserBalance -= withdrawalAmount;

                        // Insert transaction record
                        if (subordinateFunction.InsertTransaction(transactionType, withdrawalAmount))
                        {
                            // Display success message
                            MessageBox.Show("Withdrawal successful!");
                            UpdateCurrentUserAndBalance(); // Update the current user and balance after withdrawal

                            Main_Menu main_Menu = new Main_Menu();
                            main_Menu.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert transaction record.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to process withdrawal. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid PIN.");
                }
            }
            else
            {
                MessageBox.Show("No user logged in.");
            }
        }
    }
}

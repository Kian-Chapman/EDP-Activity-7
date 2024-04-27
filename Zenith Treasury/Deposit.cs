using System;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class Deposit : Form
    {
        private decimal currentUserBalance;
        private SubordinateFunction subordinateFunction;

        public Deposit()
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
                    // PIN is correct, proceed with deposit
                    decimal depositAmount;
                    if (!decimal.TryParse(amountBox.Text, out depositAmount))
                    {
                        MessageBox.Show("Invalid deposit amount.");
                        return;
                    }

                    // Perform deposit transaction
                    string transactionType = "Deposit";
                    if (subordinateFunction.PerformTransaction(SubordinateFunction.CurrentUserID, transactionType, depositAmount))
                    {
                        // Update currentUserBalance after successful deposit
                        currentUserBalance += depositAmount;

                        // Insert transaction record
                        if (subordinateFunction.InsertTransaction(transactionType, depositAmount))
                        {
                            // Display success message
                            MessageBox.Show("Deposit successful!");
                            UpdateCurrentUserAndBalance(); // Update the current user and balance after deposit

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
                        MessageBox.Show("Failed to process deposit. Please try again.");
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

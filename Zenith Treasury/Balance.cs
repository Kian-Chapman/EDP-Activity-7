using System.Collections.Generic;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class Balance : Form
    {
        private SubordinateFunction subordinateFunction;

        public Balance()
        {
            InitializeComponent();
            subordinateFunction = new SubordinateFunction();
            PopulateUserProfile(); // Populate user profile on form load
        }

        private void PopulateUserProfile()
        {
            // Check if a user is logged in
            if (!string.IsNullOrEmpty(SubordinateFunction.CurrentUserID))
            {
                // Get user details
                Dictionary<string, string> userDetails = subordinateFunction.GetUserDetails(SubordinateFunction.CurrentUserID);

                // Display user details
                if (userDetails.ContainsKey("Name"))
                {
                    nameBox.Text = userDetails["Name"];
                }

                if (userDetails.ContainsKey("Username"))
                {
                    userNameBox.Text = userDetails["Username"];
                }

                if (userDetails.ContainsKey("Contact"))
                {
                    contactBox.Text = userDetails["Contact"];
                }

                // Get user balance
                decimal userBalance = subordinateFunction.GetUserBalance(SubordinateFunction.CurrentUserID);
                balanceLabel.Text = userBalance.ToString("C"); // Display balance in currency format
            }
            else
            {
                MessageBox.Show("No user logged in.");
            }
        }

        private void returnLogo_Click(object sender, System.EventArgs e)
        {
            Main_Menu main_Menu = new Main_Menu();
            main_Menu.Show();
            this.Close();

        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class Login : Form
    {
        private SubordinateFunction Function = new SubordinateFunction();

        public Login()
        {
            InitializeComponent();
        }

        private void signUpLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the SignUp form
            SignUp signUpForm = new SignUp();

            // Show the SignUp form
            signUpForm.Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string identifier = userNameBox.Text; // Use userNameBox for username or accountIDBox for AccountID
            string pin = pinBox.Text;

            // Check if the logged-in user is an admin
            Tuple<bool, string> adminInfo = Function.IsAdmin(identifier, pin);
            if (adminInfo.Item1)
            {
                MessageBox.Show($"Admin {adminInfo.Item2} login successful!");

                // Open the Administrator form
                Administrator adminForm = new Administrator(adminInfo.Item2);
                adminForm.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                // If not admin, proceed with regular login
                bool isLoggedIn = Function.Login(identifier, pin);
                if (isLoggedIn)
                {
                    MessageBox.Show("Login successful!");

                    Main_Menu menu = new Main_Menu();
                    menu.Show();
                    this.Hide(); // Hide the login form
                }
                else
                {
                    MessageBox.Show("Invalid username or PIN.");
                }
            }
        }
    }
}

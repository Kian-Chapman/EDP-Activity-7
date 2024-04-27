using System;
using System.Windows.Forms;

namespace Zenith_Treasury
{
    public partial class SignUp : Form
    {
        SubordinateFunction Function = new SubordinateFunction();

        public SignUp()
        {
            InitializeComponent();
            eye1.Visible = false;
            eye0.Visible = true;
        }

        private void returnLogo_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string identifier = userNameBox.Text; // Use userNameBox for username or accountIDBox for AccountID
            string pin = pinBox.Text;
            string pin2 = pinBox2.Text;
            string name = nameBox.Text;
            string contact = contactBox.Text;
            decimal initialDeposit = 0;


            // Check if any of the input boxes are empty
            if (string.IsNullOrWhiteSpace(identifier) ||
                string.IsNullOrWhiteSpace(pin) ||
                string.IsNullOrWhiteSpace(pin2) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(initialDepositBox.Text))
            {
                MessageBox.Show("Please fill in all the input fields.");
                return; // Exit the method if any box is empty
            }

            try
            {
                initialDeposit = decimal.Parse(initialDepositBox.Text);
            }
            catch
            {
                MessageBox.Show("Initial deposit must be a valid amount.");
                return;
            }

            if (identifier.Contains(" "))
            {
                MessageBox.Show("Please make sure there are no spaces in the username or account ID.");
            }
            else if (pin != pin2)
            {
                MessageBox.Show("Please make sure that the PINs match.");
            }
            else
            {
                // If all conditions are met, sign up the user
                Function.SignUp(identifier, pin, name, contact, initialDeposit); // Pass identifier instead of username
                userNameBox.Clear();
                pinBox.Clear();
                pinBox2.Clear();
                nameBox.Clear();
                contactBox.Clear();
                initialDepositBox.Clear();
                this.Close();
            }
        }

        private void eye1_Click(object sender, EventArgs e)
        {
            eye1.Visible = false;
            pinBox.UseSystemPasswordChar = true;
            pinBox2.UseSystemPasswordChar = true;
            eye0.Visible = true;
        }

        private void eye0_Click(object sender, EventArgs e)
        {
            eye1.Visible = true;
            pinBox.UseSystemPasswordChar = false;
            pinBox2.UseSystemPasswordChar = false;
            eye0.Visible = false;
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Drawing;

namespace Zenith_Treasury
{
    public class SubordinateFunction
    {
        private static string currentUserID;
        private string connectionString = "server=127.0.0.1;uid=root;pwd=;database=bank";

        public static string CurrentUserID
        {
            get { return currentUserID; }
            set { currentUserID = value; }
        }

        // Function to open MySqlConnection connection
        private MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public string GenerateRandomUserID()
        {
            Random random = new Random();

            // Generate random 4-digit and 5-digit numbers
            int random4Digits = random.Next(1000, 9999); // Generate between 1000 and 9999
            int random5Digits = random.Next(10000, 99999); // Generate between 10000 and 99999

            // Get current year
            int currentYear = DateTime.Now.Year;

            // Combine parts to form the UserID
            string userID = $"{currentYear}-{random4Digits}-{random5Digits}";

            return userID;
        }

        public void SignUp(string username, string pin, string name, string contact, decimal initialDeposit)
        {
            // Trim whitespace from the username
            username = username.Trim();

            // Check if PIN is 6 digits and consists only of numbers
            if (pin.Length != 6 || !pin.All(char.IsDigit))
            {
                MessageBox.Show("PIN must be a 6-digit number.");
                return;
            }

            // Generate random UserID
            string userID = GenerateRandomUserID();

            // Modify the SQL query to perform a case-insensitive comparison for the username
            string sqlInsertUser = "INSERT INTO User_Accounts (UserID, Username, PIN, Name, Contact, InitialDeposit) " +
                                    "VALUES (@UserID, @Username, @PIN, @Name, @Contact, @InitialDeposit)";
            string sqlInsertAccount = "INSERT INTO Accounts (UserID, Balance, LastActivityDate) " +
                                        "VALUES (@UserID, @InitialDeposit, CURRENT_DATE())";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlInsertUser, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PIN", pin);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@InitialDeposit", initialDeposit);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Insert into Accounts table
                        using (MySqlCommand accountCommand = new MySqlCommand(sqlInsertAccount, connection))
                        {
                            accountCommand.Parameters.AddWithValue("@UserID", userID);
                            accountCommand.Parameters.AddWithValue("@InitialDeposit", initialDeposit);

                            int accountsRowsAffected = accountCommand.ExecuteNonQuery();
                            if (accountsRowsAffected > 0)
                            {
                                MessageBox.Show("Sign up successful! Your UserID is: " + userID);
                                Login login = new Login();
                                login.Show();
                               
                                SignUp signUp = new SignUp();
                                signUp.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Failed to create account. Please try again.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to sign up. Please try again.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // MySQL error number for duplicate entry
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    SignUp signUp = new SignUp();
                    signUp.Hide();
                    signUp.Show();
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public bool Login(string identifier, string pin)
        {
            string sqlSelect = "SELECT UserID FROM User_Accounts WHERE (Username = @Identifier OR UserID = @Identifier) AND PIN = @PIN";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelect, connection))
                {
                    command.Parameters.AddWithValue("@Identifier", identifier);
                    command.Parameters.AddWithValue("@PIN", pin);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        CurrentUserID = result.ToString(); // Store the current user ID
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or PIN.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public Tuple<bool, string> IsAdmin(string identifier, string pin)
        {
            string sqlSelectAdmin = "SELECT COUNT(*), UserID, Name FROM Administrator WHERE Username = @Username AND Password = @Password";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectAdmin, connection))
                {
                    command.Parameters.AddWithValue("@Username", identifier);
                    command.Parameters.AddWithValue("@Password", pin);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            string adminName = reader["Name"].ToString();
                            if (count > 0)
                            {
                                CurrentUserID = reader["UserID"].ToString(); // Store the admin ID
                            }
                            return Tuple.Create(count > 0, adminName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking admin credentials: " + ex.Message);
            }

            currentUserID = null;

            return Tuple.Create(false, "");
        }


        public bool PerformTransaction(string userID, string transactionType, decimal amount)
        {
            // Define SQL query to update balance based on transaction type
            string sqlUpdateBalance = "";
            if (transactionType == "Withdrawal")
            {
                sqlUpdateBalance = "UPDATE Accounts SET Balance = Balance - @Amount WHERE UserID = @UserID";
            }
            else if (transactionType == "Deposit")
            {
                sqlUpdateBalance = "UPDATE Accounts SET Balance = Balance + @Amount WHERE UserID = @UserID";
            }
            else
            {
                MessageBox.Show("Invalid transaction type.");
                return false;
            }

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlUpdateBalance, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Amount", amount);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // TransactionHistory successful
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to process transaction.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public decimal GetUserBalance(string userID)
        {
            string sqlSelectBalance = "SELECT Balance FROM Accounts WHERE UserID = @UserID";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectBalance, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve user balance.");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return 0;
            }
        }

        public bool InsertTransaction(string transactionType, decimal amount)
        {
            try
            {
                // Retrieve the AccountID based on the CurrentUserID
                string accountIDQuery = "SELECT AccountID FROM Accounts WHERE UserID = @UserID";
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(accountIDQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", CurrentUserID);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string accountID = result.ToString();

                        // Proceed with inserting the transaction
                        string insertTransactionQuery = "INSERT INTO Transactions (AccountID, TransactionType, Amount) VALUES (@AccountID, @TransactionType, @Amount)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertTransactionQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@AccountID", accountID);
                            insertCommand.Parameters.AddWithValue("@TransactionType", transactionType);
                            insertCommand.Parameters.AddWithValue("@Amount", amount);
                            int rowsAffected = insertCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // TransactionHistory record inserted successfully
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Failed to insert transaction record.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve AccountID for the current user.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting transaction: " + ex.Message);
                return false;
            }
        }

        public Dictionary<string, string> GetUserDetails(string userID)
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>();

            string sqlSelectUser = "SELECT Username, Name, Contact FROM User_Accounts WHERE UserID = @UserID";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectUser, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDetails["Username"] = reader["Username"].ToString();
                            userDetails["Name"] = reader["Name"].ToString();
                            userDetails["Contact"] = reader["Contact"] != DBNull.Value ? reader["Contact"].ToString() : ""; // Handling null value
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return userDetails;
        }


        public List<Tuple<int, string, decimal, DateTime>> GetUserTransactionsWithID()
        {
            List<Tuple<int, string, decimal, DateTime>> userTransactions = new List<Tuple<int, string, decimal, DateTime>>();

            string sqlSelectTransactions = "SELECT TransactionID, TransactionType, Amount, TransactionDate " +
                                           "FROM Transactions " +
                                           "INNER JOIN Accounts ON Transactions.AccountID = Accounts.AccountID " +
                                           "WHERE Accounts.UserID = @UserID";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectTransactions, connection))
                {
                    command.Parameters.AddWithValue("@UserID", CurrentUserID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int transactionID = Convert.ToInt32(reader["TransactionID"]);
                            string transactionType = reader["TransactionType"].ToString();
                            decimal amount = Convert.ToDecimal(reader["Amount"]);
                            DateTime transactionDate = Convert.ToDateTime(reader["TransactionDate"]);

                            userTransactions.Add(new Tuple<int, string, decimal, DateTime>(transactionID, transactionType, amount, transactionDate));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return userTransactions;
        }

        public List<Tuple<string, decimal>> GetAllTransactions()
        {
            List<Tuple<string, decimal>> allTransactions = new List<Tuple<string, decimal>>();

            string sqlSelectTransactions = "SELECT TransactionType, Amount FROM Transactions";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectTransactions, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string transactionType = reader["TransactionType"].ToString();
                            decimal amount = Convert.ToDecimal(reader["Amount"]);

                            allTransactions.Add(new Tuple<string, decimal>(transactionType, amount));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return allTransactions;
        }

        public List<Tuple<string, decimal>> GetAllInitialDeposits()
        {
            List<Tuple<string, decimal>> allInitialDeposits = new List<Tuple<string, decimal>>();

            string sqlSelectInitialDeposits = "SELECT UserID, InitialDeposit FROM User_Accounts";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                using (MySqlCommand command = new MySqlCommand(sqlSelectInitialDeposits, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userID = reader["UserID"].ToString();
                            decimal initialDeposit = Convert.ToDecimal(reader["InitialDeposit"]);

                            allInitialDeposits.Add(new Tuple<string, decimal>(userID, initialDeposit));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return allInitialDeposits;
        }


        public void ExportToExcel(DataGridView dataGridView, Chart chart1, Chart chart2, string sheetName)
        {
            try
            {
                // Create a new Excel application
                Excel.Application excelApp = new Excel.Application();

                // Create a new workbook
                Excel.Workbook workbook = excelApp.Workbooks.Add();

                // Export Chart1 to Sheet 2 if chart1 is not null
                if (chart1 != null)
                {
                    ExportChartToSheet(chart1, workbook, sheetName + " Chart 1", "Chart1");
                }

                // Export Chart2 to Sheet 2 if chart2 is not null
                if (chart2 != null)
                {
                    ExportChartToSheet(chart2, workbook, sheetName + " Chart 2", "Chart2");
                }

                // Export DataGridView data to specified sheet
                Excel.Worksheet dataSheet = ExportDataGridViewToSheet(dataGridView, workbook, sheetName + " Data");

                // Insert logo into the first row of the data sheet
                string logoPath = @"C:\Users\User\Pictures\_\logos.png";
                InsertLogoToSheet(workbook, dataSheet, logoPath, 1, 1);

                // Save the Excel file without prompt
                string excelFilePath = Path.Combine(@"C:\Users\User\Music", $"{sheetName}.xlsx");

                // Save the Excel file
                workbook.SaveAs(excelFilePath, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlUserResolution, true, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(); // Close the workbook

                MessageBox.Show("Data exported successfully!", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertLogoToSheet(Excel.Workbook workbook, Excel.Worksheet worksheet, string logoPath, int row, int column)
        {
            if (worksheet != null)
            {
                // Get the original width and height of the image
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(logoPath))
                {
   
                    float adjustedWidth = 250;
                    float adjustedHeight = 70;

                    // Insert the image at the specified location
                    worksheet.Shapes.AddPicture(logoPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, worksheet.Cells[row, column].Left, worksheet.Cells[row, column].Top, adjustedWidth, adjustedHeight);
                }
            }
            else
            {
                MessageBox.Show("Error: Worksheet is null", "Worksheet Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Excel.Worksheet ExportDataGridViewToSheet(DataGridView dataGridView, Excel.Workbook workbook, string sheetName)
        {
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets.Add();
            worksheet.Name = sheetName;

            // Export DataGridView column titles to Excel starting from row 7
            for (int j = 0; j < dataGridView.Columns.Count; j++)
            {
                Excel.Range headerCell = worksheet.Cells[7, j + 1];
                headerCell.Value = dataGridView.Columns[j].HeaderText;
                headerCell.Font.Color = System.Drawing.Color.White;
                headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft; // Align header text to left
            }

            // Export DataGridView data to Excel starting from row 8
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    if (dataGridView.Rows[i] != null && dataGridView.Rows[i].Cells[j] != null && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 8, j + 1];
                        dataCell.Value = dataGridView.Rows[i].Cells[j].Value.ToString();
                        dataCell.Interior.Color = i % 2 == 0 ? System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.WhiteSmoke) : System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray); // Alternating colors
                        dataCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft; // Align cell content to left
                    }
                    else
                    {
                        // Handle the case where the cell value is null
                        worksheet.Cells[i + 8, j + 1] = string.Empty;
                    }
                }
            }

            // Get the name of the current user
            string currentName = GetCurrentUserName();

            // Insert the name below the last column
            Excel.Range nameRange = worksheet.Cells[8 + dataGridView.Rows.Count + 1, dataGridView.Columns.Count];
            nameRange.Value = currentName;
            nameRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            nameRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;

            // Auto width the cells
            worksheet.Columns.AutoFit();

            return worksheet; // Return the worksheet object
        }

        // Method to get the name of the current user
        private string GetCurrentUserName()
        {
            string sqlSelectName = "";

            try
            {
                using (MySqlConnection connection = OpenConnection())
                {
                    // Check if the currentUserID is less than 1000
                    if (currentUserID.Contains("-"))
                    {
                        // If the userID is for a regular user, get the name from the User_Accounts table
                        sqlSelectName = "SELECT Name FROM User_Accounts WHERE UserID = @UserID";
                    }
                    else
                    {
                        // If the userID is for an admin, get the name from the Administrator table
                        sqlSelectName = "SELECT Name FROM Administrator WHERE UserID = @UserID";
                    }

                    using (MySqlCommand command = new MySqlCommand(sqlSelectName, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserID);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Failed to retrieve current name.");
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return string.Empty;
            }
        }


        private void ExportChartToSheet(Chart chart, Excel.Workbook workbook, string sheetName, string chartName)
        {
            try
            {
                // Check if the chart name already exists in the workbook
                foreach (Excel.Worksheet sheet in workbook.Sheets)
                {
                    if (sheet.Name == sheetName)
                    {
                        foreach (Excel.Shape shape in sheet.Shapes)
                        {
                            if (shape.Name == chartName)
                            {
                                // The chart name is already taken, prompt the user to confirm overwrite
                                DialogResult result = MessageBox.Show($"The chart name '{chartName}' already exists in the workbook. Do you want to overwrite it?", "Chart Name Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.No)
                                {
                                    return; // User opted not to overwrite, exit method
                                }
                                else
                                {
                                    // Remove the existing chart shape
                                    shape.Delete();
                                    break; // Exit loop once the shape is deleted
                                }
                            }
                        }
                    }
                }

                // Save the chart as an image file
                string chartImagePath = Path.GetTempFileName() + ".png";
                chart.SaveImage(chartImagePath, ChartImageFormat.Png);

                // Add the chart image to the worksheet
                Excel._Worksheet worksheet = (Excel.Worksheet)workbook.Sheets.Add();
                worksheet.Name = sheetName;
                worksheet.Shapes.AddPicture(chartImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, chart.Width, chart.Height);
                worksheet.Shapes.Item(worksheet.Shapes.Count).Name = chartName; // Assign a name to the chart

                // Delete the temporary chart image file
                File.Delete(chartImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting chart to Excel: " + ex.Message, "Export Chart", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}

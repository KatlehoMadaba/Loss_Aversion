using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Loss_Aversion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method is called when the page is loaded.
            // It can be used to perform any initialization tasks.
            // Currently, it's empty because no initialization is needed.
        }

        protected void btnNext_Click1(object sender, EventArgs e)
        {
            // Generate a unique session ID using Guid.NewGuid() and store it in the session state
            Session["SessionID"] = Guid.NewGuid().ToString();
            // Call the method to insert data into the database
            InsertIntoDatabase();
            // Redirect the user to the "Register.aspx" page
            Response.Redirect("Register.aspx");
        }

        private void InsertIntoDatabase()
        {
            // Retrieve the connection string from the web.config file
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Open a connection to the database using the connection string
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                // Define the SQL query to insert data into the database
                string query = "INSERT INTO TBL_Loss_AV (LossAV_ID,Username, Decision1, Win1, Loss1, Outcome1, Decision2, Win2, Loss2, Outcome2, Decision3, Win3, Loss3, Outcome3, Decision4, Win4, Loss4, Outcome4, Decision5, Win5, Loss5, Outcome5, Decision6, Win6, Loss6, Outcome6, Final_Score) " +
                               "VALUES (@LossAV_ID,@Username,@Decision1, @Win1, @Loss1, @Outcome1, @Decision2, @Win2, @Loss2, @Outcome2, @Decision3, @Win3, @Loss3, @Outcome3, @Decision4, @Win4, @Loss4, @Outcome4, @Decision5, @Win5, @Loss5, @Outcome5, @Decision6, @Win6, @Loss6, @Outcome6, @Final_Score)";
                SqlCommand command = new SqlCommand(query, connection);

                // Set parameters for the SQL command
                command.Parameters.AddWithValue("@LossAV_ID", Session["SessionID"]);
                command.Parameters.AddWithValue("@Username", ""); // Placeholder for username, if applicable
                // Initialize decision, win, loss, and outcome values for each question to 0
                for (int i = 1; i <= 6; i++)
                {
                    command.Parameters.AddWithValue($"@Decision{i}", 0);
                    command.Parameters.AddWithValue($"@Win{i}", 0);
                    command.Parameters.AddWithValue($"@Loss{i}", 0);
                    command.Parameters.AddWithValue($"@Outcome{i}", 0);
                }
                // Initialize final score to 0
                command.Parameters.AddWithValue("@Final_Score", 0);

                // Execute the SQL command to insert data into the database
                command.ExecuteNonQuery();
            }
        }
    }
}

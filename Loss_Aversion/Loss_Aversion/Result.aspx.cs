using System;
using System.Configuration;
using System.Data.SqlClient;


namespace Loss_Aversion.assets
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the final score from the Class1 static variable
            double finalScore = Class1.Score;

            // Display the final score on the page
            lblResults.Text = "R" + Math.Round(finalScore, 2).ToString();

            // Retrieve the connection string from the web.config file
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Open a connection to the database using the connection string
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                // Define the SQL query to update the final score in the database
                string query = "UPDATE TBL_Loss_AV " +
                               "SET Final_Score = @Final_Score " +
                               "WHERE LossAV_ID = @LossAV_ID";

                // Create a SqlCommand object to execute the SQL query
                SqlCommand command = new SqlCommand(query, connection);

                // Set parameters for the SQL command
                command.Parameters.AddWithValue("@Final_Score", finalScore);
                command.Parameters.AddWithValue("@LossAV_ID", Session["SessionID"]);

                // Execute the SQL command to update the final score in the database
                command.ExecuteNonQuery();
            }
        }
    }
}

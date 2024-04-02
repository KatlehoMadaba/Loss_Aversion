using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Loss_Aversion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Display the current balance, potential loss, and potential gain
            double Amount = Math.Round(Class1.Score, 2);
            lblBettedAmount.Text = Amount.ToString();
            lblPotentialLoss.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            lblPotentialGain.Text = Math.Round(Class1.potentialWin(Class1.count), 2).ToString();

            // Check if the page is loaded for the first time
            if (!IsPostBack)
            {
                // Display the initial question
                lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                // Reset the question counter
                Class1.count = 1;
            }
            else
            {
                // If it's a postback, check if all questions have been answered
                if (Class1.count > 5)
                {
                    // Redirect to the result page if all questions are answered
                    Response.Redirect("Result.aspx");
                }
                else
                {
                    // Display the next question
                    lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                }
            }


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





        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            // Reset session variables for Win and Loss
            HttpContext.Current.Session["Win"] = 0;
            HttpContext.Current.Session["Loss"] = 0;

            // Check if all questions have been answered
            if (Class1.count >= 7)
            {
                // Update the database and redirect to the result page
                Class1.UpdateDatabase(false, Session["SessionID"].ToString(), Class1.count + 1);
                Response.Redirect("Result.aspx");
            }

            // Move to the next question
            Class1.count++;
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Check if there are more questions to display
            if (Class1.count < 6)
            {
                // Determine if the participant wins or loses based on probability
                if (Class1.determine_win_loss(Class1.Probability[Class1.count]) == "win")
                {
                    // Set session variable for Win and Loss
                    HttpContext.Current.Session["Win"] = Class1.Bet(Class1.count);
                    HttpContext.Current.Session["Loss"] = 0;
                }
                else if (Class1.determine_win_loss(Class1.Probability[Class1.count]) == "Loss")
                {
                    // Set session variable for Win and Loss
                    HttpContext.Current.Session["Win"] = 0;
                    HttpContext.Current.Session["Loss"] = Class1.Bet(Class1.count);
                }
            }

            // Check if all questions have been answered
            if (Class1.count >= 7)
            {
                // Update the database and redirect to the result page
                Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count + 1);
                Response.Redirect("Result.aspx");
            }

            // Move to the next question
            Class1.count++;
        }
    }
}

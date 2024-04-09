using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loss_Aversion
{
    public partial class Question3 : System.Web.UI.Page
    {
        int Probability = 65;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Display the initial question
            lblQuestions.Text = "One of the companies in your portfolio is about to release its earnings report, and there are mixed predictions.";
            lblProbabilitywin.Text = Probability.ToString();
            lblProbabilityloss.Text = (100 - Probability).ToString();
            //  lblPotentialLoss.Text = Class1.potentialWin(1).ToString();
            // lblPotentialGain.Text = Class1.AmountoBet().ToString();

            double Amount = Math.Round(Convert.ToDouble(HttpContext.Current.Session["Score"]), 2);
            lblBettedAmount.Text = Amount.ToString();
        }


        protected string DisplayBalance(int i)
        {
            string balance = "Nun";
            // Connection string to connect to your SQL Server database
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Concatenate the value of 'i' with the string for column name
            string columnName = "Outcome" + i;

            string query = $"SELECT {columnName} FROM TBL_Loss_AV WHERE LossAV_ID = @ID";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        // Add parameter for LossAV_ID
                        command.Parameters.AddWithValue("@ID", Session["SessionID"]);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Access the column value using the correct column name
                                balance = reader[columnName].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return balance;
        }


        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            // Reset session variables for Win and Loss
            Class1.Win = 0;
            Class1.Loss = 0;

            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), 3);

            lblBettedAmount.Text = DisplayBalance(1);

            Response.Redirect("Question4.aspx");

        }


        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Check if there are more questions to display
            double resultAmount = -1;

            // Determine if the participant wins or loses based on probability
            //string result = Class1.determine_win_loss(Class1.Probability[Class1.count]);
            resultAmount = Class1.Bet(Probability);


            //Additially to that we need to show the Win and Loss that obtained in the bet function, the values displayed on the page are inaccurate
            // Move to the next question
            Class1.UpdateDatabase(true, Session["SessionID"].ToString(), 3);

            Session["Score"] = resultAmount;


            Response.Redirect("Question4.aspx");






        }
    }
}
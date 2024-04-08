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
    public partial class Question1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                // Display the initial question
                lblQuestions.Text = "You have invested in a stock, and there's news of a Potential  market downturn.";

            lblPotentialLoss.Text = Class1.potentialWin(1).ToString();
            lblPotentialGain.Text = Class1.AmountoBet().ToString();

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

            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), 1);

            lblBettedAmount.Text = DisplayBalance(1);

            Response.Redirect("Question2.aspx");


        }


        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Check if there are more questions to display
            double resultAmount = -1;

            // Determine if the participant wins or loses based on probability
            //string result = Class1.determine_win_loss(Class1.Probability[Class1.count]);
            resultAmount = Class1.Bet(1);
            double win = Class1.potentialWin(1);
            double loss = Class1.AmountoBet();
            // UpdateBalanceDisplay(); ///THIS IS AN ISSUE , its the reason the balance is weird !!! this must be at a new page load or something
            //Additially to that we need to show the Win and Loss that obtained in the bet function, the values displayed on the page are inaccurate
            // Move to the next question
            Class1.UpdateDatabase(true, Session["SessionID"].ToString(), 1);

            Session["Score"] = resultAmount;
            Session["Win"] = win;
            Session["Loss"] = loss;

            Response.Redirect("Question2.aspx");




        }
    }
}
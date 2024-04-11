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
            
            // Check if the page is loaded for the first time
            if (!IsPostBack)
            {
                // Display the initial question
                lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                // Reset the question counter
                 Class1.count = 1;
                divStart.Visible = true;
                divGame.Visible = false;

                //  Class1.count = 0; Cant make it 0, cz theres no Decion 0 , so minus one where i call the bet function
                //make it zero then its 0, 1, 2,3,4,5

            }
            else
            {
                // If it's a postback, check if all questions have been answered
                if (Class1.count > 6) //was 5 make it 6
                {
                    // Redirect to the result page if all questions are answered
                    Response.Redirect("Result.aspx");
                }
                else
                {
                    // Display the next question
                    lblQuestions.Text = Class1.Questions[Class1.count -1].ToString(); //say count -1
                    //lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 2).ToString();
                    //lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 2).ToString();
                }
                
            }

            //Display the current balance, potential loss, and potential gain
            if (!IsPostBack)
            {
                double Amount = Math.Round(Class1.Score, 2);
                lblBettedAmount.Text = Amount.ToString();
            }
            else
            {
                double Amount = Math.Round(Class1.Score, 2);
                //lblBettedAmount.Text = DisplayBalance(Class1.count);
                lblBettedAmount.Text = Amount.ToString();
            }

            //    //lblPotentialLoss.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            //    //lblPotentialGain.Text = Math.Round(Class1.potentialWin(Class1.count - 1), 2).ToString(); //minus 1 to get 0 pos in database
            //   //minus 1 to get 0 pos in database
            //    UpdateBalanceDisplay();
            //
        }
       public static string DisplayBalance(int i)
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
                        
                        command.Parameters.AddWithValue("@ID", HttpContext.Current.Session["SessionID"]);

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


        protected void btnstart_Click(object sender, EventArgs e)
        {
            divStart.Visible = false;
            divGame.Visible = true;
            Class1.Bet(Class1.count);
            HttpContext.Current.Session["potentialLoss"] = Class1.AmountoBet();
            HttpContext.Current.Session["potentialWin"] = Class1.potentialWin(Class1.count - 1);
            lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 2).ToString();
            lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 2).ToString();
            Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count);

            //Class1.count++;
        }
        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            // Reset session variables for Win and Loss
            Class1.Win = 0;
            Class1.Loss = 0;
            //Class1.Bet(Class1.count);

            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), Class1.count);
            //Class1.Score = Convert.ToDouble(DisplayBalance(Class1.count));
           
            Class1.count++;
            lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 2).ToString();
            lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 2).ToString();
            // Check if all questions have been answered
            if (Class1.count >= 7)  //make this 7 it was 8
            {
                // Update the database and redirect to the result page
                Response.Redirect("Result.aspx");
            }

            // Move to the next question

            if (!IsPostBack)
            {
                double Amount = Math.Round(Class1.Score, 2);
                lblBettedAmount.Text = Amount.ToString();
                //lblBettedAmount.Text = DisplayBalance(Class1.count);
            }
            else
            {
                //double Amount = Math.Round(Class1.Score, 2);
                //lblBettedAmount.Text = Amount.ToString();
            }

        }
        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Check if there are more questions to display
            //double resultAmount = -1;

                // Determine if the participant wins or loses based on probability
                //string result = Class1.determine_win_loss(Class1.Probability[Class1.count]);
              /*  resultAmount = */Class1.Bet(Class1.count);

            // UpdateBalanceDisplay(); ///THIS IS AN ISSUE , its the reason the balance is weird !!! this must be at a new page load or something
            lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 2).ToString();
            lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 2).ToString();
            //Additially to that we need to show the Win and Loss that obtained in the bet function, the values displayed on the page are inaccurate
            // Move to the next question
            Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count);
          
     
            if (!IsPostBack)
            {
                double Amount = Math.Round(Class1.Score, 2);

                //lblBettedAmount.Text = Amount.ToString();
                lblBettedAmount.Text = DisplayBalance(Class1.count);
            }
            else
            {
                //double Amount = Math.Round(Class1.Score, 2);
                //lblBettedAmount.Text = Amount.ToString();
                lblBettedAmount.Text = DisplayBalance(Class1.count);
            }

            // Check if all questions have been answered
            if (Class1.count >= 7) //make this 7 it was 8
            {
                // Update the database and redirect to the result page
                Response.Redirect("Result.aspx");
            }
            Class1.count++;
        }

        private void UpdateBalanceDisplay()
        {
            double Amount = Math.Round(Class1.Score, 2);
            lblBettedAmount.Text = Amount.ToString();
        }

   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loss_Aversion
{
    public partial class Gamble2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is loaded for the first time
            if (!IsPostBack)
            {
                // Display the initial question
                lblQuestions.Text = Class1.Questions[1].ToString();
                // Reset the question counter
                1 = 1;
            }
            else
            {
                // If it's a postback, check if all questions have been answered
                if (1 > 5)
                {
                    // Redirect to the result page if all questions are answered
                    Response.Redirect("Result.aspx");
                }
                else
                {
                    // Display the next question
                    lblQuestions.Text = Class1.Questions[1].ToString();
                }
            }

            // Display the current balance, potential loss, and potential gain
            double Amount = Math.Round(Class1.Balance(), 2);
            lblBettedAmount.Text = Amount.ToString();
            lblPotentialLoss.Text = Math.Round(Class1.potentialLoss(1), 2).ToString();
            lblPotentialGain.Text = Math.Round(Class1.potentialWin(1), 2).ToString();
        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            // Reset session variables for Win and Loss
            HttpContext.Current.Session["Win"] = 0;
            HttpContext.Current.Session["Loss"] = 0;

            // Check if all questions have been answered
            if (1 >= 7)
            {
                // Update the database and redirect to the result page
                Class1.UpdateDatabase(false, Session["SessionID"].ToString(), 1 + 1);
                Response.Redirect("Result.aspx");
            }

            // Move to the next question
            1++;
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Check if there are more questions to display
            if (1 < 6)
            {
                // Determine if the participant wins or loses based on probability
                if (Class1.Win(Class1.Probability[1]) == true)
                {
                    // Set session variable for Win and Loss
                    HttpContext.Current.Session["Win"] = Class1.Bet(1);
                    HttpContext.Current.Session["Loss"] = 0;
                }
                else if (Class1.Win(Class1.Probability[1]) == false)
                {
                    // Set session variable for Win and Loss
                    HttpContext.Current.Session["Win"] = 0;
                    HttpContext.Current.Session["Loss"] = Class1.Bet(1);
                }
            }

            // Check if all questions have been answered
            if (1 >= 7)
            {
                // Update the database and redirect to the result page
                Class1.UpdateDatabase(true, Session["SessionID"].ToString(), 1 + 1);
                Response.Redirect("Result.aspx");
            }

            // Move to the next question
            1++;
        }
    }
}
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

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
            //else
            //{
            
            if (Class1.count <=6)
            {
                lblQuestions.Text = Class1.Questions[Class1.count-1].ToString();
              
            }
            else if(Class1.count == 8)
            {
                Response.Redirect("Result.aspx");
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
                lblBettedAmount.Text = Amount.ToString();
            }

          
        }
   
        protected void btnstart_Click(object sender, EventArgs e)
        {
          
                divStart.Visible = false;
                divGame.Visible = true;
                Class1.genrateFirst(Class1.count);
                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                HttpContext.Current.Session["PreviouspotentialLoss"] = HttpContext.Current.Session["potentialLoss"];
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                HttpContext.Current.Session["PreviouspotentialWin"]= HttpContext.Current.Session["potentialWin"];
                Class1.count++;
        
         
        }
        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            if (Class1.count < 8)
            {
                // Reset session variables for Win and Loss
                Class1.Win = 0;
                Class1.Loss = 0;
                
                // Display the SweetAlert2 alert using JavaScript
               
                lblBettedAmount.Text = Class1.noBet(Class1.count).ToString();
                Class1.noBet(Class1.count);
              
                bool won = false;
                if (HttpContext.Current.Session["W_Lsession"].ToString() == "WIN")
                {
                    won = true;

                }
                //string amount = Math.Round(Convert.ToDouble(HttpContext.Current.Session["PreviouspotentialWin"]), 0).ToString();
                //string amountLoss = Math.Round(Convert.ToDouble(HttpContext.Current.Session["PreviouspotentialLoss"]), 0).ToString();
                //Class1.UpdateDatabase(false, Session["SessionID"].ToString(), Class1.count - 1);
               
                string amountLoss = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                string amount = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "showLpopup", "avoidLossPopup(" + won.ToString().ToLower() + ", '" + amount + "', '" + amountLoss + "');", true);
                Class1.genrateFirst(Class1.count);
                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
         
               

                Class1.count++;
             
            }
            else
            {
                Response.Redirect("Result.aspx");
            }

            if (Class1.count == 8)
            {
                Response.Redirect("Result.aspx");
            }



        }
        protected void btnGains_Click(object sender, EventArgs e)
        {
            if (Class1.count < 8)
            {
                Class1.Bet(Class1.count);

                //Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count - 1);
                
                bool won = false;
                if (HttpContext.Current.Session["W_Lsession"].ToString() == "WIN")
                {
                    won = true;

                }
                string amountLoss = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                string amount = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "showLpopup", "GainsPopup(" + won.ToString().ToLower() + ", '" + amount + "', '" + amountLoss + "');", true);
                Class1.genrateFirst(Class1.count);
                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                //Additially to that we need to show the Win and Loss that obtained in the bet function, the values displayed on the page are inaccurate
                // Move to the next question
                Class1.count++;
                
                if (!IsPostBack)
                {
                    double Amount = Math.Round(Class1.Score, 0);
                    lblBettedAmount.Text = Amount.ToString();
                }
                else
                {

                    double Amount = Math.Round(Class1.Score, 2);
                    lblBettedAmount.Text = Amount.ToString();
                }

                if (Class1.count == 8)
                {
                    Response.Redirect("Result.aspx");
                }
            }
            else
            {
                Response.Redirect("Result.aspx");
            }

          

        }
        

       
      
        private void UpdateBalanceDisplay()
        {
            double Amount = Math.Round(Class1.Score, 0);
            lblBettedAmount.Text = Amount.ToString();
        }

        protected void modalbtn_Click(object sender, EventArgs e)
        {

        }

        protected void testbtn_Click(object sender, EventArgs e)
        {

        }
    }
}

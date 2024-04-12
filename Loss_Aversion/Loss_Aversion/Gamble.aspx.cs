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
            //else
            //{
            
            if (Class1.count <=6)
            {
                lblQuestions.Text = Class1.Questions[Class1.count-1].ToString();
              
            }
            else if(Class1.count ==7)
            {
                Response.Redirect("Result.aspx");
            }
            //    else
            //    {
            //        Response.Redirect("Result.aspx");
            //    }
            //    // If it's a postback, check if all questions have been answered
            //    //if (Class1.count == 7) //was 5 make it 6
            //    //{
            //    //    // Redirect to the result page if all questions are answered

            //    //    Response.Redirect("Result.aspx");
            //    //}
            //    //else
            //    //{
            //    //    // Display the next question
            //    //    lblQuestions.Text = Class1.Questions[Class1.count -1].ToString(); //say count -1
            //    //    //lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 2).ToString();
            //    //    //lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 2).ToString();
            //    //}
            //    //if (Class1.count < 7)
            //    //{
            //    //    lblQuestions.Text = Class1.Questions[Class1.count -1].ToString();
            //    //}
            //}

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

            //    //lblPotentialLoss.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            //    //lblPotentialGain.Text = Math.Round(Class1.potentialWin(Class1.count - 1), 2).ToString(); //minus 1 to get 0 pos in database
            //   //minus 1 to get 0 pos in database
            //    UpdateBalanceDisplay();
            //
        }
   
        protected void btnstart_Click(object sender, EventArgs e)
        {
          
                divStart.Visible = false;
                divGame.Visible = true;
                Class1.genrateFirst(Class1.count);
                //Class1.Bet(Class1.count);

                //Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count);
                //Class1.genrateFirst(Class1.count);
                //Class1.noBet(Class1.count);
                //HttpContext.Current.Session["potentialLoss"] = Class1.AmountoBet();
                //HttpContext.Current.Session["potentialWin"] = Class1.potentialWin(Class1.count - 1);

                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                Class1.count++;
        
         
        }
        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            if (Class1.count <= 7)
            {
                // Reset session variables for Win and Loss
                Class1.Win = 0;
                Class1.Loss = 0;
                //Class1.Bet(Class1.count);
                //Class1.genrateFirst(Class1.count);
                //lblBettedAmount.Text = DisplayBalance(Class1.count-1);
                //Class1.noBet();

                lblBettedAmount.Text = Class1.noBet().ToString();
                //Class1.UpdateDatabase(false, Session["SessionID"].ToString(), Class1.count - 1);
                Class1.genrateFirst(Class1.count);
                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                //UpdateBalanceDisplay();
                //Class1.Score = Convert.ToDouble(DisplayBalance(Class1.count));

                Class1.count++;
            }
            else
            {
                Response.Redirect("Result.aspx");
            }
        
            // Check if all questions have been answered
            //if (Class1.count >=9 )  //make this 7 it was 8
            //{
            //    // Update the database and redirect to the result page
             
            //}

            // Move to the next question

            //if (!IsPostBack)
            //{
            //    double Amount = Math.Round(Class1.Score, 2);
            //    //lblBettedAmount.Text = Amount.ToString();
            //    lblBettedAmount.Text = DisplayBalance(Class1.count - 1);
            //}
            //else
            //{
            //    //double Amount = Math.Round(Class1.Score, 2);
            //    //lblBettedAmount.Text = Amount.ToString();
            //}

        }
        protected void btnGains_Click(object sender, EventArgs e)
        {
            if (Class1.count <= 7)
            {
                Class1.Bet(Class1.count);

                //Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count - 1);
                Class1.genrateFirst(Class1.count);

                lblPotentialLoss.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]), 0).ToString();
                lblPotentialGain.Text = Math.Round(Convert.ToDouble(HttpContext.Current.Session["potentialWin"]), 0).ToString();
                //Additially to that we need to show the Win and Loss that obtained in the bet function, the values displayed on the page are inaccurate
                // Move to the next question
                //Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count);

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
            }
            else
            {
                Response.Redirect("Result.aspx");
            }

            // Check if all questions have been answered
            //if (Class1.count >= 9) //make this 7 it was 8
            //{
            //    // Update the database and redirect to the result page
              
            //}

        }
        

       
      
        private void UpdateBalanceDisplay()
        {
            double Amount = Math.Round(Class1.Score, 0);
            lblBettedAmount.Text = Amount.ToString();
        }

   
    }
}

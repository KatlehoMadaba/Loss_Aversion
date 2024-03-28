using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using System.Web.UI;


namespace Loss_Aversion
{
    public partial class WebForm2 : System.Web.UI.Page 
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Score"] == null)
                {
                    Session["Score"] = 1000.00;
                    Session["Win"] = 0;
                    Session["Loss"] = 0;
                }
                //count++;
                lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                Class1.count=1;
            }
            else
            {
                if (Class1.count <= 5)
                {
                    lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                }
                else
                {
                    Response.Redirect("WebForm9.aspx");
                }
            }
            
            double amount = Class1.Balance();
            double roundedAmount = Math.Round(amount, 2);
            Bettedamountlb.Text = roundedAmount.ToString(); 
            W_Lamountlb.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            potentialGainlb.Text = Math.Round(Class1.potentialWin(0), 2).ToString();

        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Win"] = 0;
            HttpContext.Current.Session["Loss"] = 0;


            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), Class1.count + 1);

            if (Class1.count >= 6)
            {
            }

            Class1.count++;
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            if (Class1.count<6)
            {
               double value = Class1.Bet(Class1.count);
                
                if (Class1.determine_win_loss(Class1.PW[Class1.count])=="Win")
                {
                    HttpContext.Current.Session["Win"] = Class1.Bet(Class1.count);
                    HttpContext.Current.Session["Loss"] = 0;
                }
                else if (Class1.determine_win_loss(Class1.PW[Class1.count]) == "Loss")
                {
                    HttpContext.Current.Session["Win"] = 0;
                    HttpContext.Current.Session["Loss"] = Class1.Bet(Class1.count);
                }
                

            }


            if (Class1.count >= 7)
            {
                Class1.UpdateDatabase(true, Session["SessionID"].ToString(), Class1.count + 1);
                Response.Redirect("WebForm9.aspx");
            }


            Class1.count++;


        }

        //private void UpdateDatabase(bool decision, string userId)
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();


                




        //        string query = "UPDATE TBL_Loss_AV " +
        //                       "SET Decision1 = @Decision1, Outcome1 = @Outcome1" +
        //                       " WHERE LossAV_ID = @LossAV_ID";
                


        //        SqlCommand command = new SqlCommand(query, connection);


        //        command.Parameters.AddWithValue("@Decision1", decision.ToString());
        //        command.Parameters.AddWithValue("@Outcome1", Session["Score"]);
        //        command.Parameters.AddWithValue("@LossAV_ID", userId);




        //       command.ExecuteNonQuery();

        //    }
        //}

        public static class ScoreManager
        {
            public static double GetScore()
            {
                return Convert.ToDouble(HttpContext.Current.Session["Score"]);
            }

            public static void AvoidLoss()
            {
                double score = GetScore();
                HttpContext.Current.Session["Score"] = score;
                //dp nothing 
            }
        }
    }
}

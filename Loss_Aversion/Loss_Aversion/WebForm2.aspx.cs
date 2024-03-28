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

                }
                //count++;
                lblQuestions.Text = Class1.Questions[Class1.count].ToString();
                Class1.count=1;
            }
            else
            {
                lblQuestions.Text = Class1.Questions[Class1.count].ToString();

            }
            

            double amount = Class1.Balance();
            double roundedAmount = Math.Round(amount, 2);
            Bettedamountlb.Text = roundedAmount.ToString(); 
            W_Lamountlb.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            potentialGainlb.Text = Math.Round(Class1.potentialWin(0), 2).ToString();

        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            
            //ScoreManager.AvoidLoss();
            //Session["Score"] = ScoreManager.GetScore();

            //UpdateDatabase(false, Session["SessionID"].ToString());
            Class1.count++;

            if (Class1.count >= 6)
            {
                Response.Redirect("WebForm9.aspx");
            }


        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            
            Class1.Bet(Class1.count);
            Class1.count++;

            if (Class1.count >= 6)
            {
                Response.Redirect("WebForm9.aspx");
            }

        }

        private void UpdateDatabase(bool decision, string userId)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();


                




                string query = "UPDATE TBL_Loss_AV " +
                               "SET Decision1 = @Decision1, Outcome1 = @Outcome1" +
                               " WHERE LossAV_ID = @LossAV_ID";
                


                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@Decision1", decision.ToString());
                command.Parameters.AddWithValue("@Outcome1", Session["Score"].ToString());
                command.Parameters.AddWithValue("@LossAV_ID", userId);




               command.ExecuteNonQuery();

            }
        }

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

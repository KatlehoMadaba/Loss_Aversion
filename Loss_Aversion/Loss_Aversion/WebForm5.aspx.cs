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
    public partial class WebForm5 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            double amount = Class1.Balance();
            double roundedAmount = Math.Round(amount, 2);
            Bettedamountlb.Text = roundedAmount.ToString();
            W_Lamountlb.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            potentialGainlb.Text = Math.Round(Class1.potentialWin(3), 2).ToString();
        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            ScoreManager.AvoidLoss();
            Session["Score"] = ScoreManager.GetScore();

            UpdateDatabase(false, Session["SessionID"].ToString());

            Response.Redirect("WebForm6.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            //ScoreManager.Gain();
            //Session["Score"] = ScoreManager.GetScore();
            Class1.Bet(3);
            
            UpdateDatabase(true, Session["SessionID"].ToString());

            Response.Redirect("WebForm6.aspx");
        }

        private void UpdateDatabase(bool decision, string userId)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "UPDATE TBL_Loss_AV " +
                               "SET Decision4 = @Decision4, Outcome4 = @Outcome4 " +
                               "WHERE LossAV_ID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Decision4", decision);
                command.Parameters.AddWithValue("@Outcome4", Session["Score"]);
                command.Parameters.AddWithValue("@UserID", userId);

                command.ExecuteNonQuery();
            }
        }


        public static class ScoreManager
        {
            private static Random rnd = new Random();
            private static int score = 300;


            public static int GetScore()
            {
                return score;
            }

            public static void AvoidLoss()
            {

            }

            public static void Gain()
            {
                // Randomly determine whether to gain or lose points

                score += rnd.Next(-300, 700);

            }
        }

    }
}
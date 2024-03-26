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
    public partial class WebForm4 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Score"] == null)
                {
                    Session["Score"] = 300;
                }
            }
        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            ScoreManager.AvoidLoss();
            Session["Score"] = ScoreManager.GetScore();

            UpdateDatabase(false, Session["SessionID"].ToString());

            Response.Redirect("WebForm5.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            ScoreManager.Gain();
            Session["Score"] = ScoreManager.GetScore();

            UpdateDatabase(true, Session["SessionID"].ToString());

            Response.Redirect("WebForm5.aspx");
        }

        private void UpdateDatabase(bool decision, string userId)
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "UPDATE TBL_Loss_AV " +
                               "SET Decision3 = @Decision3, Outcome3 = @Outcome3 " +
                               "WHERE LossAV_ID = @LossAV_ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Decision3", decision);
                command.Parameters.AddWithValue("@Outcome3", Session["Score"]);
                command.Parameters.AddWithValue("@LossAV_ID", userId);

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
                score -= rnd.Next(100, 700);

            }

            public static void Gain()
            {
                // Randomly determine whether to gain or lose points

                score += rnd.Next(100, 700);

            }
        }

    }
}
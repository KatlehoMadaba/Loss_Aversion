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

            Response.Redirect("WebForm3.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            ScoreManager.Gain();
            Session["Score"] = ScoreManager.GetScore();

            UpdateDatabase(true, Session["SessionID"].ToString());

            Response.Redirect("WebForm3.aspx");
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
            private static Random rnd = new Random();

            public static int GetScore()
            {
                return Convert.ToInt32(HttpContext.Current.Session["Score"]);
            }

            public static void AvoidLoss()
            {
                int score = GetScore();
                HttpContext.Current.Session["Score"] = score;
            }

            public static void Gain()
            {
                int score = GetScore();
                score += rnd.Next(-300, 700);
                HttpContext.Current.Session["Score"] = score;
            }
        }
    }
}

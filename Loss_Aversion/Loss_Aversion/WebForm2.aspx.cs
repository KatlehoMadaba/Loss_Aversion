using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Loss_Aversion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

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
            // Update score and store in session
            ScoreManager.AvoidLoss();
            Session["Score"] = ScoreManager.GetScore();

            // Insert decision and outcome into database
            InsertIntoDatabase(false);

            Response.Redirect("WebForm3.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            // Update score and store in session
            ScoreManager.Gain();
            Session["Score"] = ScoreManager.GetScore();

            // Insert decision and outcome into database
            InsertIntoDatabase(true);

            Response.Redirect("WebForm3.aspx");
        }

        private void InsertIntoDatabase(bool decision)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string query = "INSERT INTO TBL_Loss_AV (Decision1, Outcome1) VALUES (@Decision1, @Outcome1)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Decision1", decision);
                command.Parameters.AddWithValue("@Outcome1", Session["Score"]);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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
            score -= rnd.Next(100, 700);
            HttpContext.Current.Session["Score"] = score;
        }

        public static void Gain()
        {
            int score = GetScore();
            score += rnd.Next(100, 700);
            HttpContext.Current.Session["Score"] = score;
        }
    }
}

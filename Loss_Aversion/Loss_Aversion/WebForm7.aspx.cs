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
    public partial class WebForm7 : System.Web.UI.Page
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
            ScoreManager.AvoidLoss();
            Session["Score"] = ScoreManager.GetScore();

            InsertIntoDatabase(false);

            Response.Redirect("WebForm9.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            ScoreManager.Gain();
            Session["Score"] = ScoreManager.GetScore();

            InsertIntoDatabase(true);

            Response.Redirect("WebForm9.aspx");
        }

        private void InsertIntoDatabase(bool decision)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "INSERT INTO TBL_Loss_AV (Decision6, Outcome6) VALUES (@Decision6, @Outcome6)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Decision6", decision);
                command.Parameters.AddWithValue("@Outcome6", Session["Score"]);




                command.ExecuteNonQuery();

            }
        }
    }
}
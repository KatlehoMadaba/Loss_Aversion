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
    public partial class WebForm6 : System.Web.UI.Page
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

            Response.Redirect("WebForm7.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            ScoreManager.Gain();
            Session["Score"] = ScoreManager.GetScore();

            InsertIntoDatabase(true);

            Response.Redirect("WebForm7.aspx");
        }

        private void InsertIntoDatabase(bool decision)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "INSERT INTO TBL_Loss_AV (Decision5, Outcome5) VALUES (@Decision5, @Outcome5)";

                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@Decision5", decision);
                command.Parameters.AddWithValue("@Outcome5", Session["Score"]);






                    command.ExecuteNonQuery();

            }
        }
    }
}
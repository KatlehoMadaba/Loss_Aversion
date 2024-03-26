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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click1(object sender, EventArgs e)
        {
            Session["SessionID"] = Guid.NewGuid().ToString();

            InsertIntoDatabase();

            Response.Redirect("WebForm8.aspx");
        }

        private void InsertIntoDatabase()
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "INSERT INTO TBL_Loss_AV (LossAV_ID, Decision1, Outcome1, Decision2, Outcome2, Decision3, Outcome3, Decision4, Outcome4, Decision5, Outcome5, Decision6, Outcome6) " +
                               "VALUES (@ID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", Session["SessionID"]);

                command.ExecuteNonQuery();
            }
        }

    }
}
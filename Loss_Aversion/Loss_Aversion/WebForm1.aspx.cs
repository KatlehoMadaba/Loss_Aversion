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

                string query = "INSERT INTO TBL_Loss_AV (LossAV_ID) " +
                               "VALUES (@ID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", Session["SessionID"]);

                command.ExecuteNonQuery();
            }
        }

    }
}
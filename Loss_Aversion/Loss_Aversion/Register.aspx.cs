using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Loss_Aversion
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Class1.Score == 0)
            {
                Class1.Score = 1000.00;
                Session["Win"] = 0;
                Session["Loss"] = 0;
            }
        }

        

        protected void btnNext_Click(object sender, EventArgs e)
        {
            InsertIntoDatabase();
            Session["Username"] = txtname.Text; //session
            Response.Redirect("Gamble.aspx");
        }

        private void InsertIntoDatabase()
        {

            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "UPDATE TBL_Loss_AV SET Username = @Username WHERE LossAV_ID = @LossAV_ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LossAV_ID", Session["SessionID"]);
                command.Parameters.AddWithValue("@Username", txtname.Text);

                command.ExecuteNonQuery();


            }
        }
    }
}

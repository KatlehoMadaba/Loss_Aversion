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
            if (!IsPostBack)
            {
                if (Session["Score"] == null)
                {
                    Session["Score"] = 300;
                }
            }
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            InsertIntoDatabase();
            Session["Username"] = txtname.Text; //session
            Response.Redirect("WebForm2.aspx");
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

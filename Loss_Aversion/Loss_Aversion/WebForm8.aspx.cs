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
            Response.Redirect("WebForm3.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            InsertIntoDatabase();
            Session["Username"] = txtname.Text; //session
            Response.Redirect("WebForm3.aspx");
        }

        private void InsertIntoDatabase()
        {
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "INSERT INTO TBL_User (Username) VALUES (@Username)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtname.Text);
                command.ExecuteNonQuery();
            }
        }
    }
}

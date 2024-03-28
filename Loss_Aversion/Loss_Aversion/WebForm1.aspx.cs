using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Loss_Aversion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

                string query = "INSERT INTO TBL_Loss_AV (LossAV_ID,Username, Decision1, Win1, Loss1, Outcome1, Decision2, Win2, Loss2, Outcome2, Decision3, Win3, Loss3, Outcome3, Decision4, Win4, Loss4, Outcome4, Decision5, Win5, Loss5, Outcome5, Decision6, Win6, Loss6, Outcome6, Final_Score) " +
                               "VALUES (@LossAV_ID,@Username,@Decision1, @Win1, @Loss1, @Outcome1, @Decision2, @Win2, @Loss2, @Outcome2, @Decision3, @Win3, @Loss3, @Outcome3, @Decision4, @Win4, @Loss4, @Outcome4, @Decision5, @Win5, @Loss5, @Outcome5, @Decision6, @Win6, @Loss6, @Outcome6, @Final_Score)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LossAV_ID", Session["SessionID"]);
                command.Parameters.AddWithValue("@Username","");
                command.Parameters.AddWithValue("@Decision1", 0);
                command.Parameters.AddWithValue("@Win1", 0);
                command.Parameters.AddWithValue("@Loss1", 0);
                command.Parameters.AddWithValue("@Outcome1", 0);
                command.Parameters.AddWithValue("@Decision2", 0);
                command.Parameters.AddWithValue("@Win2", 0);
                command.Parameters.AddWithValue("@Loss2", 0);
                command.Parameters.AddWithValue("@Outcome2", 0);
                command.Parameters.AddWithValue("@Decision3", 0);
                command.Parameters.AddWithValue("@Win3", 0);
                command.Parameters.AddWithValue("@Loss3", 0);
                command.Parameters.AddWithValue("@Outcome3", 0);
                command.Parameters.AddWithValue("@Decision4", 0);
                command.Parameters.AddWithValue("@Win4", 0);
                command.Parameters.AddWithValue("@Loss4", 0);
                command.Parameters.AddWithValue("@Outcome4", 0);
                command.Parameters.AddWithValue("@Decision5", 0);
                command.Parameters.AddWithValue("@Win5", 0);
                command.Parameters.AddWithValue("@Loss5", 0);
                command.Parameters.AddWithValue("@Outcome5", 0);
                command.Parameters.AddWithValue("@Decision6", 0);
                command.Parameters.AddWithValue("@Win6", 0);
                command.Parameters.AddWithValue("@Loss6", 0);
                command.Parameters.AddWithValue("@Outcome6", 0);
                command.Parameters.AddWithValue("@Final_Score", 0);
                command.ExecuteNonQuery();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loss_Aversion.assets
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblResults.Text = "R" + Session["Score"].ToString();

            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "UPDATE TBL_Loss_AV " +
                               "SET Final_Score = @Final_Score " +
                               "WHERE LossAV_ID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Final_Score", Session["Score"]);
                command.Parameters.AddWithValue("@UserID", Session["SessionID"]);

                command.ExecuteNonQuery();

            }
        }
    }
}
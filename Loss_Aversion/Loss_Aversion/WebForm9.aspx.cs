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
            double finalScore =  Convert.ToDouble(HttpContext.Current.Session["Score"]);

            lblResults.Text = "R" + Math.Round(finalScore, 2).ToString();

            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = "UPDATE TBL_Loss_AV " +
                               "SET Final_Score = @Final_Score " +
                               "WHERE LossAV_ID = @LossAV_ID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Final_Score", HttpContext.Current.Session["Score"]);
                command.Parameters.AddWithValue("@LossAV_ID", Session["SessionID"]);

                //command.ExecuteNonQuery();

            }
        }
    }
}
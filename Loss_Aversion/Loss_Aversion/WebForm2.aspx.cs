using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using System.Web.UI;


namespace Loss_Aversion
{
    public partial class WebForm2 : System.Web.UI.Page 
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {


            Class1.Score = 1000.00;
            Session["Win"] = 0;
            Session["Loss"] = 0;
                
            lblQuestions.Text = "You have invested in a stock, and there's news of a Potential  market downturn.";
           
            
            double amount = Class1.Score;
            double roundedAmount = Math.Round(amount, 2);
            Bettedamountlb.Text = roundedAmount.ToString(); 
            W_Lamountlb.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            potentialGainlb.Text = Math.Round(Class1.potentialWin(0), 2).ToString();

        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Win"] = 0;
            HttpContext.Current.Session["Loss"] = 0;


            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), 1);

            Class1.count++;


            Response.Redirect("WebForm3.aspx");


        }

        protected void btnGains_Click(object sender, EventArgs e)
        {



            double value = Class1.Bet(Class1.count);







            Class1.UpdateDatabase(true, Session["SessionID"].ToString(), 1);

            Class1.count++;

            Response.Redirect("WebForm3.aspx");

        }






        //private void UpdateDatabase(bool decision, string userId)
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();







        //        string query = "UPDATE TBL_Loss_AV " +
        //                       "SET Decision1 = @Decision1, Outcome1 = @Outcome1" +
        //                       " WHERE LossAV_ID = @LossAV_ID";



        //        SqlCommand command = new SqlCommand(query, connection);


        //        command.Parameters.AddWithValue("@Decision1", decision.ToString());
        //        command.Parameters.AddWithValue("@Outcome1", Session["Score"]);
        //        command.Parameters.AddWithValue("@LossAV_ID", userId);




        //        command.ExecuteNonQuery();

        //    }
        //}

    }
}

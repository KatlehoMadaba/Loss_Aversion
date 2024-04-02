﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loss_Aversion
{
    public partial class WebForm3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            double amount = Class1.Score;
            double roundedAmount = Math.Round(amount, 2);
            Bettedamountlb.Text = roundedAmount.ToString();
            W_Lamountlb.Text = Math.Round(Class1.AmountoBet(), 2).ToString();
            potentialGainlb.Text = Math.Round(Class1.potentialWin(1), 2).ToString();
        }

        protected void btnAlosses_Click(object sender, EventArgs e)
        {
            ScoreManager.AvoidLoss();
            Session["Score"] = ScoreManager.GetScore();

            Class1.UpdateDatabase(false, Session["SessionID"].ToString(),2);

            Response.Redirect("WebForm4.aspx");
        }

        protected void btnGains_Click(object sender, EventArgs e)
        {
            //ScoreManager.Gain();
            //Session["Score"] = ScoreManager.GetScore();
            
            Class1.Bet(1);

            Class1.UpdateDatabase(false, Session["SessionID"].ToString(), 2);

            Response.Redirect("WebForm4.aspx");
        }

        //private void UpdateDatabase(bool decision, string userId)
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(connString))
        //    {
        //        connection.Open();

        //        string query = "UPDATE TBL_Loss_AV " +
        //                       "SET Decision2 = @Decision2, Outcome2 = @Outcome2 " +
        //                       "WHERE LossAV_ID = @LossAV_ID";

        //        SqlCommand command = new SqlCommand(query, connection);

        //        command.Parameters.AddWithValue("@Decision2", decision.ToString());
        //        command.Parameters.AddWithValue("@Outcome2", Session["Score"]);
        //        command.Parameters.AddWithValue("@LossAV_ID", userId);

        //        command.ExecuteNonQuery();
        //    }
        //}


        public static class ScoreManager
        {
            public static double GetScore()
            {
                return Convert.ToDouble(HttpContext.Current.Session["Score"]);
            }

            public static void AvoidLoss()
            {
                double score = GetScore();
                HttpContext.Current.Session["Score"] = score;
                //dp nothing 
            }
        }

    }
}
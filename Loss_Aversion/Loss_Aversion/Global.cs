using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public class Class1
{
    public static double[] Probability = { 0.9, 0.8, 0.75, 0.67, 0.50, 0.25 };
    
    public static string[] Questions = { "You have invested in a stock, and there's news of a Potential  market downturn.",
                                            "You hold a portfolio of stocks, and the market experiences high volatility.",
                                            "One of the companies in your portfolio is about to release its earnings report, and there are mixed predictions.",
                                            "There are rumors circulating about a Potential  merger involving a company you've invested in.",
                                            "The global economic situation is uncertain, impacting stock markets worldwide.",
                                            "A promising tech company is going public, and you have the chance to invest in it."};

   
    public static int count = 0;
    public static double Score = 0;


	public Class1(){}




    public static Boolean Win(double Probability)
    {
        Probability = Probability * 100;
        Random rand = new Random();

        if (rand.Next(1,100) <= Probability)
        {
            return true;
        }

        return false;

    }

    public static double Balance()
    {
        //double Balance = Convert.ToDouble(HttpContext.Current.Session["Score"]);
        return Score;
    }


    public static double expected_win_amount(double ProbWin, double Balance)
    {
        return (Balance / ProbWin); //CAL amount to win
    }

    public static double potentialLoss(int Index_Prob)
    {
        double pWin = expected_win_amount(Probability[Index_Prob], Balance());
        return pWin;
    }

    public static double potentialWin(int Index_Prob)
    {
        double pWin= expected_win_amount(Probability[Index_Prob], Balance());
        return pWin;
    }

    public static double Bet(int Index_Prob)
    {
        //add iƒ they won 
        if (Class1.Win(Class1.Probability[Index_Prob]) == true && Balance() != 0)
        {
            Score = Balance()+ Class1.expected_win_amount(Class1.Probability[Index_Prob], potentialLoss(Index_Prob));
            return Score;

        }
        else if(Class1.Win(Class1.Probability[Index_Prob]) == false && Balance() != 0)
        {   //subtract what they lost
          
            Score = Balance() - potentialLoss(Index_Prob);
            return Score;
        }

        return 0;
        
    }

    public static void UpdateDatabase(bool decision, string userId, int questIndex)
    {
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        int index = questIndex;

        if (index <= 6)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = $"UPDATE TBL_Loss_AV " +
                           $"SET Decision{questIndex} = @Decision{questIndex}, Outcome{questIndex} = @Outcome{questIndex}, " +
                           $"Win{questIndex} = @Win{questIndex}, Loss{questIndex} = @Loss{questIndex} " +
                           $"WHERE LossAV_ID = @LossAV_ID";

                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue($"@Decision{questIndex}", decision.ToString());
                command.Parameters.AddWithValue($"Outcome{questIndex}", Score);
                command.Parameters.AddWithValue("@LossAV_ID", userId);
                command.Parameters.AddWithValue($"@Win{questIndex}", HttpContext.Current.Session["Win"]);
                command.Parameters.AddWithValue($"@Loss{questIndex}", HttpContext.Current.Session["Loss"]);

                command.ExecuteNonQuery();
            }
        }
    }
}



using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public class Class1
{
    public static double[] Probability = { 95, 90, 85, 80, 50, 10 };
    
    public static string[] Questions = {    "You have invested in a stock, and there's news of a Potential  market downturn.",
                                            "You hold a portfolio of stocks, and the market experiences high volatility.",
                                            "One of the companies in your portfolio is about to release its earnings report, and there are mixed predictions.",
                                            "There are rumors circulating about a Potential  merger involving a company you've invested in.",
                                            "The global economic situation is uncertain, impacting stock markets worldwide.",
                                            "A promising tech company is going public, and you have the chance to invest in it."};

   
    public static int count = 0;
    
    //public static double Score = 0;
    public static double Score
    {
        get { return HttpContext.Current.Session["Score"] != null ? Convert.ToDouble(HttpContext.Current.Session["Score"]) : 0; }
        set { HttpContext.Current.Session["Score"] = value; }
    }

    public Class1(){}

    public static double expected_win_amount(double probability, double Balance)
    {
        double probPerc = (probability / 100);//Converting probability into decimal 
        double P_l = 1 - probPerc;// CAL probability of losing (sample space of probability )
        double A_W = Balance + (Balance / probPerc) - Balance;//CAL amount to win
        double A_1 = Balance;//Amount lost 
        //expected_amount = ((probPerc * A_W) - (P_l * A_1));//p
        //expected_amount = A_W-Balance;//p
        return A_W;
    }
    public static string determine_win_loss(double P_W)
    {
        P_W = P_W / 100;
        Random random = new Random();
        double RN = random.NextDouble();
        string Results = (RN < P_W) ? "win" : "Loss";
        return Results;
    }

    public static double AmountoBet()
    {
        Random randomAmountToBetPercecnt = new Random();

        double AmountoBet = randomAmountToBetPercecnt.NextDouble() * Score;

        return AmountoBet;
    }

    public static double potentialWin(int Index_Prob)
    {
        double pWin = Convert.ToDouble(Class1.expected_win_amount(Probability[Index_Prob], AmountoBet()));
        return pWin;

    }

    public static double Bet(int Index_Prob)
    {
        double amountToBet = AmountoBet();
        double potentialWinAmount = potentialWin(Index_Prob);
        double potentialLossAmount = amountToBet; // Potential loss is the amount to bet

        // Check if potential win amount is greater than potential loss amount
        if (potentialWinAmount > potentialLossAmount && Score >= 0)
        {
            // Determine win or loss
            if (determine_win_loss(Probability[Index_Prob]) == "win")
            {
                // Add the win amount to the balance
                Score = Score + potentialWinAmount;
            }
            else
            {
                // Subtract the loss amount from the balance
                Score = Score - potentialLossAmount;
            }
        }


        return Score;
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
    //public static Boolean Win(double Probability)
    //{
    //    Probability = Probability * 100;
    //    Random rand = new Random();

    //    return rand.Next(1,100)<=Probability;

    //}

    //public static double expected_win_amount(double ProbWin, double Balance)
    //{
    //    return (Balance / ProbWin); //CAL amount to win
    //}

    //public static double potentialLoss(int Index_Prob)
    //{
    //    double pWin = expected_win_amount(Probability[Index_Prob], Balance());
    //    return pWin;
    //}

    //public static double potentialWin(int Index_Prob)
    //{
    //    double pWin= expected_win_amount(Probability[Index_Prob], Balance());
    //    return pWin;
    //}

    //public static double Bet(int Index_Prob)
    //{
    //    //add iƒ they won 
    //    if (Class1.Win(Class1.Probability[Index_Prob]) == true && Balance() >= 0)
    //    {
    //        Score = Balance() + Class1.expected_win_amount(Class1.Probability[Index_Prob], potentialLoss(Index_Prob));
    //        return Score;

    //    }
    //    else if(Class1.Win(Class1.Probability[Index_Prob]) == false && Balance() != 0)
    //    {   //subtract what they lost

    //        Score = Balance() - potentialLoss(Index_Prob);
    //        return Score;
    //    }

    //    return 0;

    //}







}



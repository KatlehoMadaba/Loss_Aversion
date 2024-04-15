using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public class Class1
{
    public static double[] Probability = { 95, 90, 85, 80, 50, 10, 5 };
    //public static double[] Probability = { 95, 90, 85, 80, 50, 10,34 };

    public static string[] Questions = {    "You have invested in a stock, and there's news of a Potential  market downturn.",
                                            "You hold a portfolio of stocks, and the market experiences high volatility.",
                                            "One of the companies in your portfolio is about to release its earnings report, and there are mixed predictions.",
                                            "There are rumors circulating about a Potential  merger involving a company you've invested in.",
                                            "The global economic situation is uncertain, impacting stock markets worldwide.",
                                            "A promising tech company is going public, and you have the chance to invest in it."};

   
    //public static int count = 0;
    public static int count = 1;
    public static double Win = 0;
    public static double Loss = 0;
    public static int questCount = 0;
    
    public static double Score = 0;
    //public static double amountToBet ;
    //public static double potentialWinAmount ;
    // public static double Score
    //{
    //  get { return HttpContext.Current.Session["Score"] != null ? Convert.ToDouble(HttpContext.Current.Session["Score"]) : 0; }
    //set { HttpContext.Current.Session["Score"] = value; }
    //}

    public Class1(){}
    public static double getWin(double Win)
    {
        return Win;
    }
    public static double getLoss( double Loss)
    {
        return Loss;
    }
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

        double AmountoBet = Math.Round(randomAmountToBetPercecnt.NextDouble() * Score,0);

        return AmountoBet;
    }

    public static double potentialWin(int Index_Prob)
    {
        double pWin = Math.Round(Convert.ToDouble(Class1.expected_win_amount(Probability[Index_Prob], AmountoBet())),0);
        return pWin;

    }

    //For when you gamble
    public static double Bet(int Index_Prob)
  {
        //double amountToBet = AmountoBet();
        //HttpContext.Current.Session["potentialLoss"]= amountToBet;
        //double potentialWinAmount = potentialWin(Index_Prob-1); //minus one at index
        ////HttpContext.Current.Session["potentialWin"] = potentialWinAmount;
        //double potentialLossAmount = amountToBet; // Potential loss is the amount to bet
        double potentialLossAmount = Convert.ToDouble(HttpContext.Current.Session["potentialLoss"]);
        double potentialWinAmount = Convert.ToDouble(HttpContext.Current.Session["potentialWin"]);
        string W_Lsession;
            if (determine_win_loss(Probability[Index_Prob -1]) == "win") //minus one at index
             {
            // Add the win amount to the balance
                W_Lsession = "WIN";
                HttpContext.Current.Session["W_Lsession"] = W_Lsession;
                Win = potentialWinAmount;
                Loss = 0;
                Score = Score + potentialWinAmount;

            }
            else
            {
            // Subtract the loss amount from the balance
                W_Lsession = "LOST";
                HttpContext.Current.Session["W_Lsession"] = W_Lsession;
                Win = 0;
                Loss = potentialLossAmount;
                Score = Score - potentialLossAmount;
            }
           UpdateDatabase(true, HttpContext.Current.Session["SessionID"].ToString(), count - 1);
        return Score;
    }
    //Genrate PL and PW for the next page/ scenrio
    public static void genrateFirst(int Index_Prob)
    {
        double amountToBet = AmountoBet();
        HttpContext.Current.Session["potentialLoss"] = amountToBet;
        double potentialWinAmount = potentialWin(Index_Prob - 1); //minus one at index
        HttpContext.Current.Session["potentialWin"] = potentialWinAmount;
        double potentialLossAmount = amountToBet;
    }

    //For when you avoid the loss
    public static double noBet(int Index_Prob)
    {
        Win = 0;
        Loss = 0;
        Score =Score+ 0;

        double amountToBet = AmountoBet();
        HttpContext.Current.Session["nobetLoss"] = amountToBet;
        double noBetloss = amountToBet;

        double noBetwin= potentialWin(Index_Prob - 1);
        HttpContext.Current.Session["nobetWin"] = noBetwin;

        string W_Lsession;
        if (determine_win_loss(Probability[Index_Prob - 1]) == "win") //minus one at index
        {
            // Add the win amount to the balance
            W_Lsession = "WIN";
            HttpContext.Current.Session["W_Lsession"] = W_Lsession;
        }
        else
        {
            // Subtract the loss amount from the balance
            W_Lsession = "LOST";
            HttpContext.Current.Session["W_Lsession"] = W_Lsession;
        }
        
        return Score;

    }
    public static void UpdateDatabase(bool decision, string userId, int questIndex)
    {
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connString))
        {
            connection.Open();

            string query = $"UPDATE TBL_Loss_AV " +
                       $"SET Decision{questIndex} = @Decision{questIndex}, Outcome{questIndex} = @Outcome{questIndex}, " +
                       $"Win{questIndex} = @Win{questIndex}, Loss{questIndex} = @Loss{questIndex} " +
                       $"WHERE LossAV_ID = @LossAV_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue($"@Decision{questIndex}", decision.ToString());
            command.Parameters.AddWithValue($"Outcome{questIndex}", Math.Round(Score));
            command.Parameters.AddWithValue("@LossAV_ID", userId);
            command.Parameters.AddWithValue($"@Win{questIndex}", Math.Round(Win));
            command.Parameters.AddWithValue($"@Loss{questIndex}", Math.Round(Loss));

            command.ExecuteNonQuery();
        }

    }
    public static string displayPotentialL(int i)
    {
        string balance = "Nun";
        // Connection string to connect to your SQL Server database
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Concatenate the value of 'i' with the string for column name
        string columnName = "Loss" + i;

        string query = $"SELECT {columnName} FROM TBL_Loss_AV WHERE LossAV_ID = @ID";

        using (SqlConnection connection = new SqlConnection(connString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    // Add parameter for LossAV_ID

                    command.Parameters.AddWithValue("@ID", HttpContext.Current.Session["SessionID"]);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the column value using the correct column name
                            balance = reader[columnName].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        return balance;
    }
    public static string displayPotentialW(int i)
    {
        string balance = "Nun";
        // Connection string to connect to your SQL Server database
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Concatenate the value of 'i' with the string for column name
        string columnName = "Win" + i;

        string query = $"SELECT {columnName} FROM TBL_Loss_AV WHERE LossAV_ID = @ID";

        using (SqlConnection connection = new SqlConnection(connString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    // Add parameter for LossAV_ID

                    command.Parameters.AddWithValue("@ID", HttpContext.Current.Session["SessionID"]);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the column value using the correct column name
                            balance = reader[columnName].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        return balance;
    }
    public static string DisplayBalance(int i)
    {
        string balance = "Nun";
        // Connection string to connect to your SQL Server database
        string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Concatenate the value of 'i' with the string for column name
        string columnName = "Outcome" + i;

        string query = $"SELECT {columnName} FROM TBL_Loss_AV WHERE LossAV_ID = @ID";

        using (SqlConnection connection = new SqlConnection(connString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    // Add parameter for LossAV_ID

                    command.Parameters.AddWithValue("@ID", HttpContext.Current.Session["SessionID"]);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the column value using the correct column name
                            balance = reader[columnName].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        return balance;
    }
    
   







}



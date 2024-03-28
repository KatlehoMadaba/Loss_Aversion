﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;

public class Class1
{
    public static double[] PW = {70,60,50,30,10,5};
    public string WorL = "";

	public Class1(){}

    public static double expected_win_amount(double probability,double Balance)
    {
        double probPerc = (probability / 100);//Converting probability into decimal 
        double expected_amount = 0;
        double P_l = 1 - probPerc;// CAL probability of losing (sample space of probability )
        double A_W=Balance+(Balance/ probPerc)-Balance;//CAL amount to win
        double A_1 = Balance;//Amount lost 
        expected_amount = ((probPerc * A_W) - (P_l * A_1));//p
        return expected_amount;
    }

    public static string determine_win_loss(double P_W)
    {  
        P_W = P_W / 100;
        Random random = new Random();
        double RN = random.NextDouble();
        string Results = (RN< P_W) ? "win" : "Loss";
        return Results;
    }
    public static double Balance()
    {
        double Balance = Convert.ToDouble(HttpContext.Current.Session["Score"]);
        return Balance;
        //The balance
    }
    public static double AmountoBet()
    {
        Random randomAmountToBetPercecnt = new Random();

        double AmountoBet = randomAmountToBetPercecnt.NextDouble() * Balance();

        return AmountoBet;
    }
    public static double potentialWin(int Index_Prob)
    {
       double pWin= Convert.ToDouble(Class1.expected_win_amount(Class1.PW[Index_Prob], AmountoBet()));
        return pWin;
    }
    public static double Bet(int Index_Prob)
    {

        
        //add iƒ they won 
        if (Class1.determine_win_loss(Class1.PW[Index_Prob]) == "win" && Balance() != 0)
        {
            HttpContext.Current.Session["Score"] =Convert.ToDouble(HttpContext.Current.Session["Score"])+ Class1.expected_win_amount(Class1.PW[Index_Prob], AmountoBet());
            return Convert.ToDouble(HttpContext.Current.Session["Score"]);
        }
        else
        {   //subtract what they lost
          
            HttpContext.Current.Session["Score"] = Balance() - AmountoBet();
            //WorL = "LOSS";
            return Convert.ToDouble(HttpContext.Current.Session["Score"]);
        }
        
    }

}

using System;
using System.Security.Cryptography.X509Certificates;

public class Class1
{
    public static double[] PW = {70,60,50,30,5};   
	public Class1()
	{ 
        
	}

    public static double expected_win_amount(double probability,double betAmount)
    {
        double expected_amount = 0;
        double P_l = 1 - probability;
        double A_W=betAmount+(betAmount/probability)-betAmount;
        double A_1 = betAmount;
        double EV = ((probability * A_W) - (P_l * A_1));
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

}

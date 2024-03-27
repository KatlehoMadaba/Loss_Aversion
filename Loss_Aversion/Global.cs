using System;

public class Class1
{
	public Class1()
	{
	}
    public static decimal expected_win_amount(double probability,double Balance)
    {
        decimal expected_amount = 0;
        double P_l = 1 - probability;
        double A_W=Balance+(Balance/probability)-Balance;
        double A_1 = Balance;
        double EV =((probability*A_W)-(P_l*A_1))
        return expected_amount;
    }
    public static string determine_win_loss(double P_W)
    {  
        P_W = P_W / 100;
       Random random = new Random(0,1);
        double A = random.NextDouble();
        string Results = (A < P_W) ? "win" : "Loss";
        return Results;
    }

}


public class Globals
{
	public static int maxPlayers = 6;
	public static float CalculatePayoffPercent(float distPercent)
	{
		return (distPercent * 3) + 1;
	}

	public static float startingMoney = 100;
	public static float[] playerMoney = new float[maxPlayers];
}
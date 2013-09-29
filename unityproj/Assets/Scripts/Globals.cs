using UnityEngine;

public class Globals
{
	public static int maxPlayers = 6;

	public static float CalculatePayoffPercent(float distPercent)
	{
		return Mathf.Min(8f, Mathf.Pow(distPercent, -1f / 2f));
	}

	public static int FindNextPlayerIndex(int index)
	{
		int zeroBasedIndex = index - 1;
		int nextIndex = zeroBasedIndex + 1;
		for (int p = 0; p < Globals.maxPlayers; p++)
		{
			if (nextIndex > numPlayers)
			{
				nextIndex = 0;
			}

			if (joinedPlayers[nextIndex])
			{
				// Convert to 1 based.
				return nextIndex + 1;
			}

			nextIndex++;
		}

		return index;
	}

	public static float startingMoney = 100;
	public static float hoopReward = 20;
	public static float[] playerMoney = new float[maxPlayers];
	public static int numPlayers = 0;
	public static int activePilotPlayerIndex = -1;
	public static float[] playerBetPos = new float[maxPlayers];
	public static float[] playerBetAmount = new float[maxPlayers];
	public static Color[] playerColors = new Color[maxPlayers];
	public static bool[] joinedPlayers = new bool[maxPlayers];
	public static bool[] readyPlayers = new bool[maxPlayers];
	public static string[] playerNames = { "Red Baron", "Evel", "Houdini", "Curly", "Fred", "Steve" };
}
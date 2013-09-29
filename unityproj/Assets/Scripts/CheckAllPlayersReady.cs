using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class CheckAllPlayersReady : MonoBehaviour
{
    public GUIText countdownText;
    public ScreenController screenController;
    public double countdownTime = 5;
    private double startTime = -1;

    // Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
    	bool allReady = false;
        if (Globals.numPlayers > 1)
        {
            allReady = true;
            for (int p = 0; p < Globals.numPlayers; p++)
            {
                if (Globals.joinedPlayers[p] && !Globals.readyPlayers[p])
                {
                    allReady = false;
                    countdownText.text = "";
                    startTime = -1;
                    break;
                }
            }
        }

        if (allReady)
        {
            if (startTime < 0)
            {
                startTime = Utility.GetSystemTime();
            }
            double timeLeft = countdownTime - ((Utility.GetSystemTime() - startTime) / 1000);
            countdownText.text = timeLeft.ToString("0.");

            if (timeLeft <= 0)
            {
                screenController.StartFlying();
            }
        }
	}
}

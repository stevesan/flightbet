using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class BetCountdown : MonoBehaviour
{
	public GUIText countdownText;
	public ScreenController screenController;
	public double countdownTime = 15;

	private double startTime = -1;

    // Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
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

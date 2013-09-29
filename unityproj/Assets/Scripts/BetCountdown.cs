using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class BetCountdown : MonoBehaviour
{
	public GUIText countdownText;
	public float countdownTime;

	private float timeLeft;
	
    // Use this for initialization
	void Start()
    {
    	timeLeft = countdownTime;
	}
	
	// Update is called once per frame
	void Update()
    {
    	timeLeft -= 0.01f;
    	countdownText.text = timeLeft.ToString("0.");
	}
}

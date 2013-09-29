using UnityEngine;
using System.Collections;
using SteveSharp;

public class ScreenController : MonoBehaviour
{
    public GameObject gambleScreen;
    public GameObject flyScreen;
    public PlaneMover planeMover;

    public GUIText countdownText;
    public double endFlyCountdownTime = 5;
    private double endFlyStartTime = -1;

	// Use this for initialization
	void Start()
    {
	   StartGambling();
       planeMover.gameOverEvent.AddHandler(gameObject, OnPlayerDie);
	}
	
	// Update is called once per frame
	void Update()
    {
        if (endFlyStartTime > -1)
        {
            double timeLeft = endFlyCountdownTime - ((Utility.GetSystemTime() - endFlyStartTime) / 1000);
            countdownText.text = timeLeft.ToString("0.");

            if (timeLeft <= 0)
            {
                endFlyStartTime = -1;
                countdownText.text = "";
                StartGambling();
            }
        }
	}

    public void StartGambling()
    {
        flyScreen.SetActive(false);
        gambleScreen.SetActive(true);
    }

    public void StartFlying()
    {
        flyScreen.SetActive(true);
        gambleScreen.SetActive(false);
    }

    public void OnPlayerDie(GameObject plane)
    {
        endFlyStartTime = Utility.GetSystemTime();
    }
}

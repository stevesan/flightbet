using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class UpdatePlayerHUD : MonoBehaviour
{
	public GameObject moneyPrefab;
	public GameObject namePrefab;
	public GameObject pilotIconPrefab;
	private int playerIndex = -1;

    // Use this for initialization
	void Start()
    {
	}

	public void SetPlayerIndex(int setIndex)
	{
		playerIndex = setIndex;
	}
	
	// Update is called once per frame
	void Update()
    {
    	if (playerIndex < 0)
    	{
    		return;
    	}

    	moneyPrefab.GetComponent<GUIText>().text = "$" + Globals.playerMoney[playerIndex - 1].ToString("0.");
		moneyPrefab.GetComponent<GUIText>().color = Globals.playerColors[playerIndex - 1];
		namePrefab.GetComponent<GUIText>().text = Globals.playerNames[playerIndex - 1].ToString();
		namePrefab.GetComponent<GUIText>().color = Globals.playerColors[playerIndex - 1];
		pilotIconPrefab.SetActive(playerIndex == Globals.activePilotPlayerIndex);
	}
}

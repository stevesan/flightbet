using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class PlayerJoin : MonoBehaviour
{
	public GameObject joinedPlayerPrefab;
	public GameObject parentScreen;
	public PlayersHUD playersHUD;
	public Color[] playerColors;

	private class JoinedPlayer
	{
		public GameObject joinedPlayerGO;
		public int playerIndex;
	}
	private List<JoinedPlayer> joinedPlayers = new List<JoinedPlayer>();

    // Use this for initialization
	void Start()
    {
	}

	private bool GetPlayerJoined(int playerIndex)
	{
		return Globals.joinedPlayers[playerIndex - 1];
	}

	public void Reset()
	{
		foreach (JoinedPlayer joinedPlayer in joinedPlayers)
		{
			joinedPlayer.joinedPlayerGO.GetComponent<GambleMover>().Reset();
		}
	}
	
	// Update is called once per frame
	void Update()
    {
    	for (int p = 1; p <= Globals.maxPlayers; p++)
    	{
    		if (!GetPlayerJoined(p) && Input.GetAxisRaw("P" + p + "Accel") > 0.5f)
    		{
    			Globals.playerMoney[p - 1] = Globals.startingMoney;
    			Globals.playerColors[p - 1] = playerColors[p - 1];
    			
    			GameObject newPlayer = (GameObject) Instantiate(joinedPlayerPrefab, new Vector3(0, 0.3f, 0), Quaternion.identity);
    			newPlayer.GetComponent<GambleMover>().SetPlayerIndex(p);
    			newPlayer.transform.Find("GambleArrow").gameObject.GetComponent<GUITexture>().color = playerColors[p - 1];
    			newPlayer.transform.Find("BetAmount").gameObject.GetComponent<GUIText>().color = playerColors[p - 1];
    			newPlayer.transform.Find("WinAmount").gameObject.GetComponent<GUIText>().color = playerColors[p - 1];
    			newPlayer.transform.Find("Payoff").gameObject.GetComponent<GUIText>().color = playerColors[p - 1];
    			newPlayer.transform.parent = parentScreen.transform;
    			JoinedPlayer joinedPlayer = new JoinedPlayer();
    			joinedPlayer.joinedPlayerGO = newPlayer;
    			joinedPlayer.playerIndex = p;
    			joinedPlayers.Add(joinedPlayer);

    			if (Globals.activePilotPlayerIndex <= 0)
    			{
    				Globals.activePilotPlayerIndex = p;
    				Globals.ActivePlayerBuyIn();
    				Globals.readyPlayers[p - 1] = true;
    				newPlayer.SetActive(false);
    			}

	    		Globals.numPlayers++;
	    		Globals.joinedPlayers[p - 1] = true;
	    		playersHUD.OnPlayerAdded(p);
    		}
    	}
	}
}

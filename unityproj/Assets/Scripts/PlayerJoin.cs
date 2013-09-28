using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class PlayerJoin : MonoBehaviour
{
	public GameObject joinedPlayerPrefab;
	public GameObject parentScreen;

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
		foreach (JoinedPlayer joinedPlayer in joinedPlayers)
		{
			if (joinedPlayer.playerIndex == playerIndex)
			{
				return true;
			}
		}

		return false;
	}
	
	// Update is called once per frame
	void Update()
    {
    	for (int p = 1; p <= 6; p++)
    	{
    		if (!GetPlayerJoined(p) && Input.GetAxisRaw("P" + p + "Accel") > 0.5f)
    		{
    			GameObject newPlayer = (GameObject) Instantiate(joinedPlayerPrefab, new Vector3(0, -32, 0), Quaternion.identity);
    			newPlayer.GetComponent<GambleMover>().SetPlayerIndex(p);
    			newPlayer.transform.parent = parentScreen.transform;
    			JoinedPlayer joinedPlayer = new JoinedPlayer();
    			joinedPlayer.joinedPlayerGO = newPlayer;
    			joinedPlayer.playerIndex = p;
    			joinedPlayers.Add(joinedPlayer);
    		}
    	}
	}
}

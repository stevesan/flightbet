using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class PlayersHUD : MonoBehaviour
{
	public GameObject hudPlayerPrefab;

    // Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
	}

	public void OnPlayerAdded(int newPlayerIndex)
	{
		GameObject newHUDPlayer = (GameObject) Instantiate(hudPlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		newHUDPlayer.transform.position = new Vector3(((float) newPlayerIndex) * 0.15f, 0.1f, 0);
		newHUDPlayer.GetComponent<UpdatePlayerHUD>().SetPlayerIndex(newPlayerIndex);
		newHUDPlayer.transform.Find("Money").gameObject.GetComponent<GUIText>().text = "$" + Globals.playerMoney[newPlayerIndex - 1].ToString();
		newHUDPlayer.transform.Find("Money").gameObject.GetComponent<GUIText>().color = Globals.playerColors[newPlayerIndex - 1];
		newHUDPlayer.transform.Find("Name").gameObject.GetComponent<GUIText>().text = Globals.playerNames[newPlayerIndex - 1].ToString();
		newHUDPlayer.transform.Find("Name").gameObject.GetComponent<GUIText>().color = Globals.playerColors[newPlayerIndex - 1];
		newHUDPlayer.transform.Find("PilotIcon").gameObject.SetActive(newPlayerIndex == Globals.activePilotPlayerIndex);
	}
}

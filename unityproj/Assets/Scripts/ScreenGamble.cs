using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class ScreenGamble : MonoBehaviour
{
	// Use this for initialization
	void Start()
    {
    	
	}

	// Update is called once per frame
	void Update()
    {
	}

	void OnEnable()
	{
		for (int p = 0; p < Globals.maxPlayers; p++)
		{
			Globals.readyPlayers[p] = false;
		}

		if (Globals.activePilotPlayerIndex > -1)
		{
			Globals.activePilotPlayerIndex = Globals.FindNextPlayerIndex(Globals.activePilotPlayerIndex);
			Globals.readyPlayers[Globals.activePilotPlayerIndex - 1] = true;
		}

		transform.Find("PlayerJoin").gameObject.GetComponent<PlayerJoin>().Reset();
	}

	void OnDisable()
	{
	}
}

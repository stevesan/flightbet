using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class PlayersHUD : MonoBehaviour
{
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
        Debug.Log("Player added " + newPlayerIndex);
    }
}

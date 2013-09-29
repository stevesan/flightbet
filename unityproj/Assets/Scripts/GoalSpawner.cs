using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class GoalSpawner : MonoBehaviour
{
	private List<GameObject> goals = new List<GameObject>();
	public GameObject goalPrefab;

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
		for (int p = 0; p < Globals.numPlayers; p++)
		{
			if (Globals.joinedPlayers[p] && (p + 1) != Globals.activePilotPlayerIndex)
			{
				GameObject newGoal = (GameObject) Instantiate(goalPrefab, new Vector3(0, 0, 0), Quaternion.identity);

				newGoal.transform.position = new Vector3(Globals.playerBetPos[p], 0, 0);
				newGoal.GetComponent<tk2dSprite>().color = Globals.playerColors[p];
				goals.Add(newGoal);
			}
		}
	}

	void OnDisable()
	{
		foreach( GameObject obj in goals )
        {
            Destroy(obj);
        }
        goals.Clear();
	}
}

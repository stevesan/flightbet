using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class CheckBets : MonoBehaviour
{
    public GameObject plane;

    bool awardedBets = false;

    // Use this for initialization
	void Start()
    {
	}

    void OnEnable()
    {
        awardedBets = false;
        // Assume the gamblers will win their bets.
        for (int p = 0; p < Globals.maxPlayers; p++)
        {
            Globals.playerWonBet[p] = true;
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        if (plane.GetComponent<PlaneMover>().GetIsDead())
        {
            if (!awardedBets)
            {
                for (int p = 0; p < Globals.maxPlayers; p++)
                {
                    if (Globals.joinedPlayers[p] && (p - 1) != Globals.activePilotPlayerIndex && Globals.playerWonBet[p])
                    {
                        Globals.playerMoney[p] += Globals.playerBetWinAmount[p];
                    }
                }
                awardedBets = true;
            }
        }
        else
        {
            // Check if the plane crosses any bet lines.
        	for (int p = 0; p < Globals.maxPlayers; p++)
            {
                if (Globals.joinedPlayers[p] && (p - 1) != Globals.activePilotPlayerIndex)
                {
                    // If the plane crosses the bet line, the gambler loses the bet.
                    if (Globals.playerWonBet[p] && plane.transform.position.x >= Globals.playerBetPos[p])
                    {
                        Globals.playerWonBet[p] = false;
                    }
                }
            }
        }
	}
}

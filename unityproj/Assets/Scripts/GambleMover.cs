using UnityEngine;
using System.Collections;

public class GambleMover : MonoBehaviour
{
    private int playerIndex = 0;
    private float minX = 0.3f;
    private float maxX = 0.8f;
    private enum GambleMode { Dist, Cash, Ready };
    private GambleMode gambleMode = GambleMode.Dist;
    private bool selectButtonReleased = false;
    private float betAmountMoney = 100f;

    public GameObject arrow;
    public GameObject payoff;
    public GameObject betAmount;
    public GameObject winAmount;

    // Use this for initialization
	void Start()
    {
        betAmount.SetActive(false);
        winAmount.SetActive(false);
	}

    public void SetPlayerIndex(int setIndex)
    {
        playerIndex = setIndex;
    }

    private float CalcPosPercent()
    {
        Vector3 pos = this.transform.position;
        float clampedX = Mathf.Clamp(pos.x, minX, maxX);
        this.transform.position = new Vector3(clampedX, pos.y, pos.z);

        return (clampedX + Mathf.Abs(minX)) / (maxX - minX);
    }

    private void UpdateDist()
    {
        float input = Input.GetAxisRaw("P" + playerIndex + "Horizontal");
        if (Mathf.Abs(input) > 0.5f)
        {
            this.transform.Translate(new Vector3(input * 0.01f, 0, 0));
        }

        float posPercent = CalcPosPercent();
        float gamblePayoffPercent = Globals.CalculatePayoffPercent(posPercent);
        payoff.GetComponent<TextMesh>().text = (gamblePayoffPercent * 100f).ToString("0.") + "%";

        if (selectButtonReleased && Input.GetAxisRaw("P" + playerIndex + "Accel") > 0.5f)
        {
            payoff.SetActive(false);
            betAmount.SetActive(true);
            winAmount.SetActive(true);
            gambleMode = GambleMode.Cash;
        }
    }

    private void UpdateCash()
    {
        float input = Input.GetAxisRaw("P" + playerIndex + "Horizontal");
        if (Mathf.Abs(input) > 0.5f)
        {
            betAmountMoney += input;
            betAmountMoney = Mathf.Clamp(betAmountMoney, 0, Globals.playerMoney[playerIndex - 1]);
        }

        betAmount.gameObject.GetComponent<GUIText>().text = "$" + (betAmountMoney).ToString("0.");

        float posPercent = CalcPosPercent();
        float winAmountMoney = betAmountMoney * Globals.CalculatePayoffPercent(posPercent);
        winAmount.GetComponent<TextMesh>().text = "+$" + (winAmountMoney).ToString("0.");

        if (selectButtonReleased && Input.GetAxisRaw("P" + playerIndex + "Accel") > 0.5f)
        {
            payoff.SetActive(false);
            betAmount.SetActive(true);
            winAmount.SetActive(true);
            gambleMode = GambleMode.Ready;
        }
    }

    void Update()
    {
        if (playerIndex > 0)
        {
            if (gambleMode == GambleMode.Dist)
            {
                UpdateDist();
            }
            else if (gambleMode == GambleMode.Cash)
            {
                UpdateCash();
            }

            selectButtonReleased = (Input.GetAxisRaw("P" + playerIndex + "Accel") < 0.5f);
        }
    }
}

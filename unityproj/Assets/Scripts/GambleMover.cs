using UnityEngine;
using System.Collections;

public class GambleMover : MonoBehaviour
{
    private int playerIndex = 0;
    private float minX = -70;
    private float maxX = 70;
    private enum GambleMode { Dist, Cash, Ready };
    private GambleMode gambleMode = GambleMode.Dist;
    private bool selectButtonReleased = false;
    private float betAmount = 100f;

    // Use this for initialization
	void Start()
    {
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
            this.transform.Translate(Vector3.right * Time.deltaTime * input * 30);
        }

        float posPercent = CalcPosPercent();
        float gamblePayoffPercent = Globals.CalculatePayoffPercent(posPercent);
        this.transform.Find("Payoff").gameObject.GetComponent<TextMesh>().text = (gamblePayoffPercent * 100f).ToString("0.") + "%";

        if (selectButtonReleased && Input.GetAxisRaw("P" + playerIndex + "Accel") > 0.5f)
        {
            this.transform.Find("Payoff").gameObject.SetActive(false);
            this.transform.Find("BetAmount").gameObject.SetActive(true);
            this.transform.Find("WinAmount").gameObject.SetActive(true);
            gambleMode = GambleMode.Cash;
        }
    }

    private void UpdateCash()
    {
        float input = Input.GetAxisRaw("P" + playerIndex + "Horizontal");
        if (Mathf.Abs(input) > 0.5f)
        {
            betAmount += input;
            betAmount = Mathf.Clamp(betAmount, 0, Globals.playerMoney[playerIndex - 1]);
        }

        this.transform.Find("BetAmount").gameObject.GetComponent<TextMesh>().text = "$" + (betAmount).ToString("0.");

        float posPercent = CalcPosPercent();
        float winAmount = betAmount * Globals.CalculatePayoffPercent(posPercent);
        this.transform.Find("WinAmount").gameObject.GetComponent<TextMesh>().text = "+$" + (winAmount).ToString("0.");

        if (selectButtonReleased && Input.GetAxisRaw("P" + playerIndex + "Accel") > 0.5f)
        {
            this.transform.Find("Payoff").gameObject.SetActive(false);
            this.transform.Find("BetAmount").gameObject.SetActive(false);
            this.transform.Find("WinAmount").gameObject.SetActive(false);
            gambleMode = GambleMode.Cash;
        }
    }

    void FixedUpdate()
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

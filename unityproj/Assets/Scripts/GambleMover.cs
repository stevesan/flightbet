using UnityEngine;
using System.Collections;

public class GambleMover : MonoBehaviour
{
    private int playerIndex = 0;

    // Use this for initialization
	void Start()
    {
	}

    public void SetPlayerIndex(int setIndex)
    {
        playerIndex = setIndex;
    }

    void FixedUpdate()
    {
        if (playerIndex > 0)
        {
            float input = Input.GetAxisRaw("P" + playerIndex + "Horizontal");
            if (Mathf.Abs(input) > 0.5f)
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * input * 30);
            }
        }
    }
}

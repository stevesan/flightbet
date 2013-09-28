using UnityEngine;
using System.Collections;
using SteveSharp;

public class ScreenController : MonoBehaviour
{
    public GameObject gambleScreen;
    public GameObject flyScreen;

	// Use this for initialization
	void Start()
    {
	   flyScreen.SetActive(false);
       gambleScreen.SetActive(true);
	}
	
	// Update is called once per frame
	void Update()
    {
	   //flyScreen.active = true;
       //gambleScreen.active = false;
	}
}

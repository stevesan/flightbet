using UnityEngine;
using System.Collections;
using SteveSharp;

public class PracticeController : MonoBehaviour
{
    public LevelGenerator gen;
    public PlaneMover plane;

    void Reset()
    {
        gen.DestroyAll();
        gen.Generate();
        plane.Reset();
    }

	// Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
        if( Input.GetKeyDown("r") )
        {
            Reset();
        }
	}
}

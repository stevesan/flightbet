using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class LevelPreview : MonoBehaviour
{
	public LevelGenerator levelGenerator;
	public Camera screenCamera;
	public TextMesh placeYourBetsText;

	private bool initialUpdate = false;

	// Use this for initialization
	void Start()
    {
    	
	}

	// Update is called once per frame
	void Update()
    {
    	if (!initialUpdate)
    	{
    		initialUpdate = true;
    		// Generate a new level.
	    	levelGenerator.DestroyAll();
	    	levelGenerator.Generate();
	    	Time.timeScale = 0.0f;
	    	Bounds levelBounds = levelGenerator.GetBounds();
	    	screenCamera.orthographicSize = levelBounds.extents.y * screenCamera.aspect * 2;
	    	screenCamera.transform.position = new Vector3(levelBounds.center.x, levelBounds.center.y, screenCamera.transform.position.z);
    	}
	}

	void OnDisable()
	{
		Time.timeScale = 1.0f;
		initialUpdate = false;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class LevelGenerator : MonoBehaviour
{
    public bool genOnAwake = false;

    public int sizeX = 10;
    public int sizeY = 10;
    public LevelSpawner spawner;

    public float terrainPerlinFreq = 0.1f;
    public float terrainMaxHeight = 5;

    public float cloudPerlinXFreq = 0.1f;
    public float cloudPerlinYFreq = 0.1f;
    public float cloudPerlinThreshA = 0.5f;
    public float cloudPerlinThreshB = 0.2f;
    public int cloudYMin = 5;

    char[,] chars;
    int[] terrainHeight;

    public static string GridToString( char[,] grid )
    {
        string s = "";

        for( int y = 0; y < grid.GetLength(1); y++ )
        {
            for( int x = 0; x < grid.GetLength(0); x++ )
                s += grid[x,y];
            s += "\n";
        }

        return s;
    }

    void Start()
    {
        if( genOnAwake )
        {
            Generate();
        }
    }

    public void Generate()
    {
        chars = new char[ sizeX, sizeY ];
        terrainHeight = new int[ sizeX ];

        for( int x = 0; x < sizeX; x++ )
        for( int y = 0; y < sizeY; y++ )
            chars[x,y] = spawner.ignoreChar[0];

        //----------------------------------------
        //  Generate basic terrain layer
        //----------------------------------------
        float perlinX = Random.value * 10f;
        for( int x = 0; x < sizeX; x++ )
        {
            int yMax = 1    // always a bottom layer
                + Mathf.FloorToInt( terrainMaxHeight *
                    Mathf.PerlinNoise( perlinX, terrainPerlinFreq*x ) );
            terrainHeight[x] = yMax;

            /*
            for( int y = 0; y < yMax; y++ )
                chars[x,y] = 'g';
                */
            chars[x, yMax] = 'g';
        }

        //----------------------------------------
        //  Clouds
        //----------------------------------------
        for( int x = 0; x < sizeX; x++ )
        {
            for( int y = cloudYMin; y < sizeY; y++ )
            {
                float perlin = Mathf.PerlinNoise(
                        cloudPerlinXFreq * x,
                        cloudPerlinYFreq * y );

                float thresh = Mathf.Lerp(
                        cloudPerlinThreshA, cloudPerlinThreshB,
                        Utility.Unlerp( cloudYMin, sizeY, y ) );

                if( perlin > thresh )
                {
                    chars[x,y] = 'c';
                }
            }
        }


        spawner.Spawn( GridToString(chars) );
    }

}

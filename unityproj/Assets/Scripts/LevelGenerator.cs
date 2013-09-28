using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class LevelGenerator : MonoBehaviour
{
    public bool genOnAwake = false;

    public int sizeX = 10;
    public int sizeY = 10;
    public LevelSpawner backgroundSpawner;
    public LevelSpawner objectsSpawner;

    public float terrainPerlinFreq = 0.1f;
    public float terrainMaxHeight = 5;

    public float cloudPerlinXFreq = 0.1f;
    public float cloudPerlinYFreq = 0.1f;
    public float cloudPerlinThreshA = 0.5f;
    public float cloudPerlinThreshB = 0.2f;
    public int cloudYMin = 5;

    public float mineChance = 0.1f;

    public float easyHoopChance = 0.025f;

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

    char[,] CreateCharGrid(char defaultChar)
    {
        char[,] grid = new char[ sizeX, sizeY ];

        for( int x = 0; x < sizeX; x++ )
        for( int y = 0; y < sizeY; y++ )
            grid[x,y] = defaultChar;

        return grid;
    }

    public void Generate()
    {
        char[,] bgChars = CreateCharGrid( backgroundSpawner.ignoreChar[0] );
        int[] terrainHeight = new int[ sizeX ];

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

            for( int y = 0; y < yMax; y++ )
                bgChars[x,y] = 'd';
            bgChars[x, yMax] = 'g';
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
                    bgChars[x,y] = 'c';
                }
            }
        }

        backgroundSpawner.Spawn( GridToString(bgChars) );

        //----------------------------------------
        //  Mines
        //----------------------------------------
        char[,] objectsChars = CreateCharGrid( objectsSpawner.ignoreChar[0] );

        for( int x = 0; x < sizeX; x++ )
        {
            if( Random.value < mineChance )
            {
                int yMin = terrainHeight[x] + 1;
                int y = Mathf.FloorToInt( Mathf.Lerp( yMin, sizeY, Random.value ) );
                objectsChars[x, y] = 'm';
            }
        }

        //----------------------------------------
        //  Easy Hoops
        //----------------------------------------
        for( int x = 0; x < sizeX; x++ )
        {
            if( Random.value < easyHoopChance )
            {
                int yMin = terrainHeight[x] + 1;
                int y = Mathf.FloorToInt( Mathf.Lerp( yMin, sizeY, Random.value ) );
                objectsChars[x, y] = 'e';
            }
        }

        objectsSpawner.Spawn( GridToString(objectsChars) );
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class LevelGenerator : MonoBehaviour
{
    public bool genOnAwake = false;

    public int sizeX = 10;
    public int sizeY = 10;
    public LevelSpawner terrainSpawner;
    public LevelSpawner objectsSpawner;

    public float terrainPerlinFreq = 0.1f;
    public float terrainMaxHeight = 5;

    public float cloudPerlinXFreq = 0.1f;
    public float cloudPerlinYFreq = 0.1f;
    public float cloudPerlinThreshA = 0.5f;
    public float cloudPerlinThreshB = 0.2f;
    public int cloudYMin = 5;

    public float startMineChance = 0.1f;
    public float endMineChance = 0.5f;
    public float startWindChance = 0.1f;
    public float endWindChance = 0.5f;

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

    public void DestroyAll()
    {
        objectsSpawner.DestroyAll();
        terrainSpawner.DestroyAll();
    }

    public void Generate()
    {
        char bgEmptyChar = terrainSpawner.ignoreChar[0];
        char[,] terrainChars = CreateCharGrid( bgEmptyChar );
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
                terrainChars[x,y] = 'd';
            terrainChars[x, yMax] = 'g';
        }

        // Figure out which ones should be slope tiles
        for( int x = 1; x < sizeX-1; x++ )
        {
            int y = terrainHeight[x];
            bool leftFilled = terrainChars[x-1, y] != bgEmptyChar;
            bool rightFilled = terrainChars[x+1, y] != bgEmptyChar;

            if( !leftFilled && rightFilled )
            {
                terrainChars[x, y] = 'l';
                if( y-1 >= 0 )
                    terrainChars[x, y-1] = 'd';
            }
            else if( leftFilled && !rightFilled )
            {
                terrainChars[x, y] = 'r';
                if( y-1 >= 0 )
                    terrainChars[x, y-1] = 'd';
            }
        }

        //----------------------------------------
        //  Treeeees
        //----------------------------------------
        for( int x = 1; x < sizeX-1; x++ )
        {
            int y = terrainHeight[x]+1;

            if( Random.value < 0.2f )
            {
                if( terrainChars[x,y-1] == 'g' )
                    terrainChars[x,y] = '1';
            }
            else if( Random.value < 0.2f )
            {
                if( terrainChars[x,y-1] == 'g' )
                    terrainChars[x,y] = '2';
            }
        }

        //----------------------------------------
        //  Fill the top with solid ground so the plane doesn't leave
        //----------------------------------------
        for( int x = 0; x < sizeX; x++ )
        {
            terrainChars[x, sizeY-1] = 't';
        }

        for( int y = 0; y < sizeY; y++ )
        {
            terrainChars[0, y] = 'd';
        }

        //----------------------------------------
        //  Clouds
        //----------------------------------------
        /*
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
                    terrainChars[x,y] = 'c';
                }
            }
        }
        */

        terrainSpawner.Spawn( GridToString(terrainChars) );

        //----------------------------------------
        //  Mines
        //----------------------------------------
        char[,] objectsChars = CreateCharGrid( objectsSpawner.ignoreChar[0] );

        for( int x = 0; x < sizeX; x++ )
        {
            int yMin = terrainHeight[x] + 1;

            float mineChance = Utility.LinearMap( 0, sizeX, startMineChance, endMineChance, x );
            if( Random.value < mineChance )
            {
                int y = Mathf.FloorToInt( Mathf.Lerp( yMin, sizeY, Random.value ) );
                objectsChars[x, y] = 'm';
            }

            float windChance = Utility.LinearMap( 0, sizeX, startWindChance, endWindChance, x );
            if( Random.value < windChance )
            {
                while(true)
                {
                    int y = Mathf.FloorToInt( Mathf.Lerp( yMin, sizeY, Random.value ) );
                    if( objectsChars[x,y] == objectsSpawner.ignoreChar[0] )
                    {
                        objectsChars[x,y] = 'w';
                        break;
                    }
                }
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

        //----------------------------------------
        //  TEMP
        //----------------------------------------
        Bounds b = GetBounds();
        Debug.Log("min = "+b.min+" max = "+b.max);
    }

    public Bounds GetBounds()
    {
        Bounds b = terrainSpawner.GetBounds();
        Utility.GrowBounds( ref b, objectsSpawner.GetBounds() );
        return b;
    }
}

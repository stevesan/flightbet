using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SteveSharp;

public class LevelSpawner : MonoBehaviour
{
    public TextAsset input;

    [System.Serializable]
    public class PrefabEntry
    {
        public string character;
        public GameObject prefab;
    }
    public PrefabEntry[] prefabMap;

    public string playerStartChar;
    public string ignoreChar;
    public bool spawnOnAwake = false;

    public Vector3 rowStep;
    public Vector3 colStep;

    bool isSpawned = false;
    List<GameObject> instances = new List<GameObject>();

    int maxRow = 0;
    int maxCol = 0;

    void Start()
    {
        if( spawnOnAwake )
            Spawn(input.text);
    }

    public bool GetIsSpawned()
    {
        return isSpawned;
    }

    public GameObject GetPrefab( char c )
    {
        foreach( PrefabEntry e in prefabMap )
        {
            if( e.character[0] == c )
                return e.prefab;
        }

        return null;
    }

    public void DestroyAll()
    {
        foreach( GameObject obj in instances )
        {
            Destroy(obj);
        }
        instances.Clear();

        maxRow = 0;
        maxCol = 0;
    }
    
    public void Spawn(string text)
    {
        DestroyAll();

        string[] lines = text.Split('\n');

        int row = 0;
        foreach( string line in lines )
        {
            int col = 0;

            foreach( char c in line )
            {
                Vector3 cellCenter = transform.position + (row+0.5f)*rowStep + (col+0.5f)*colStep;
                if( c == ignoreChar[0] )
                {
                }
                else
                {
                    GameObject prefab = GetPrefab(c);
                    if( prefab == null )
                    {
                        Debug.LogError("Could not find prefab for character "+c);
                    }
                    else
                    {
                        GameObject obj = Utility.Instantiate( prefab, cellCenter, transform );
                        instances.Add(obj);

                        maxCol = Mathf.Max( maxCol, col );
                        maxRow = Mathf.Max( maxRow, row );
                    }
                }

                col++;
            }

            row++;
        }

        isSpawned = true;
    }

    public Bounds GetBounds()
    {
        Vector3 c0 = transform.position;
        Vector3 c1 = transform.position + (maxRow+1)*rowStep;
        Vector3 c2 = transform.position + (maxCol+1)*colStep;
        Vector3 c3 = transform.position + (maxRow+1)*rowStep + (maxCol+1)*colStep;

        Bounds b = new Bounds();
        b.min = Vector3.Min( c0, Vector3.Min( c1, Vector3.Min( c2, c3 ) ) );
        b.max = Vector3.Max( c0, Vector3.Max( c1, Vector3.Max( c2, c3 ) ) );
        return b;
    }
}

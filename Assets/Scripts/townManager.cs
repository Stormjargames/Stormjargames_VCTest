using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townManager : MonoBehaviour
{ 
    public int gridx = 5;
    public int gridz = 5;
    public int gridSpacing = 6;
    public List<GameObject> tiles;
    [HideInInspector]
    public List<GameObject> SpawnedTiles;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnGrid();
    }

    public void SpawnGrid()
    {
        for (int i = 0; i < gridx; i++)
        {
            for (int j = 0; j < gridz; j++)
            {
                GameObject tile1 = tiles[Random.Range(0, tiles.Count)];
                GameObject go = Instantiate(tile1);
                go.transform.position = new Vector3(i * gridSpacing, 0, j * gridSpacing);
                SpawnedTiles.Add(go);
            }
        }
    }

}

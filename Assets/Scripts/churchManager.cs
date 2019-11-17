using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class churchManager : MonoBehaviour
{ 
    int gridx = 5;
    int gridz = 5;
    int gridSpacing = 6;
    townManager tm;
    public List<GameObject> churchPiece;
    [HideInInspector]
    public List<GameObject> SpawnedPieces;
    public int toggleAmount = 4;
    public float endTime = 4;

    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.Find("townManager").GetComponent<townManager>();
        gridx = tm.gridx;
        gridz = tm.gridz;
        gridSpacing = tm.gridSpacing;
        SpawnGrid();
        StartCoroutine("itime");
    }

    public void SpawnGrid()
    {
        for (int i = 0; i < gridx; i++)
        {
            for (int j = 0; j < gridz; j++)
            {
                GameObject tile1 = churchPiece[Random.Range(0, churchPiece.Count)];
                GameObject go = Instantiate(tile1);
                go.transform.position = new Vector3(i * gridSpacing, 0, j * gridSpacing);
                SpawnedPieces.Add(go);
                go.SetActive(false);
                //go.GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0,255),Random.Range(0,255),Random.Range(0,255));

            }
        }
    }

    public IEnumerator itime()
    {
        while (true)
        {
            ToggleTile();
            yield return new WaitForSeconds(endTime);
        }
    }

    // Update is called once per frame


    public void ToggleTile()
    {
        List<GameObject> toggleThese = new List<GameObject>();
        for (int k = 0; k < toggleAmount; k++)
        {
            int i = Random.Range(0, SpawnedPieces.Count);
            toggleThese.Add(SpawnedPieces[i]);
            
            if (tm != null) { toggleThese.Add(tm.SpawnedTiles[i]); }
        }

        foreach (GameObject g in toggleThese)
        {
            g.SetActive(!g.activeSelf);
        }
    }

}

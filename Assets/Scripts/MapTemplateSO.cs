using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "MapTemplate", menuName = "BellyOfDoors/MapTemplate", order = 0)]
public class MapTemplateSO : ScriptableObject
{
    //Default control allows negative values = bad
    [HideInInspector]
    public int width = 3, height = 3;

    [HideInInspector]
    public string TemplateName = "MapTemplate";


    public GameObject[] prefabs = new GameObject[1];

    [System.Serializable]
    public class Row
    {
        public TileType[] entries;
    }

    public void RandomGen()
    {
        foreach(Row r in myThings)
        {
            for(int i = 0; i< r.entries.Length; i++)
            {
                int rand = Random.Range(0,2);
                r.entries[i] = (rand == 0) ? TileType.CATHEDRAL : TileType.TOWN;
            }
        }
    }

    public void ChangeAllTiles(TileType t)
    {
        foreach (Row r in myThings)
        {
            for (int i = 0; i < r.entries.Length; i++)
            {
                r.entries[i] = t;
            }
        }
    }

    //Hide default array drawing = ugly
    [HideInInspector]
    public Row[] myThings;


}

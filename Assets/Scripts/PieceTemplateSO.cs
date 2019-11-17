using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BitMaskTemplate", menuName = "BellyOfDoors/PieceTemplate", order = 0)]
public class PieceTemplateSO : ScriptableObject
{
    //Default control allows negative values = bad
    [HideInInspector]
    public int width = 3, height = 3;

    [HideInInspector]
    public string TemplateName = "BitMaskTemplate";

    [HideInInspector]
    public CompassPoints direction = CompassPoints.NORTH;

    [HideInInspector]
    public bool disable = false;

    [HideInInspector]
    public bool boostWhenTied = false;

    public GameObject[] prefabs = new GameObject[1];

    [System.Serializable]
    public class Row
    {
        public TileType[] entries;
    }

    //Hide default array drawing = ugly
    [HideInInspector]
    public Row[] myThings;


}

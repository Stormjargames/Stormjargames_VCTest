using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [TextArea]
    public string map;
    public int mapXrange = 5;
    public int mapYrange = 5;
    public char[,] mapRead;
    public int x = 0;
    public int y = 0;
    public DefineTileMask dftm;
    public GameObject prefab;

    void Start()
    {
        readMapnow();
        
        //for (int i = 0; i < mapRead.GetLength(0); i++)
        //{
        //    string s = "";
        //    for (int j = 0; j < mapRead.GetLength(1); j++)
        //    {
        //        s += mapRead[i, j];
        //    }
        //    print(s);
            
        //}
        
    }

    public void readMapnow()
    {
        mapRead = ReadMap(map, mapXrange, mapYrange);
    }

    [EasyButtons.Button]
    public void doButton()
    {
        for (int i = 0; i < mapXrange; i++)
        {
            for (int j = 0; j < mapYrange; j++)
            {
                char[] needle = dftm.GetLocalRange(mapRead, i, j);
                bool b = dftm.TrySpawningTile(needle, dftm.outerWallStraight, prefab, i, j);
            }
        }
    }


    char[,] ReadMap(string m, int Xrange, int Yrange)
    {
        char[,] output = new char[Xrange, Yrange];
        int count = 0;
        for (int i = 0; i < Xrange; i++)
        {
            for (int j = 0; j < Yrange; j++)
            {
                //print("Here" + i + " " + j);
                output[i, j] = m.ToCharArray()[count];
                //print(output[i, j].ToString() + " .." + count);
                count += 1;
            }
            count += 1;
        }
        return output;
    }
}

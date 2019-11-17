using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TileType
{
    NULL,
    WATER, //test
    LAND, //test
    OUTOFBOUNDS,
    TOWN,
    CATHEDRAL
}

public enum CompassPoints
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class TextureRegionContext
{
    public List<GameObject> modelList;
    public TileContext context;
    public int instanceScore = 0;
    public string thisName = "";
    public CompassPoints rotation = CompassPoints.NORTH;
    public bool disabled = false;
    public bool boostCentre = false;

    public TextureRegionContext(TileContext context, List<GameObject> models, string bigName, CompassPoints dir, bool disable, bool boost)
    {
        this.modelList = models;
        this.context = context;
        this.thisName = bigName;
        rotation = dir;
        disabled = disable;
        boostCentre = boost;
       
    }
}

public class TileContext
{

    public TileContext(TileType[] adjacentsTilesTypes)
    {
        adjacentTilesType = adjacentsTilesTypes;
    }
    /**
    * Adjacent types in clockwise order starting topleft
    * Index 4 is the current tile's type
    * 0 | 1 | 2
    * 3 | 4 | 5
    * 6 | 7 | 8
    */
    public TileType[] adjacentTilesType = new TileType[9];

    /**
    * Compares to TileDef contexts and score them. Returns adjacentTilesType.length if identical
    */
    public int compareScore(TileType[] other)
    {
        int score = 0;

        for (int i = 0; i < adjacentTilesType.Length; i++) {
            if (adjacentTilesType[i] == TileType.NULL// Null is a wildcard
                    || other[i] == TileType.NULL)
            {
                continue;
            }

            else if (adjacentTilesType[i] == other[i])
            {
                score++;
            }
            else
            {
                score--;
            }
        }
        return score;
    }
}

public class QuickBitMask: MonoBehaviour
{
    public List<PieceTemplateSO> templates;

    public TileType[] tileMap = new TileType[9]{

            TileType.WATER,     TileType.WATER,     TileType.WATER,
            TileType.WATER,     TileType.WATER,     TileType.LAND,
            TileType.WATER,     TileType.WATER,     TileType.WATER
        };

    public MapTemplateSO mapTemplate;

    public TileType[,] tileMap2D;

    public int tileMapSize = 3;
    public float spacing = 0f;
    public Vector2 manualOffset;
    public bool autoOffset = true;

    void Awake()
    {
        tileMap2D = new TileType[tileMapSize, tileMapSize];
        tileMap = GetTileContextFromTemplate(mapTemplate, mapTemplate.width).adjacentTilesType;

        int count = 0;
        for (int j = 0; j< tileMapSize; j++)
        {
            for (int i = 0; i < tileMapSize; i++)
            {
                tileMap2D[i, j] = tileMap[count];
                count++;
            }
        }
    }

    public TileContext GetTileContextFromTemplate(PieceTemplateSO p, int size)
    {
        TileType[] listOfTypes = new TileType[size*size];

        int count = 0;
        for(int j = 0; j< size; j++)
        {
            for(int i = 0; i<size; i++)
            {
                listOfTypes[count] = p.myThings[j].entries[i];
                count++; 
            }
        }

        return new TileContext(listOfTypes);
    }

    public TileContext GetTileContextFromTemplate(MapTemplateSO p, int size)
    {
        TileType[] listOfTypes = new TileType[size*size];

        //print(p.myThings[0].entries[4].ToString());

        int count = 0;
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                
                listOfTypes[count] = p.myThings[j].entries[i];
                count++;
            }
        }

        return new TileContext(listOfTypes);
    }


    void Start()
    {

        //add the textures to the texture list

        
        List<TextureRegionContext> textureMasks = new List<TextureRegionContext>();
        foreach (PieceTemplateSO p in templates)
        {
            TextureRegionContext t = new TextureRegionContext(GetTileContextFromTemplate(p, 3), p.prefabs.ToList(), p.TemplateName, p.direction, p.disable, p.boostWhenTied);
            textureMasks.Add(t);
        }

        //now go through each tile and spawn the right prefab!

        for (int j = 0; j < tileMapSize; j++)
        {
            for (int i = 0; i < tileMapSize; i++)
            {
                
                TileType[] t = GetAdjacentTiles(tileMap2D, i, j);
                List<int> scores = new List<int>();
                foreach(TextureRegionContext trc in textureMasks)
                {
                    if (!trc.disabled)
                    {
                        trc.instanceScore = trc.context.compareScore(t);
                    }
                }

                List<TextureRegionContext> choices = textureMasks.OrderByDescending(p => p.instanceScore).ToList();
                TextureRegionContext choice = choices[0];
                if (choices.Count>1 && choices[0].instanceScore == choices[1].instanceScore)
                {
                    int drawnscore = choices[0].instanceScore;
                    var ties = choices.Where(p => p.instanceScore == drawnscore);
                    if (ties.Any(p => p.boostCentre))
                    {
                        choice = ties.First(p => p.boostCentre);
                    }
                }

                SpawnTile(choice.modelList, i, j, choice.rotation);

            }
        }

    }

    public void SpawnTile(List<GameObject> prefabs, int x, int z, CompassPoints rot)
    {
        Vector3 xyz = new Vector3((float)x * spacing, 0, (float)z * -spacing);

        if (autoOffset)
        {
            xyz -= new Vector3((tileMapSize -1) * spacing/2, 0, (tileMapSize - 1) * -spacing / 2);
        }
        else
        {
            xyz += new Vector3(manualOffset.x, 0, manualOffset.y);
        }

        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

        float euler = 0;
        switch (rot)
        {
            case CompassPoints.NORTH:
                euler = 0;
                break;
            case CompassPoints.EAST:
                euler = 90;
                break;
            case CompassPoints.SOUTH:
                euler = 180;
                break;
            case CompassPoints.WEST:
                euler = 270;
                break;
        }

        Instantiate(prefab, xyz, Quaternion.Euler(0, euler, 0));
    }

    public TileType[] GetAdjacentTiles(TileType[,] map, int x, int z)
    {
        
        TileType[] adjacentTiles = new TileType[9];
        TileType zero = (x-1 < 0 || x-1 >= tileMapSize || z-1 < 0 || z-1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x - 1, z - 1];
        TileType one = (x < 0 || x >= tileMapSize || z-1 < 0 || z-1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x, z - 1];
        TileType two = (x+1 < 0 || x+1 >= tileMapSize || z-1 < 0 || z-1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x + 1, z - 1];
        TileType three = (x-1 < 0 || x-1 >= tileMapSize || z < 0 || z >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x -1, z];
        TileType four = map[x, z]; 
        TileType five = (x+1 < 0 || x+1 >= tileMapSize || z < 0 || z >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x + 1, z];
        TileType six = (x-1 < 0 || x-1 >= tileMapSize || z+1 < 0 || z+1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x - 1, z + 1];
        TileType seven = (x < 0 || x >= tileMapSize || z+1 < 0 || z+1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x, z + 1];
        TileType eight = (x + 1 < 0 || x + 1 >= tileMapSize || z + 1 < 0 || z + 1 >= tileMapSize) ? TileType.OUTOFBOUNDS : map[x + 1, z + 1];

        adjacentTiles = new TileType[9]
        {
            zero, one, two, three, four, five, six, seven, eight
        };
        return adjacentTiles;
    }


}

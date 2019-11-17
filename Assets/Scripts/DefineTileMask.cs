using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineTileMask : MonoBehaviour
{
    /*public static List<char>[][] DefineTilemask(List<char> nW, List<char> n, List<char> nE, List<char> w, List<char> centre, List<char> e, List<char> sW, List<char> s, List<char> sE)
    {
        List<char>[] template = new List<char>[9] {
        nW, n,      nE,
        w,  centre, e,
        sW, s,      sE
    };

    return new List<char>[4][] {
        RotateLocalRange (template, 0),
        RotateLocalRange (template, 1),
        RotateLocalRange (template, 2),
        RotateLocalRange (template, 3)
        };
    }

    public static List<char>[] RotateLocalRange(List<char>[] localRange, int rotations)
    {
        List<char>[] rotatedList = new List<char>[9] { localRange[0], localRange[1], localRange[2], localRange[3], localRange[4], localRange[5], localRange[6], localRange[7], localRange[8] };

        for (int i = 0; i < rotations; i++)
        {

            List<char>[] tempList = new List<char>[9] { rotatedList[6], rotatedList[3], rotatedList[0], rotatedList[7], rotatedList[4], rotatedList[1], rotatedList[8], rotatedList[5], rotatedList[2] };
            rotatedList = tempList;
        }

        return rotatedList;
    }

    
Usage:
    char[] localRange = GetLocalRange (blueprint, row, column);
     
    blueprint is the 2D array that defines the building.
    row and column are the array indices of the current tile being evaluated.


    public static char[] GetLocalRange(char[,] thisArray, int row, int column)
    {
        char[] localRange = new char[9];
        int localRangeCounter = 0;

        // Iterators start counting from -1 to offset the reading up and to the left, placing the requested index in the centre.
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int tempRow = row + i;
                int tempColumn = column + j;

                if (IsIndexValid(thisArray, tempRow, tempColumn) == true)
                {
                    localRange[localRangeCounter] = thisArray[tempRow, tempColumn];
                }
                else
                {
                    localRange[localRangeCounter] = '_';
                }

                localRangeCounter++;
            }
        }

        return localRange;
    }

    public static bool IsIndexValid(char[,] thisArray, int row, int column)
    {
        // Must check the number of rows at this point, or else an OutOfRange exception gets thrown when checking number of columns.
        if (row < thisArray.GetLowerBound(0) || row > (thisArray.GetUpperBound(0))) return false;

        if (column < thisArray.GetLowerBound(1) || column > (thisArray.GetUpperBound(1))) return false;
        else return true;
    }

    
Usage:
    TrySpawningTile (localRange, TileIDs.outerWallStraight, outerWallWall, 
                     floorEdgingHalf, row, column);


    // These quaternions have a -90 rotation along X because models need to be 
    // rotated in Unity due to the different up axis in Blender.
    public static readonly Quaternion ROTATE_NONE = Quaternion.Euler(-90, 0, 0);
    public static readonly Quaternion ROTATE_RIGHT = Quaternion.Euler(-90, 90, 0);
    public static readonly Quaternion ROTATE_FLIP = Quaternion.Euler(-90, 180, 0);
    public static readonly Quaternion ROTATE_LEFT = Quaternion.Euler(-90, -90, 0);

    bool TrySpawningTile(char[] needleArray, List<char>[][] templateArray, GameObject tilePrefab, int row, int column)
    {
        Quaternion horizontalRotation;

        if (TileMatchesTemplate(needleArray, templateArray, out horizontalRotation) == true)
        {
            SpawnTile(tilePrefab, row, column, horizontalRotation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TileMatchesTemplate(char[] needleArray, List<char>[][] tileMaskJaggedArray, out Quaternion horizontalRotation)
    {
        horizontalRotation = ROTATE_NONE;

        for (int i = 0; i < (tileMaskJaggedArray.Length); i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j == 4) continue; // Skip checking the centre position (no need to ascertain that a block is what it says it is).

                if (tileMaskJaggedArray[i][j].Count != 0)
                {
                    if (tileMaskJaggedArray[i][j].Contains(needleArray[j]) == false) break;
                }

                if (j == 8) // The loop has iterated nine times without stopping, so all tiles must match.
                {
                    switch (i)
                    {
                        case 0:
                            horizontalRotation = ROTATE_NONE;
                            break;
                        case 1:
                            horizontalRotation = ROTATE_RIGHT;
                            break;
                        case 2:
                            horizontalRotation = ROTATE_FLIP;
                            break;
                        case 3:
                            horizontalRotation = ROTATE_LEFT;
                            break;
                    }
                    return true;
                }
            }
        }
        return false;
    }

    void SpawnTile(GameObject tilePrefab, int row, int column, Quaternion horizontalRotation)
    {
        Instantiate(tilePrefab, new Vector3(column, 0, -row), horizontalRotation);
    }

    public static readonly List<char> any = new List<char>() { };
    public static readonly List<char> ignored = new List<char>() { ' ', '_' };
    public static readonly List<char> wall = new List<char>() { '#', 'D', 'W' };

    public static List<char>[][] outerWallStraight = DefineTileMask.DefineTilemask
    (
        any, ignored, any,
        wall, any, wall,
        any, any, any
    );

    //void Start()
    //{

    //    for (int i = 0; i < outerWallStraight.Length; i++)
    //    {
    //        for (int j = 0; j < outerWallStraight[i].Length; j++)
    //        {
    //            string n = i + ", " + j + ", ";
    //            foreach (char c in outerWallStraight[i][j])
    //            {
    //                string hc = c.ToString();
    //                n += hc + " ";
    //            }
    //            print(n);
    //        }
    //    }
    //}

    public static List<char>[][] outerWallCorner = DefineTilemask
    (
        any, ignored, any,
        wall, any, wall,
        any, any, any
    );
    */

    public List<char>[][] DefineTilemask(List<char> nW, List<char> n, List<char> nE, List<char> w, List<char> centre, List<char> e, List<char> sW, List<char> s, List<char> sE)
    {
        List<char>[] template = new List<char>[9] {
        nW, n,      nE,
        w,  centre, e,
        sW, s,      sE
    };

    return new List<char>[4][] {
        RotateLocalRange (template, 0),
        RotateLocalRange (template, 1),
        RotateLocalRange (template, 2),
        RotateLocalRange (template, 3)
        };
    }

    public List<char>[] RotateLocalRange(List<char>[] localRange, int rotations)
    {
        List<char>[] rotatedList = new List<char>[9] { localRange[0], localRange[1], localRange[2], localRange[3], localRange[4], localRange[5], localRange[6], localRange[7], localRange[8] };

        for (int i = 0; i < rotations; i++)
        {

            List<char>[] tempList = new List<char>[9] { rotatedList[6], rotatedList[3], rotatedList[0], rotatedList[7], rotatedList[4], rotatedList[1], rotatedList[8], rotatedList[5], rotatedList[2] };
            rotatedList = tempList;
        }

        return rotatedList;
    }

    
/*Usage:
    char[] localRange = GetLocalRange (blueprint, row, column);
     
    blueprint is the 2D array that defines the building.
    row and column are the array indices of the current tile being evaluated.
*/

    public char[] GetLocalRange(char[,] thisArray, int row, int column)
    {
        char[] localRange = new char[9];
        int localRangeCounter = 0;

        // Iterators start counting from -1 to offset the reading up and to the left, placing the requested index in the centre.
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int tempRow = row + i;
                int tempColumn = column + j;

                if (IsIndexValid(thisArray, tempRow, tempColumn) == true)
                {
                    localRange[localRangeCounter] = thisArray[tempRow, tempColumn];
                }
                else
                {
                    localRange[localRangeCounter] = '_';
                }

                localRangeCounter++;
            }
        }

        return localRange;
    }

    public bool IsIndexValid(char[,] thisArray, int row, int column)
    {
        // Must check the number of rows at this point, or else an OutOfRange exception gets thrown when checking number of columns.
        if (row < thisArray.GetLowerBound(0) || row > (thisArray.GetUpperBound(0))) return false;

        if (column < thisArray.GetLowerBound(1) || column > (thisArray.GetUpperBound(1))) return false;
        else return true;
    }

/*
Usage:
    TrySpawningTile (localRange, TileIDs.outerWallStraight, outerWallWall, 
                     floorEdgingHalf, row, column);
*/

    // These quaternions have a -90 rotation along X because models need to be 
    // rotated in Unity due to the different up axis in Blender.
    public Quaternion ROTATE_NONE = Quaternion.Euler(-90, 0, 0);
    public Quaternion ROTATE_RIGHT = Quaternion.Euler(-90, 90, 0);
    public Quaternion ROTATE_FLIP = Quaternion.Euler(-90, 180, 0);
    public Quaternion ROTATE_LEFT = Quaternion.Euler(-90, -90, 0);

    public bool TrySpawningTile(char[] needleArray, List<char>[][] templateArray, GameObject tilePrefab, int row, int column)
    {
        Quaternion horizontalRotation;

        if (TileMatchesTemplate(needleArray, templateArray, out horizontalRotation) == true)
        {
            SpawnTile(tilePrefab, row, column, horizontalRotation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TileMatchesTemplate(char[] needleArray, List<char>[][] tileMaskJaggedArray, out Quaternion horizontalRotation)
    {
        horizontalRotation = ROTATE_NONE;

        for (int i = 0; i < (tileMaskJaggedArray.Length); i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j == 4) continue; // Skip checking the centre position (no need to ascertain that a block is what it says it is).

                if (tileMaskJaggedArray[i][j].Count != 0)
                {
                    if (tileMaskJaggedArray[i][j].Contains(needleArray[j]) == false) break;
                }

                if (j == 8) // The loop has iterated nine times without stopping, so all tiles must match.
                {
                    switch (i)
                    {
                        case 0:
                            horizontalRotation = ROTATE_NONE;
                            break;
                        case 1:
                            horizontalRotation = ROTATE_RIGHT;
                            break;
                        case 2:
                            horizontalRotation = ROTATE_FLIP;
                            break;
                        case 3:
                            horizontalRotation = ROTATE_LEFT;
                            break;
                    }
                    return true;
                }
            }
        }
        return false;
    }

    void SpawnTile(GameObject tilePrefab, int row, int column, Quaternion horizontalRotation)
    {
        Instantiate(tilePrefab, new Vector3(column, 0, -row), horizontalRotation);
    }

    public List<char> any = new List<char>() { };
    public List<char> ignored = new List<char>() { ' ', '_' };
    public List<char> wall = new List<char>() { '#', 'D', 'W' };

    public List<char>[][] outerWallStraight;

    void Start()
    {
        outerWallStraight = DefineTilemask
        (
            any, any, any,
            any, wall, any,
            any, any, any
        );

        /*for (int i = 0; i < outerWallStraight.Length; i++)
        {
            for (int j = 0; j < outerWallStraight[i].Length; j++)
            {
                string n = i + ", " + j + ", ";
                foreach (char c in outerWallStraight[i][j])
                {
                    string hc = c.ToString();
                    n += hc + " ";
                }
                print(n);
            }
        }*/
    }

}

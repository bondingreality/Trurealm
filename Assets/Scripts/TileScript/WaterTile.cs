using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class WaterTile : Tile
{
    enum DirectionIndex
    { 
        LeftBottom = 0,
        LeftMiddle = 1,
        LeftTop = 2,
        CenterBottom = 3,
        CenterTop = 4,
        RightBottom = 5,
        RightMiddle = 6,
        RightTop = 7
    }

    [SerializeField]
    private List<Sprite> WaterSprites;

    //A preview of the tile
    [SerializeField]
    private Sprite preview;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }

    /// <summary>
    /// Refreshes this tile when something changes
    /// </summary>
    /// <param name="position">The tiles position in the grid</param>
    /// <param name="tilemap">A reference to the tilemap that this tile belongs to.</param>
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++) //Runs through all the tile's neighbours 
        {
            for (int x = -1; x <= 1; x++)
            {
                //We store the position of the neighbour 
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if (HasWater(tilemap, nPos)) //If the neighbour has water on it
                {
                    tilemap.RefreshTile(nPos); //Them we make sure to refresh the neighbour aswell
                }
            }
        }
    }

    private bool HasWater(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);
                    if (HasWater(tilemap, nPos))
                    {
                        composition += 'w';
                    }
                    else
                    {
                        composition += 'e';
                    }
                }
            }
        }

        string edgeCheckString = 
            string.Concat(
                composition[(int)DirectionIndex.LeftMiddle], 
                composition[(int)DirectionIndex.CenterBottom], 
                composition[(int)DirectionIndex.CenterTop], 
                composition[(int)DirectionIndex.RightMiddle]);

        bool needsCorner = false;

        #region WaterTileSelection
        switch (edgeCheckString)
        {
            case "eeee":
                tileData.sprite = WaterSprites.Find(T => T.name == "0");
                break;
            case "eeew":
                tileData.sprite = WaterSprites.Find(T => T.name == "26");
                break;
            case "eewe":
                tileData.sprite = WaterSprites.Find(T => T.name == "29");
                break;
            case "eeww":
                if(composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "17");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "18");
                }
                break;
            case "ewee":
                tileData.sprite = WaterSprites.Find(T => T.name == "30");
                break;
            case "ewew":
                if (composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "1");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "2");
                }
                break;
            case "ewwe":
                tileData.sprite = WaterSprites.Find(T => T.name == "28");
                break;
            case "ewww":
                if (composition[(int)DirectionIndex.RightBottom] == 'w' && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "7");
                }
                else if(composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "9");
                }
                else if (composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "8");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "10");
                }
                break;
            case "weee":
                tileData.sprite = WaterSprites.Find(T => T.name == "25");
                break;
            case "weew":
                tileData.sprite = WaterSprites.Find(T => T.name == "27");
                break;
            case "wewe":
                if (composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "23");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "24");
                }
                break;
            case "weww":
                if (composition[(int)DirectionIndex.LeftTop] == 'w' && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "19");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "20");
                }
                else if (composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "21");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "22");
                }
                break;
            case "wwee":
                if (composition[(int)DirectionIndex.LeftBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "4");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "5");
                }
                break;
            case "wwew":
                if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "3");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "14");
                }
                else if (composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "6");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "16");
                }
                break;
            case "wwwe":
                if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "12");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "13");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "11");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "15");
                }
                break;
            case "wwww":

                if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.LeftTop] == 'w' 
                    && composition[(int)DirectionIndex.RightBottom] == 'w' && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    System.Random r = new System.Random(DateTime.Now.Millisecond);
                    if (!needsCorner)
                    {
                        switch (r.Next(0, 10))
                        {
                            case 0:
                                tileData.sprite = WaterSprites.Find(T => T.name == "46");
                                break;
                            case 1:
                            case 2:
                                tileData.sprite = WaterSprites.Find(T => T.name == "48");
                                break;
                            default:
                                tileData.sprite = WaterSprites.Find(T => T.name == "47");
                                break;
                        }
                    }
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.LeftTop] == 'w'
                    && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "36");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.LeftTop] == 'w'
                    && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "34");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w'
                    && composition[(int)DirectionIndex.RightTop] == 'w' && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "38");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w'
                    && composition[(int)DirectionIndex.RightTop] == 'w' && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "37");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "41");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w' && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "31");
                }
                else if (composition[(int)DirectionIndex.RightTop] == 'w' && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "33");
                }
                else if (composition[(int)DirectionIndex.RightBottom] == 'w' && composition[(int)DirectionIndex.LeftBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "35");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w' && composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "42");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w' && composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "39");
                }
                else if (composition[(int)DirectionIndex.LeftBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "44");
                }
                else if (composition[(int)DirectionIndex.LeftTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "40");
                }
                else if (composition[(int)DirectionIndex.RightTop] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "43");
                }
                else if (composition[(int)DirectionIndex.RightBottom] == 'w')
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "32");
                }
                else
                {
                    tileData.sprite = WaterSprites.Find(T => T.name == "45");
                }
                break;
            default:
                tileData.sprite = WaterSprites.Find(T => T.name == "0");
                break;
        }
        #endregion

        tileData.colliderType = ColliderType.Sprite;
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        return false;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WaterTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Water Tile", "New Watertile", "asset", "Save watertile", "Assets");
        if(path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WaterTile>(), path);
    }
#endif

}

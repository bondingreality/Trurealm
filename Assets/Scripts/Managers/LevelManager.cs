using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite defaultTile;

    private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();

    [SerializeField]
    private SpriteAtlas waterAtlas;

    private Vector3 WorldStartPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {
        int height = mapData[0].height;
        int width = mapData[0].width;

        for (int i = 0; i < mapData.Length; i++)
        {
            for(int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color c = mapData[i].GetPixel(x, y);

                    MapElement newElement = Array.Find(mapElements, e => e.MyTileColor == c);

                    if(newElement != null)
                    {
                        float xPos = WorldStartPosition.x + (defaultTile.bounds.size.x * x);
                        float yPos = WorldStartPosition.y + (defaultTile.bounds.size.y * y);
                        GameObject go = Instantiate(newElement.MyElementPrefab);
                        go.transform.position = new Vector2(xPos, yPos);

                        if(newElement.MyTileTag == "Tree")
                        {
                            go.GetComponent<SpriteRenderer>().sortingOrder = height * 2 - y *2;
                        }
                        if (newElement.MyTileTag == "Water")
                        {
                            waterTiles.Add(new Point(x, y), go);
                        }
                        go.transform.parent = map;
                    }
                }
            }
        }

        CheckWater();
    }

    private void CheckWater()
    {
        foreach (var tile in waterTiles)
        {
            string composition = TileCheck(tile.Key);
            string edgeCheckString = string.Concat(composition[1], composition[3], composition[4], composition[6]);
            bool needsCorner = false;

            #region corners
            //Top Right Corner
            if (composition[1] == 'w' && composition[2] == 'e' && composition[4] == 'w')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("15");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
                needsCorner = true;
            }
            //Top Left Corner
            if (composition[4] == 'w' && composition[7] == 'e' && composition[6] == 'w')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("16");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
                needsCorner = true;
            }
            //Bottom Right Corner
            if (composition[1] == 'w' && composition[0] == 'e' && composition[3] == 'w')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("17");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
                needsCorner = true;
            }
            //Bottom Left Corner
            if (composition[3] == 'w' && composition[5] == 'e' && composition[6] == 'w')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("14");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
                needsCorner = true;
            }
            #endregion

            #region edges
            switch (edgeCheckString)
            {
                case "eeee":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("0");
                    break;
                case "eeew":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("26");
                    break;
                case "eewe":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("29");
                    break;
                case "eeww":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("17");
                    break;
                case "ewee":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("30");
                    break;
                case "ewew":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("1");
                    break;
                case "ewwe":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("28");
                    break;
                case "ewww":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("7");
                    break;
                case "weee":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("25");
                    break;
                case "weew":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("27");
                    break;
                case "wewe":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("23");
                    break;
                case "weww":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("19");
                    break;
                case "wwee":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("4");
                    break;
                case "wwew":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("3");
                    break;
                case "wwwe":
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("12");
                    break;
                case "wwww":
                    System.Random r = new System.Random(DateTime.Now.Millisecond);
                    if (!needsCorner)
                    {
                        switch (r.Next(0, 5))
                        {
                            case 0:
                                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("46");
                                break;
                            case 1:
                            case 2:
                                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("48");
                                break;
                            default:
                                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("47");
                                break;
                        }
                    }
                    else
                    {
                        tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("47");
                    }
                    break;
            }
            #endregion

        }
    }

    public string TileCheck(Point curPos)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x != 0 || y != 0)
                {
                    if(waterTiles.ContainsKey(new Point(curPos.X + x, curPos.Y + y)))
                    {
                        composition += "w";
                    }else
                    {
                        composition += "e";
                    }
                }
            }
        }

        return composition;
    }
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
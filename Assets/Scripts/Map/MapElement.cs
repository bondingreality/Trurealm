using System;
using UnityEngine;

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color tileColor;

    [SerializeField]
    private GameObject elementPrefab;

    public string MyTileTag
    {
        get
        {
            return tileTag;
        }
    }
    public Color MyTileColor
    {
        get
        {
            return tileColor;
        }
    }
    public GameObject MyElementPrefab
    {
        get
        {
            return elementPrefab;
        }
    }

}

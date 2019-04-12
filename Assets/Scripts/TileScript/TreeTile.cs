using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTile : Tile
{
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        try
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = -position.y * 2;
        }
        catch (Exception) { }
        return base.StartUp(position, tilemap, go);
    }
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/TreeTile")]
    public static void CreateTreeTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Tree Tile", "New Treetile", "asset", "Save treetile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TreeTile>(), path);
    }
#endif
}

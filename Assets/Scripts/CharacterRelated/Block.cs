using System;
using UnityEngine;

[Serializable]
public class Block
{
    [SerializeField]
    private GameObject[] blocks;

    public void Deactivate()
    {
        foreach(GameObject go in blocks)
        {
            go.SetActive(false);
        }
    }
    public void Activate()
    {
        foreach (GameObject go in blocks)
        {
            go.SetActive(true);
        }
    }
}
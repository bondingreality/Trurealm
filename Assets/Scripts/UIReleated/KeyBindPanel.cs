using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBindPanel : MonoBehaviour
{
    private static KeyBindPanel instance;
    public static KeyBindPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBindPanel>();
            }
            return instance;
        }
    }
    [SerializeField]
    private CanvasGroup canvasGroup;
    public CanvasGroup MyCanvasGroup
    {
        get
        {
            return canvasGroup;
        }
    }

    public void OpenClose()
    {
        MyCanvasGroup.alpha = MyCanvasGroup.alpha > 0 ? 0 : 1;
        MyCanvasGroup.blocksRaycasts = MyCanvasGroup.alpha > 0 ? true : false; //Block raycast based on current alpha
    }
}
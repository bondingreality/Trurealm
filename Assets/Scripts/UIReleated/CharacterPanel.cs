using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;
    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0 ? true : false; //Block raycast based on current alpha
    }
}

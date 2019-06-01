using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private List<CharButton> charButtons;

    public CharButton MySelectedButton { get; set; }

    public CanvasGroup MyCanvasGroup
    {
        get
        {
            return canvasGroup;
        }
    }

    public void OpenClose()
    {
        UIManager.MyInstance.ShowCanvasGroup(MyCanvasGroup);
    }

    public void EquipArmor(Armor armor)
    {
        var myButtons = charButtons.Where(T => T.MyArmorSlot == armor.MyArmorSlot);
        if (myButtons.Count() == 0)
            return;

        foreach (var btn in myButtons)
        {
            btn.EquipArmor(armor);
        }
    }
}

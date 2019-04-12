using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string title;

    [SerializeField]
    private RarityLevel rarity;

    [SerializeField]
    private int stackSize;

    private SlotScript slotScript;

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlotScript
    {
        get
        {
            return slotScript;
        }

        set
        {
            slotScript = value;
        }
    }

    public RarityLevel MyRarity
    {
        get
        {
            return rarity;
        }
    }


    public string GetTitle()
    {
        string color = RarityLevelBase.GetColor(MyRarity);
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("<color={0}>", color));
        sb.AppendLine(title);
        sb.Append("</color>");
        return sb.ToString();
    }
    public virtual string GetDescription()
    {
        string color = RarityLevelBase.GetColor(MyRarity);
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("<color={0}>",  color));
        sb.Append(GetTitle());
        sb.AppendLine(string.Format("<size=9>{0}</size>", MyRarity));
        if(MyStackSize > 0)
            sb.AppendLine(string.Format("<size=9>Max Stack: {0}</size>", MyStackSize));
        sb.Append("</color>");
        return sb.ToString();
    }

    public void Remove()
    {
        if(MySlotScript != null)
        {
            MySlotScript.RemoveItem(this);
        }
    }
}

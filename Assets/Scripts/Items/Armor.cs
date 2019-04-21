using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

enum ArmorType
{
    Helmet = 1 << 1,
    Shoulders = 1 << 2,
    Chest = 1 << 3,
    Bracers = 1 << 4,
    Gloves = 1 << 5,
    Belt = 1 << 6,
    Pants = 1 << 7,
    Boots = 1 << 8,
    Ring = 1 << 9,
    Necklace = 1 << 10,
    Earring = 1 << 11,
    OneHand = 1 << 12,
    TwoHand = 1 << 13,
    OffHand = 1 << 14,
    Ranged = 1 << 15
}
enum ArmorSlot
{
    Head = 1 << 1,
    Shoulders = 1 << 2,
    Chest = 1 << 3,
    Wrists = 1 << 4,
    Hands = 1 << 5,
    Waist = 1 << 6,
    Legs = 1 << 7,
    Feet = 1 << 8,
    Finger = 1 << 9,
    Neck = 1 << 10,
    Ear = 1 << 11,
    HandHeld = 1 << 12
}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private ArmorSlot armorSlot;

    [SerializeField]
    private bool IsUseable;

    [SerializeField]
    private List<CStat> statEnhancements = new List<CStat>();

    [SerializeField]
    private AnimationClip[] animationClips;

    internal ArmorType MyArmorType
    {
        get
        {
            return armorType;
        }

        set
        {
            armorType = value;
        }
    }
    internal ArmorSlot MyArmorSlot
    {
        get
        {
            return armorSlot;
        }

        set
        {
            armorSlot = value;
        }
    }

    public AnimationClip[] MyAnimationClips
    {
        get
        {
            return animationClips;
        }
    }

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(base.GetDescription());
        sb.Append("<size=7>");
        sb.AppendLine(string.Format("{0}", MyArmorType));
        sb.Append("</size>");
        sb.Append("<size=10>");
        foreach (CStat enhancement in statEnhancements)
        {
            sb.AppendLine();
            sb.Append(enhancement.GetDescription());
        }
        sb.Append("</size>");
        return sb.ToString();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipArmor(this);
    }
}

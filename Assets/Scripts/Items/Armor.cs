using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

enum ArmorType
{
    Helmet = 1 << 1,
    Shoulders = 1 << 2,
    Chest = 1 <<3,
    Bracers= 1 <<4,
    Gloves= 1 <<5,
    Pants= 1 <<6,
    Feet= 1 <<7,
    Ring = 1 <<8,
    Necklace = 1 << 9,
    Earring = 1 << 10
}
enum WeaponType
{
    OneHand = 1 << 1,
    TwoHand = 1 << 2,
    OffHand = 1 << 3,
    Ranged = 1 << 4
}
enum ArmorSlots
{
    Helmet = 1 << 1,
    Shoulders = 1 << 2,
    Chest = 1 << 3,
    Bracers = 1 << 4,
    Gloves = 1 << 5,
    Pants = 1 << 6,
    Feet = 1 << 7,
    Ring_RightThumb = 1 << 8,
    Ring_RightPointer = 1 << 9,
    Ring_RightIndex = 1 << 10,
    Ring_RightRing = 1 << 11,
    Ring_RightPinky = 1 << 12,
    Ring_LeftThumb = 1 << 13,
    Ring_LeftPointer = 1 << 14,
    Ring_LeftIndex = 1 << 15,
    Ring_LeftRing = 1 << 16,
    Ring_LeftPinky = 1 << 17,
    Necklace = 1 << 18,
    Ear_Left = 1 << 19,
    Ear_Right = 1 << 20
}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private List<CStat> statEnhancements = new List<CStat>();

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(base.GetDescription());
        sb.Append("<size=7>");
        sb.AppendLine(string.Format("{0}", armorType));
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
}

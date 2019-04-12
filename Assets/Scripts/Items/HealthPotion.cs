using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName ="Items/Potion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;
    public void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }
    }

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.GetDescription());
        sb.Append(string.Format("Use: Restores {0} points of health", health));
        return sb.ToString();
    }
}

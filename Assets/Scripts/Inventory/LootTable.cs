using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot;

    private bool rolled = false;
    private List<Item> droppedItems = new List<Item>();
    
    public void ShowLoot()
    {
        RollLoot();
        LootWindow.MyInstance.CreatePages(droppedItems);
    }

    private void RollLoot()
    {
        if (!rolled)
        {
            foreach (Loot item in loot)
            {
                int roll = Random.Range(0, 100);

                if (roll <= item.MyDropChance)
                {
                    droppedItems.Add(item.MyItem);
                }
            }
            rolled = true;
        }
    }
}

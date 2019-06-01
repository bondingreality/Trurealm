using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<SlotScript> slots = new List<SlotScript>();
    public List<SlotScript> MySlots
    {
        get
        {
            return slots;
        }
    }
    public int MyFilledSlotCount
    {
        get
        {
            return MySlots.Count(T => !T.IsEmpty);
        }
    }
    public int MyEmptySlotCount
    {
        get
        {
            return MySlots.Count(T => T.IsEmpty);
        }
    }

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public List<Item> GetItems()
    {
        List<Item> returnList = new List<Item>();
        foreach(SlotScript slot in slots)
        {
            if (!slot.IsEmpty)
            {
                returnList.AddRange(slot.MyItems);
            }
        }
        return returnList;
    }
    public void AddSlots(int slotCount)
    {
        for(int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slot.MyBag = this;
            slots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in slots)
        {
            if(slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = IsOpen;
    }

    public void Clear()
    {
        foreach(var slot in slots)
        {
            slot.Clear();
        }
    }
}

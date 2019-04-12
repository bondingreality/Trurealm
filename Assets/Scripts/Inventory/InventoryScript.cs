using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public delegate void ItemCountChanged(Item item);
public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;
    
    private static InventoryScript instance;
    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private List<BagButton> bagButtons;

    //DEBUG ONLY
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 5; }
    }
    public int MyEmptySlotCount
    {
        get
        {
            return bags.Sum(T => T.MyBagScript.MyEmptySlotCount);
        }
    }
    public int MyFilledSlotCount
    {
        get
        {
            return bags.Sum(T => T.MyBagScript.MyFilledSlotCount);
        }
    }
    public int MyTotalSlotCount
    {
        get
        {
            return bags.Sum(T => T.MyBagScript.MySlots.Count);
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;
            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[2]);
        bag.Initialize(16);
        bag.Use();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[2]);
            bag.Use();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[1]);
            AddItem(bag);
            bag = (Bag)Instantiate(items[2]);
            AddItem(bag);
            bag = (Bag)Instantiate(items[3]);
            AddItem(bag);
            bag = (Bag)Instantiate(items[4]);
            AddItem(bag);
            bag = (Bag)Instantiate(items[5]);
            AddItem(bag);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion pot = (HealthPotion)Instantiate(items[0]);
            AddItem(pot);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            Armor helmet = (Armor)Instantiate(items[6]);
            AddItem(helmet);
        }
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            GameInfoObject slvls = (GameInfoObject)Instantiate(items[7]);
            AddItem(slvls);
            GameInfoObject qlvls = (GameInfoObject)Instantiate(items[8]);
            AddItem(qlvls);
            GameInfoObject rlvls = (GameInfoObject)Instantiate(items[9]);
            AddItem(rlvls);
        }
    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagbtn in bagButtons)
        {
            if(bagbtn.MyBag == null)
            {
                bagbtn.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagbtn;
                break;
            }
        }
    }
    public void AddBag(Bag bag, BagButton bagButton)
    {
        bagButton.MyBag = bag;
        bags.Add(bag);
    }
    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }
    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = MyTotalSlotCount - oldBag.MySlots + newBag.MySlots;

        if(newSlotCount - MyFilledSlotCount >= 0)
        {
            List<Item> bagItems = oldBag.MyBagScript.GetItems();
            RemoveBag(oldBag);
            newBag.MyBagButton = oldBag.MyBagButton;
            newBag.Use();
            foreach(Item curItem in bagItems)
            {
                if(curItem != newBag) 
                    AddItem(curItem);
            }
            AddItem(oldBag);
            Hand.MyInstance.Drop();
            MyInstance.fromSlot = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0 && PlaceInStack(item))
                return true;

        return PlaceInEmpty(item);
    }
    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag b in bags)
        {
            if (b.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }
    private bool PlaceInStack(Item item)
    {
        foreach (Bag b in bags)
        {
            foreach (SlotScript s in b.MyBagScript.MySlots)
            {
                if(s.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(T => !T.MyBagScript.IsOpen);

        foreach (Bag b in bags)
        {
            if (b.MyBagScript.IsOpen != closedBag)
            {
                b.MyBagScript.OpenClose();
            }
        }
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in bags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if(!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach(Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }
    
    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}

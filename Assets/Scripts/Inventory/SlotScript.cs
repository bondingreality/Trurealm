using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();
    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSizeText;

    [SerializeField]
    public BagScript MyBag { get; set; }

    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool IsEmpty
    {
        get
        {
            return MyItems.Count == 0;
        }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty)
                return false;

            if (MyCount < MyItem.MyStackSize)
                return false;

            return true;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return MyItems.Count;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSizeText;
        }
    }

    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlotScript = this;
        return true;
    }
    private bool AddItems(ObservableStack<Item> newItems)
    {
        if(IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;
            for(int i = 0; i < count; i++)
            {
                if (IsFull)
                    return false;

                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }
    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            MyItems.Clear();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                if (Hand.MyInstance.MyMoveable != null)
                {
                    if (Hand.MyInstance.MyMoveable is Bag) {
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(Hand.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else if (Hand.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorSlot == (Hand.MyInstance.MyMoveable as Armor).MyArmorSlot)
                        {
                            (MyItem as Armor).Equip();
                            Hand.MyInstance.Drop();
                        }
                    }
                }
                else {
                    Hand.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (Hand.MyInstance.MyMoveable is Bag)
                {
                    Bag item = Hand.MyInstance.MyMoveable as Bag;

                    if (item.MyBagButton != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - item.MySlots > 0)
                    {
                        AddItem(item);
                        item.MyBagButton.RemoveBag();
                        Hand.MyInstance.Drop();
                    }
                }
                else if (Hand.MyInstance.MyMoveable is Armor)
                {
                    Armor item = Hand.MyInstance.MyMoveable as Armor;

                    if (AddItem(item))
                    {
                        CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                        Hand.MyInstance.Drop();
                    }
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null)
            {
                if (PutItemBack()
                    || MergeItems(InventoryScript.MyInstance.FromSlot)
                    || SwapItems(InventoryScript.MyInstance.FromSlot)
                    || AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
                {
                    Hand.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right && Hand.MyInstance.MyMoveable == null)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if(MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlotScript = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if(InventoryScript.MyInstance.FromSlot == this)
        {
            MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }

        if (from.MyItem.GetType() != MyItem.GetType() && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;

            for(int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }
            return true;
        }
        return false;
    }
    private bool SwapItems(SlotScript from)
    {
        if(IsEmpty)
        {
            return false;
        }

        if(from.MyItem.GetType() != MyItem.GetType() || from.IsFull  || IsFull)
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);
            from.MyItems.Clear();
            from.AddItems(MyItems);
            MyItems.Clear();
            AddItems(tmpFrom);
            return true;
        }
        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!IsEmpty)
        {
            UIManager.MyInstance.ShowToolTip(transform.position, MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}

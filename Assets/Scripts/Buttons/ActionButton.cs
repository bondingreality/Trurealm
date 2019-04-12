using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    public IUseable MyUseable { get; set; }

    [SerializeField]
    private Text stackSize;
    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    private Stack<IUseable> useables = new Stack<IUseable>();
    public Stack<IUseable> MyUseables
    {
        get
        {
            return useables;
        }

        set
        {
            if (value.Count > 0)
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }

            useables = value;
        }
    }

    private int count;
    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public Button MyButton { get; private set; }

    [SerializeField]
    private Image icon;
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




    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (Hand.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            else if (MyUseables != null && MyUseables.Count > 0)
            {
                MyUseables.Peek().Use();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Hand.MyInstance.MyMoveable != null && Hand.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(Hand.MyInstance.MyMoveable as IUseable);
            }
        }
    }
    
    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = InventoryScript.MyInstance.GetUseables(useable);
            if (InventoryScript.MyInstance.FromSlot != null)
            {
                InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
                InventoryScript.MyInstance.FromSlot = null;
            }
        }
        else
        {
            MyUseables.Clear();
            this.MyUseable = useable;
        }

        count = MyUseables.Count;
        UpdateVisual(useable as IMoveable);
    }

    public void UpdateVisual(IMoveable moveable)
    {
        if (Hand.MyInstance.MyMoveable != null)
        {
            Hand.MyInstance.Drop();
        }

        MyIcon.sprite = moveable.MyIcon;
        MyIcon.color = Color.white;
        
        if(count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if(item is IUseable && MyUseables.Count > 0)
        {
            if(MyUseables.Peek().GetType() == item.GetType())
            {
                MyUseables = InventoryScript.MyInstance.GetUseables(item as IUseable);
                count = MyUseables.Count;
                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MyUseable != null && MyUseable is IDescribable)
        {
            UIManager.MyInstance.ShowToolTip(transform.position, MyUseable as IDescribable);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}

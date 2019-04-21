using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ArmorSlot armorSlot;
    
    private Armor equippedArmor;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private GearSocket gearSocket;

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


    public void EquipArmor(Armor armor)
    {
        armor.Remove();

        if(equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                armor.MySlotScript.AddItem(equippedArmor);
                UIManager.MyInstance.RefreshToolTip(equippedArmor);
            }
        }
        else
        {
            UIManager.MyInstance.HideToolTip();
        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        this.equippedArmor = armor;

        if (Hand.MyInstance.MyMoveable == (armor as IMoveable))
        {
            Hand.MyInstance.Drop();
        }

        if (gearSocket != null && equippedArmor.MyAnimationClips != null && equippedArmor.MyAnimationClips.Count() > 0)
        {
            gearSocket.Equip(equippedArmor.MyAnimationClips);
        }
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;

        if (gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Dequip();
        }

        equippedArmor = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Hand.MyInstance.MyMoveable is Armor)
            {
                Armor inHandArmor = (Armor)Hand.MyInstance.MyMoveable;

                if (inHandArmor.MyArmorSlot == MyArmorSlot)
                {
                    EquipArmor(inHandArmor);
                }

                UIManager.MyInstance.RefreshToolTip(equippedArmor);
            }
        }
        else if (Hand.MyInstance.MyMoveable == null && equippedArmor != null)
        {
            Hand.MyInstance.TakeMoveable(equippedArmor);
            CharacterPanel.MyInstance.MySelectedButton = this;
            icon.color = Color.gray;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(equippedArmor != null)
        {
            UIManager.MyInstance.ShowToolTip(new Vector2(0, 0), transform.position, equippedArmor);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}

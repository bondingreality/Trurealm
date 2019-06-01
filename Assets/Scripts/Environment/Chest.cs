using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private Sprite openSprite, closeSprite;

    private bool isOpen;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private List<Item> items;

    [SerializeField]
    private BagScript bag;

    public void Interact()
    {
        if(isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;
            SpriteRenderer.sprite = openSprite;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        StoreItems();
        bag.Clear();
        isOpen = false;
        SpriteRenderer.sprite = closeSprite;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void AddItems()
    {
        if(items != null)
        {
            foreach (var item in items)
            {
                item.MySlotScript.AddItem(item);
            }
        }
    }

    public void StoreItems()
    {
        items = bag.GetItems();
    }
}

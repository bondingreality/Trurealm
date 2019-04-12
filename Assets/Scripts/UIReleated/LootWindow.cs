using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;
    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LootWindow>();
            }

            return instance;
        }
    }

    [SerializeField]
    private LootButton[] lootButtons;

    private CanvasGroup canvasGroup;

    private List<List<Item>> lootPages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private int maxLootPerPage = 4;
    private int lootPageIndex = 0;

    [SerializeField]
    private Text pageNumber;
    [SerializeField]
    private GameObject prevBtn, nextBtn;

    [SerializeField]
    private Item[] items;

    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0; }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        if (droppedLoot.Count > 0)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void Close()
    {
        lootPages.Clear();
        ClearButtons();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void CreatePages(List<Item> dropItems)
    {
        if (!IsOpen)
        {
            List<Item> curPage = new List<Item>();

            droppedLoot = dropItems;

            foreach (Item i in dropItems)
            {
                if (curPage.Count == maxLootPerPage)
                {
                    lootPages.Add(curPage);
                    curPage = new List<Item>();
                }
                curPage.Add(i);
            }
            lootPages.Add(curPage);

            AddLoot();

            Open();
        }
    }
    private void AddLoot()
    {
        if (lootPages.Count > 0)
        {
            ClearButtons();
            pageNumber.text = string.Concat(lootPageIndex + 1, "/", lootPages.Count);
            prevBtn.SetActive(lootPageIndex > 0);
            nextBtn.SetActive(lootPages.Count > 1 && lootPageIndex < lootPages.Count - 1);

            int i = 0;
            foreach(Item item in lootPages[lootPageIndex])
            {
                lootButtons[i].MyLoot = item;
                lootButtons[i].MyIcon.sprite = item.MyIcon;
                lootButtons[i].gameObject.SetActive(true);
                lootButtons[i].MyTitle.text = item.GetTitle();
                i += 1;
            }
        }
    }

    public void ClearButtons()
    {
        foreach(LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if(lootPageIndex < lootPages.Count - 1)
        {
            lootPageIndex++;
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if (lootPageIndex > 0)
        {
            lootPageIndex--;
            AddLoot();
        }
    }

    public void TakeLoot(Item lootItem)
    {
        var curPage = lootPages[lootPageIndex];
        curPage.Remove(lootItem);

        droppedLoot.Remove(lootItem);

        if (droppedLoot.Count == 0)
        {
            Close();
        }
        else
        {
            if (curPage.Count == 0)
            {
                lootPages.Remove(curPage);

                if (lootPageIndex == lootPages.Count && lootPageIndex > 0)
                {
                    lootPageIndex--;
                }

                AddLoot();
            }
        }
    }
}

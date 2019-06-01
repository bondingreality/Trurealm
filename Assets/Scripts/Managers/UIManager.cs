﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private Image targetPortrait;


    private Stat healthStat;

    [SerializeField]
    private CanvasGroup characterPanel;
    [SerializeField]
    private CanvasGroup keybindMenu;
    [SerializeField]
    private CanvasGroup spellBook;
    [SerializeField]
    private GameObject toolTip;
    
    private Text toolTipText;

    [SerializeField]
    private RectTransform toolTipRect;

    private GameObject[] keybindButtons;

    public void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        toolTipText = toolTip.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();
    }

    private List<CanvasGroup> MenuUp = new List<CanvasGroup>();
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCanvasGroup(keybindMenu);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowCanvasGroup(spellBook);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ShowCanvasGroup(characterPanel);
        }
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        target.healthChanged += new HealthChanged(UpdateTargetFrame);
        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
        targetPortrait.sprite = target.Portrait;
        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
    }
    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);

    }
    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0 ? true : false; //Block raycast based on current alpha
        if (canvasGroup.blocksRaycasts)
            MenuUp.Add(canvasGroup);
        else
            MenuUp.Remove(canvasGroup);
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = MenuUp.Count > 0 ? 0 : 1;
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            ClearStackCount(clickable);
        }

        if(clickable.MyCount == 0)
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
        }
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyIcon.color = Color.white;
        clickable.MyStackText.color = new Color(0, 0, 0, 0);
    }

    public void ShowToolTip(Vector3 position, IDescribable description)
    {
        toolTip.SetActive(true);
        toolTip.transform.position = position;
        toolTipText.text = description.GetDescription();
    }
    public void ShowToolTip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        toolTipRect.pivot = pivot;
        toolTip.SetActive(true);
        toolTip.transform.position = position;
        toolTipText.text = description.GetDescription();
    }
    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    public void RefreshToolTip(IDescribable description)
    {
        toolTipText.text = description.GetDescription();
    }
}

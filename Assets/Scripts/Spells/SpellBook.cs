using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;
    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image castingIcon;
    [SerializeField]
    private Image castingBarFill;
    [SerializeField]
    private Text castingName;
    [SerializeField]
    private Text castingTime;
    [SerializeField]
    private CanvasGroup castingBar;


    [SerializeField]
    private Spell[] spells;


    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        castingIcon.sprite = spell.MyIcon;
        castingBarFill.fillAmount = 0;
        castingBarFill.color = spell.MyBarColor;
        castingName.text = spell.MyName;
        castingTime.text = spell.MyCastTime.ToString();

        spellRoutine = StartCoroutine(Progress(spell));
        fadeRoutine = StartCoroutine(FadeBar());

        return spell;
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / .25f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castingBar.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator Progress (Spell spell)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / spell.MyCastTime;

        float progress = 0.0f;

        while(progress <= 1.0)
        {
            castingBarFill.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            float timeRemaining = spell.MyCastTime - timePassed;
            if (timeRemaining < 0)
                timeRemaining = 0;

            castingTime.text = timeRemaining.ToString("F2");

            yield return null; 
        }

        StopSpell();
    }

    public void StopSpell()
    {
        if(spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
        if (fadeRoutine != null)
        {
            castingBar.alpha = 0;
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        return Array.Find(spells, x => x.MyName == spellName);
    }
}

using System;
using System.Text;
using UnityEngine;


[Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damageMax;

    [SerializeField]
    private int damageMin;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Color barColor;

    [SerializeField]
    private string description;

    public int damage
    {
        get
        {
            System.Random r = new System.Random(DateTime.Now.Millisecond);
            //for(int i = 0; i <100; i++)
            //{
            //    Debug.Log(r.Next(MyDamageMin*100, MyDamageMax*100));
            //}

            return r.Next(MyDamageMin, MyDamageMax+1); 
        }
    }

    #region Properties
    public string MyName
    {
        get
        {
            return name;
        }
    }

    public int MyDamageMax
    {
        get
        {
            return damageMax;
        }
    }

    public int MyDamageMin
    {
        get
        {
            return damageMin;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public float MySpeed
    {
        get
        {
            return speed;
        }
    }

    public float MyCastTime
    {
        get
        {
            return castTime;
        }
    }
    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }
    }

    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }

    #endregion Properties

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
    public string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("{0}", MyName));
        sb.AppendLine(string.Format("Damage: {0}-{1}", MyDamageMin, MyDamageMax));
        sb.AppendLine(string.Format("DPS: {0}-{1}", Math.Round(MyDamageMin / MyCastTime, 2), Math.Round(MyDamageMax / MyCastTime, 2)));
        sb.AppendLine(string.Format("Cast Time: {0} second(s)", MyCastTime));
        sb.AppendLine(string.Format("{0}", description));
        return sb.ToString();
    }

}

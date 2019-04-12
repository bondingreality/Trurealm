using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class CStatModifier : IDescribable
{
    [SerializeField]
    private float value;
    [SerializeField]
    private StatModProperty property;
    [SerializeField]
    private StatModType type;
    [SerializeField]
    private StatModEffect effect;
    [SerializeField]
    private int order;
    [SerializeField]
    private object source;

    public float Value
    {
        get
        {
            return value;
        }
    }

    public StatModProperty Property
    {
        get
        {
            return property;
        }
    }

    public StatModType Type
    {
        get
        {
            return type;
        }
    }

    public StatModEffect Effect
    {
        get
        {
            return effect;
        }
    }

    public int Order
    {
        get
        {
            return order;
        }
    }

    public object Source
    {
        get
        {
            return source;
        }
    }

    public CStatModifier(StatModProperty property, StatModType type, StatModEffect effect, float value, int order, object source)
    {
        this.property = property;
        this.value = value;
        this.effect = effect;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    public CStatModifier(StatModProperty property, StatModType type, StatModEffect effect, float value) : this(property, type, effect, value, (int)type, null) { }

    public CStatModifier(StatModProperty property, StatModType type, StatModEffect effect, float value, int order) : this(property, type, effect, value, order, null) { }

    public CStatModifier(StatModProperty property, StatModType type, StatModEffect effect, float value,object source) : this(property, type, effect, value, (int)type, source) { }

    public string GetDescription()
    {
        string eff = string.Empty;
        switch (Effect)
        {
            case StatModEffect.Add:
                eff = "+";
                break;
            case StatModEffect.Subtract:
                eff = "-";
                break;
            case StatModEffect.Multiply:
                eff = "*";
                break;
            case StatModEffect.Divide:
                eff = "/";
                break;
        }
        string val = Value.ToString();
        string typ = string.Empty;
        switch (Type)
        {
            case StatModType.Flat:
                break;
            case StatModType.Percent:
                val = (Value * 100).ToString();
                typ = "%";
                break;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("{0}{1}{2}", eff, val, typ));
        return sb.ToString();
    }
}

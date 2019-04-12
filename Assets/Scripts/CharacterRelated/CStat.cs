using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;


[Serializable]
public class CStat : IDescribable
{
    [SerializeField]
    private PrimaryStats type;
    protected PrimaryStats MyType
    {
        get { return type; }
    }
    public float MyMaxValue {
        get
        {
            return (float)Enum.GetValues(typeof(PrimaryStats)).Cast<PrimaryStats>().Last();
        }
    }
    public float MyMinValue
    {
        get
        {
            return (float)Enum.GetValues(typeof(PrimaryStats)).Cast<PrimaryStats>().First();
        }
    }

    [SerializeField]
    private float baseValue;
    public float MyBaseValue
    {
        get { return baseValue; }
    }
    public StatLevel GetRating
    {
        get
        {
            int curVal = (int)(MyBaseValue / 100) * 100;
            return (StatLevel)curVal;
        }
    }

    protected bool isDirty = true;
    protected float lastBaseValue;

    protected float _value;
    public virtual float MyValue
    {
        get
        {
            if (isDirty || lastBaseValue != MyBaseValue)
            {
                lastBaseValue = MyBaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    [SerializeField]
    private List<CStatModifier> statModifiers = new List<CStatModifier>();
    public readonly ReadOnlyCollection<CStatModifier> StatModifiers;

    public CStat()
    {
        statModifiers = new List<CStatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CStat(float baseValue) : this()
    {
        this.baseValue = baseValue;
    }

    public virtual void AddModifier(CStatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
    }

    public virtual bool RemoveModifier(CStatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

        if (numRemovals > 0)
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    protected virtual int CompareModifierOrder(CStatModifier a, CStatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; //if (a.Order == b.Order)
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = MyBaseValue;
        float sumPercentAdd = 0;

        statModifiers.Sort(CompareModifierOrder);

        for (int i = 0; i < statModifiers.Count; i++)
        {
            CStatModifier mod = statModifiers[i];
            switch (mod.Type)
            {
                case StatModType.Flat:
                    finalValue += mod.Value;
                    break;
                case StatModType.Percent:
                    switch (mod.Effect)
                    {
                        case StatModEffect.Add:
                            sumPercentAdd += mod.Value;
                            if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.Percent  || StatModifiers[i + 1].Effect != StatModEffect.Add)
                            {
                                finalValue *= 1 + sumPercentAdd;
                                sumPercentAdd = 0;
                            }
                            break;
                        case StatModEffect.Subtract:
                            break;
                        case StatModEffect.Multiply:
                            finalValue *= 1 + mod.Value;
                            break;
                        case StatModEffect.Divide:
                            break;
                    }

                    break;
            }
        }

        // Workaround for float calculation errors, like displaying 12.00001 instead of 12
        return (float)Math.Round(finalValue, 4);
    }

    public string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("{0} +{1}", MyType, baseValue));
        foreach (CStatModifier mod in statModifiers)
        {
            sb.AppendLine();
            sb.Append(string.Format("{0} Modifier: {1}", MyType, mod.GetDescription()));
        }
        return sb.ToString();
    }
}
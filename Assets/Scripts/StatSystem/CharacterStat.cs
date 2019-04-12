using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Trurealm.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;

        protected bool isDirty = true;
        protected float lastBaseValue = float.MinValue;
        protected float _value;
        public virtual float Value
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }

        }


        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }
        public CharacterStat(float baseValue)
            : this()
        {
            this.BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }
        public virtual bool RemoveAllSourceModifiers(object source)
        {
            bool didRemove = false;

            if (statModifiers.RemoveAll(T => T.Source == source) > 0)
            {
                isDirty = true;
                didRemove = true;
            }

            return didRemove;
        }


        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }
        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float calculateModifier = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];
                switch (mod.Type)
                {
                    case StatModType.Flat:
                        finalValue += mod.Value;
                        break;
                    case StatModType.PercentAdd:
                        calculateModifier += mod.Value;

                        if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                        {
                            finalValue *= 1 + calculateModifier;
                            calculateModifier = 0;
                        }
                        break;
                    case StatModType.PercentMult:
                        finalValue *= 1 + mod.Value;
                        break;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}
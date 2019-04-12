using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class StatLevelBase
{
    private static Dictionary<StatLevel, Dictionary<StatLevelAttribute, object>> statLevelDictionary =
        new Dictionary<StatLevel, Dictionary<StatLevelAttribute, object>>()
        {
            {
                StatLevel.I,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#fe2712ff" },
                    {StatLevelAttribute.minRange, 1 },
                    {StatLevelAttribute.maxRange, 99 },
                    {StatLevelAttribute.reqExp2LvlExpo, 0 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 0 }
                }
            },
            {
                StatLevel.H,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#fd5308ff" },
                    {StatLevelAttribute.minRange, 100 },
                    {StatLevelAttribute.maxRange, 199 },
                    {StatLevelAttribute.reqExp2LvlExpo, 1 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 1 }
                }
            },
            {
                StatLevel.G,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#fb9902ff" },
                    {StatLevelAttribute.minRange, 200 },
                    {StatLevelAttribute.maxRange, 299 },
                    {StatLevelAttribute.reqExp2LvlExpo, 2 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 1 }
                }
            },
            {
                StatLevel.F,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#fabc02ff" },
                    {StatLevelAttribute.minRange, 300 },
                    {StatLevelAttribute.maxRange, 499 },
                    {StatLevelAttribute.reqExp2LvlExpo, 3 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 2 }
                }
            },
            {
                StatLevel.E,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#fefe33ff" },
                    {StatLevelAttribute.minRange, 500 },
                    {StatLevelAttribute.maxRange, 599 },
                    {StatLevelAttribute.reqExp2LvlExpo, 4 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 3 }
                }
            },
            {
                StatLevel.D,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#d0ea2bff" },
                    {StatLevelAttribute.minRange, 600 },
                    {StatLevelAttribute.maxRange, 699 },
                    {StatLevelAttribute.reqExp2LvlExpo, 5 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 5 }
                }
            },
            {
                StatLevel.C,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#66b032ff" },
                    {StatLevelAttribute.minRange, 700 },
                    {StatLevelAttribute.maxRange, 799 },
                    {StatLevelAttribute.reqExp2LvlExpo, 6 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 8 }
                }
            },
            {
                StatLevel.B,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#0391ceff" },
                    {StatLevelAttribute.minRange, 800 },
                    {StatLevelAttribute.maxRange, 899 },
                    {StatLevelAttribute.reqExp2LvlExpo, 7 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 13 }
                }
            },
            {
                StatLevel.A,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#0247feff" },
                    {StatLevelAttribute.minRange, 900 },
                    {StatLevelAttribute.maxRange, 999 },
                    {StatLevelAttribute.reqExp2LvlExpo, 8 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 21 }
                }
            },
            {
                StatLevel.S,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#3d01a4ff" },
                    {StatLevelAttribute.minRange, 1000 },
                    {StatLevelAttribute.maxRange, 1099 },
                    {StatLevelAttribute.reqExp2LvlExpo, 10 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 34 }
                }
            },
            {
                StatLevel.SS,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#8601afff" },
                    {StatLevelAttribute.minRange, 1100 },
                    {StatLevelAttribute.maxRange, 1199 },
                    {StatLevelAttribute.reqExp2LvlExpo, 13 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 55 }
                }
            },
            {
                StatLevel.SSS,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#a7194bff" },
                    {StatLevelAttribute.minRange, 1200 },
                    {StatLevelAttribute.maxRange, 1299 },
                    {StatLevelAttribute.reqExp2LvlExpo, 17},
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 89 }
                }
            },
            {
                StatLevel.Unique,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#bec2cbff" },
                    {StatLevelAttribute.minRange, 1300 },
                    {StatLevelAttribute.maxRange, 1399 },
                    {StatLevelAttribute.reqExp2LvlExpo, 22 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 144 }
                }
            },
            {
                StatLevel.Godly,
                new Dictionary<StatLevelAttribute, object>() {
                    {StatLevelAttribute.color, "#d4af37ff" },
                    {StatLevelAttribute.minRange, 1400 },
                    {StatLevelAttribute.maxRange, 1499 },
                    {StatLevelAttribute.reqExp2LvlExpo, 28 },
                    {StatLevelAttribute.onLvlUpBonusMultiplier, 233 }
                }
            }
        };
    public static string GetColor(StatLevel statLevel)
    {
        return statLevelDictionary.First(T => T.Key == statLevel).Value.First(T => T.Key == StatLevelAttribute.color).Value.ToString();
    }
    public static int GetMinRange(StatLevel statLevel)
    {
        return (int)statLevelDictionary.First(T => T.Key == statLevel).Value.First(T => T.Key == StatLevelAttribute.minRange).Value;
    }
    public static int GetMaxRange(StatLevel statLevel)
    {
        return (int)statLevelDictionary.First(T => T.Key == statLevel).Value.First(T => T.Key == StatLevelAttribute.maxRange).Value;
    }
    public static int GetReqExp2LvlExponent(StatLevel statLevel)
    {
        return (int)statLevelDictionary.First(T => T.Key == statLevel).Value.First(T => T.Key == StatLevelAttribute.reqExp2LvlExpo).Value;
    }
    public static int GetOnLvlUpBonusMultiplier(StatLevel statLevel)
    {
        return (int)statLevelDictionary.First(T => T.Key == statLevel).Value.First(T => T.Key == StatLevelAttribute.onLvlUpBonusMultiplier).Value;
    }
    public static StatLevel GetLevel(int value)
    {
        foreach(var item in statLevelDictionary)
        {
            try
            {
                int Min = GetMinRange(item.Key);
                int Max = GetMaxRange(item.Key);
                if (Min <= value && value <= Max)
                    return item.Key;
            }
            catch (Exception) {
                throw new InvalidProgramException(string.Format("The Stat Level: {0} is missing key attributes", item.Key));
            }
        }
        throw new InvalidProgramException("The provided stat level value is invalid");
    }
    public static string WriteStateLevelDictionary()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Stat Level Dictionary");
        sb.AppendLine("-------------------------");
        foreach (var item in statLevelDictionary)
        {
            StatLevel stat = item.Key;
            sb.AppendLine(string.Format("<color={0}>{1}</Color> Range:[{2} - {3}] ExpExpo:{4} OnLvlBonusMulti%:{5}", GetColor(stat), stat, GetMinRange(stat), GetMaxRange(stat), GetReqExp2LvlExponent(stat), GetOnLvlUpBonusMultiplier(stat)));
        }
        sb.AppendLine("-------------------------");
        return sb.ToString();
    }
}
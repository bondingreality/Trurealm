using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class QualityLevelBase
{
    private static Dictionary<QualityLevel, Dictionary<QualityAttribute, object>> qualityDictionary =
        new Dictionary<QualityLevel, Dictionary<QualityAttribute, object>>()
        {
            {
                QualityLevel.Unrepairable,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#a0a0a0ff" },
                    {QualityAttribute.minPercentage, -10000 },
                    {QualityAttribute.maxPercentage, -1000 }
                }
            },
            {QualityLevel.Broken,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#ff0000ff" },
                    {QualityAttribute.minPercentage, -999 },
                    {QualityAttribute.maxPercentage, 0 }
                }
            },
            {QualityLevel.Bad,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#ff8300ff" },
                    {QualityAttribute.minPercentage, 1 },
                    {QualityAttribute.maxPercentage, 10 }
                }
            },
            {QualityLevel.Poor,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#dbcb1cff" },
                    {QualityAttribute.minPercentage, 11 },
                    {QualityAttribute.maxPercentage, 25 }
                }
            },
            {QualityLevel.Average,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#d6d6d6ff" },
                    {QualityAttribute.minPercentage, 26 },
                    {QualityAttribute.maxPercentage, 50 }
                }
            },
            {QualityLevel.Good,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#00ff00ff" },
                    {QualityAttribute.minPercentage, 51 },
                    {QualityAttribute.maxPercentage, 75 }
                }
            },
            {QualityLevel.Great,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#0000ffff" },
                    {QualityAttribute.minPercentage, 76 },
                    {QualityAttribute.maxPercentage, 90 }
                }
            },
            {QualityLevel.Perfect,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#800080ff" },
                    {QualityAttribute.minPercentage, 91 },
                    {QualityAttribute.maxPercentage, 100 }
                }
            },
            {QualityLevel.Unbreakable,
                new Dictionary<QualityAttribute, object>() {
                    {QualityAttribute.color, "#b2006bff" },
                    {QualityAttribute.minPercentage, 1000 },
                    {QualityAttribute.maxPercentage, 10000 }
                }
            }
        };
    public static string GetColor(QualityLevel quality)
    {
        return qualityDictionary.First(T => T.Key == quality).Value.First(T => T.Key == QualityAttribute.color).Value.ToString();
    }
    public static int GetMinPercentage(QualityLevel quality)
    {
        return (int)qualityDictionary.First(T => T.Key == quality).Value.First(T => T.Key == QualityAttribute.minPercentage).Value;
    }
    public static int GetMaxPercentage(QualityLevel quality)
    {
        return (int)qualityDictionary.First(T => T.Key == quality).Value.First(T => T.Key == QualityAttribute.maxPercentage).Value;
    }
    public static QualityLevel GetQuality(int durability)
    {
        foreach(var item in qualityDictionary)
        {
            try
            {
                int Min = GetMinPercentage(item.Key);
                int Max = GetMaxPercentage(item.Key);
                if (Min <= durability && durability <= Max)
                    return item.Key;
            }
            catch (Exception) { }
        }
        return QualityLevel.Unbreakable;
    }
    public static string WriteQualityLevelDictionary()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Quality Level Dictionary");
        sb.AppendLine("-------------------------");
        foreach (var item in qualityDictionary)
        {
            QualityLevel qual = item.Key;
            sb.AppendLine(string.Format("<color={0}>{1}</Color> Min%:{2} - Max%:{3}", GetColor(qual), qual, GetMinPercentage(qual), GetMaxPercentage(qual)));
        }
        sb.AppendLine("-------------------------");
        return sb.ToString();
    }
}
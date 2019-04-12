using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RarityLevelBase
{
    private static Dictionary<RarityLevel, Dictionary<RarityAttribute, object>> rarityDictionary =
        new Dictionary<RarityLevel, Dictionary<RarityAttribute, object>>()
        {
            {RarityLevel.Common,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#d6d6d6ff" }
                }
            },
            {RarityLevel.Uncommon,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#00ff00ff" }
                }
            },
            {RarityLevel.Rare,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#0000ffff" }
                }
            },
            {RarityLevel.Epic,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#800080ff" }
                }
            },
            {RarityLevel.Legendary,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#ff8300ff" }
                }
            },
            {RarityLevel.Unique,
                new Dictionary<RarityAttribute, object>() {
                    {RarityAttribute.color, "#ff0000ff" }
                }
            }
        };
    public static string GetColor(RarityLevel rarity)
    {
        return rarityDictionary.First(T => T.Key == rarity).Value.First(T => T.Key == RarityAttribute.color).Value.ToString();
    }
    public static string WriteRarityLevelDictionary()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Rarity Level Dictionary");
        sb.AppendLine("-------------------------");
        foreach (var item in rarityDictionary)
        {
            RarityLevel rare = item.Key;
            sb.AppendLine(string.Format("<color={0}>{1}</Color>", GetColor(rare), rare));
        }
        sb.AppendLine("-------------------------");
        return sb.ToString();
    }
}


using System.Text;
using UnityEngine;

enum GameInfoObjects
{
    Quality,
    Rarity,
    StatLevel
}

[CreateAssetMenu(fileName = "GameInfoObject", menuName = "GameStats/GameInfoObject", order = 1)]
public class GameInfoObject : Item, IUseable
{
    [SerializeField]
    private GameInfoObjects gameInfoObjects;

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(base.GetDescription());
        switch (gameInfoObjects)
        {
            case GameInfoObjects.Quality:
                sb.AppendLine(QualityLevelBase.WriteQualityLevelDictionary());
                break;
            case GameInfoObjects.Rarity:
                sb.AppendLine(RarityLevelBase.WriteRarityLevelDictionary());
                break;
            case GameInfoObjects.StatLevel:
                sb.AppendLine(StatLevelBase.WriteStateLevelDictionary());
                break;
            default:
                break;
        }
        return sb.ToString();
    }

    public void Use() { }
}

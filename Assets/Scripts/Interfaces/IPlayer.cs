

public interface IPlayer
{
    int RunAway { get; set; } 
    int Gold { get; set; }
    int Level { get; set; }
    int Power { get; set; }
    int SellCard(TreasureCardMono treasureCardMono);
    int GainGold(int gold);
    int GainExperience(int experience);
    int LevelUp(int levels);
}


public interface IPlayer
{
    int RunAway { get; set; }
    CharacterClass PlayerClass
    {
        get;
        set;
    }
    int Gold { get; set; }
    int Level { get; set; }

    int Power
    {
        get;
        set;
    }

    int PlayCard();

    int MoveCard();

    int SellCard(TreasureCardMono a_Card);

    int GainGold(int a_Gold);

    int GainExperience(int a_Experience);

    int LevelUp(int a_Levels);
}
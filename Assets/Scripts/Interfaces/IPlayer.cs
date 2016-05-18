

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

    int SellCard(TreasureCardMono a_card);

    int GainGold(int a_gold);

    int GainExperience(int a_experience);

    int LevelUp(int a_levels);
}
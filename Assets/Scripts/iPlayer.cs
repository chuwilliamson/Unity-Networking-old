using Card;
namespace Eric
{
    public interface iPlayer
    {
        int PlayCard();
        int DrawCard();
        int MoveCard();
        int SellCard(TreasureCard a_card);
        int GainGold(int a_gold);
        int LevelUp(int a_levels);
        int GainExperience(int a_experience);
    }
}

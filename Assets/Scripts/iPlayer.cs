using Card;
namespace Eric
{
    public interface iPlayer
    {
        int PlayCard();
        int DrawCard();
        int MoveCard();
        int SellCard(TreasureCardMono a_card);
        int GainGold(int a_gold);
        int GainExperience(int a_experience);
        int LevelUp(int a_levels);
    }
}

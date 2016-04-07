using Card;
namespace Eric
{
    public interface iPlayer
    {
        int PlayCard();
        int DrawTreasureCard();
        int DrawMysteryCard();
        int MoveCard();
        int SellCard(TreasureCardMono a_card);
        int GainGold(int a_gold);
        int GainExperience(int a_experience);
        int LevelUp(int a_levels);
    }
}


namespace Eric
{
    public interface iPlayer
    {
        int PlayCard();
        bool DrawCard<T>();        
        int MoveCard();
        int SellCard(TreasureCardMono a_card);
        int GainGold(int a_gold);
        int GainExperience(int a_experience);
        int LevelUp(int a_levels);
    }
}

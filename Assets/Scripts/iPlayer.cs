namespace Eric
{
    public interface iPlayer
    {
        int PlayCard();
        int DrawCard();
        int GainGold(int a_gold);
        int LevelUp(int a_levels);
        int GainExperience(int a_experience);
    }
}

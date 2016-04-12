public interface ICard// : ICardLogger
{
	string Name { get; set; }

	string Description { get; set; }

 

}


public interface ICardLogger
{
	string Info{ get; set; }
}



public interface IPlayer
{
	int PlayCard ();

	bool DrawCard<T> () where T : class, new();
	//bool DrawCard<T> () where T : class, new();

	int MoveCard ();

	int SellCard (TreasureCardMono a_card);

	int GainGold (int a_gold);

	int GainExperience (int a_experience);

	int LevelUp (int a_levels);
}


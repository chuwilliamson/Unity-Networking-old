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
	int RunAway{ get; set; }
	Character.CharacterClass PlayerClass {
		get;
		set;
	}
	int Gold{ get; set; }
	int Level { get; set;
	}
	int Power 
	{
		get;
		set;
	}
	int PlayCard ();

	bool DrawCard<T> () where T : class, new();

	int MoveCard ();

	int SellCard (TreasureCardMono a_card);

	int GainGold (int a_gold);

	int GainExperience (int a_experience);

	int LevelUp (int a_levels);
}

public interface IMystery : ICard
{
	int Power{ get; set; }
    int Reward { get; set; }
	MysteryType CardType{ get; set; }
}

public interface ITreasure : ICard
{
	int Gold{ get; set; }
	int Power { get; set; }
	TreasureType CardType{get; set;}
}


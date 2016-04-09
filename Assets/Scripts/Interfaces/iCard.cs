public interface iCard : iInfo
{
	string Name { get; set; }
	string Description { get; set; }
	bool State { get; set; } 
	CardType Type{get;set;}
	System.Type MonoType{ get; set; }
}


public interface iInfo
{
	string Info{ get; set;}
}
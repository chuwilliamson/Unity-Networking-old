

public interface IMystery : ICard
{
    int Power { get; set; }
    int Reward { get; set; }
    MysteryType CardType { get; set; }
}


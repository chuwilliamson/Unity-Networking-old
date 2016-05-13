public interface ITreasure : ICard
{
    int Gold { get; set; }
    int Power { get; set; }
    TreasureType CardType { get; set; }
}


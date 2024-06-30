using PokerCalculator.Enums;


namespace PokerCalculator.Interfaces;

public interface ICard
{
    public Rank Rank { get; }

    public Suit Suit { get; }
}
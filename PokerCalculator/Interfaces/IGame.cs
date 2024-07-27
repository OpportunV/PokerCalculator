using System.Collections.Generic;


namespace PokerCalculator.Interfaces;

public interface IGame
{
    public IBoard Board { get; }

    public IList<IPlayer> Players { get; }

    public IDeck Deck { get; }
}
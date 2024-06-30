using System.Collections.Generic;
using PokerCalculator.Enums;


namespace PokerCalculator.Interfaces;

public interface IGameFactory
{
    public IGame CreateGame(PokerGameType gameType, IBoard board, IList<IHand> playerHands);
}
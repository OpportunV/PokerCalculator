using System;
using System.Collections.Generic;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class GameFactory : IGameFactory
{
    public IGame CreateGame(PokerGameType gameType, IBoard board, IList<IHand> playerHands)
    {
        return gameType switch
        {
            PokerGameType.TexasHoldem => new HoldemGame(board, playerHands),
            _ => throw new NotSupportedException($"Unsupported game type: {gameType}")
        };
    }
}
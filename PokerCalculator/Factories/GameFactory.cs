using System;
using System.Collections.Generic;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Factories;

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
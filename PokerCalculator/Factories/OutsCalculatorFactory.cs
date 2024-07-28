using System;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Factories;

public class OutsCalculatorFactory : IOutsCalculatorFactory
{
    public IOutsCalculator GetOutsCalculator(PokerGameType gameType, IGame game)
    {
        return gameType switch
        {
            PokerGameType.TexasHoldem => new HoldemOutsCalculator(game),
            _ => throw new NotSupportedException($"Unsupported game type: {gameType}")
        };
    }
}
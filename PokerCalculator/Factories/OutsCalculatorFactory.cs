using System;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Factories;

public class OutsCalculatorFactory : IOutsCalculatorFactory
{
    public IOutsCalculator GetOutsCalculator(PokerGameType gameType)
    {
        return gameType switch
        {
            PokerGameType.TexasHoldem => new HoldemOutsCalculator(),
            _ => throw new NotSupportedException($"Unsupported game type: {gameType}")
        };
    }
}
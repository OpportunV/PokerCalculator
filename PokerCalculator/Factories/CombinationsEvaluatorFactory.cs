using System;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Factories;

public class CombinationsEvaluatorFactory : ICombinationsEvaluatorFactory
{
    public ICombinationsEvaluator GetCombinationsEvaluator(PokerGameType gameType)
    {
        return gameType switch
        {
            PokerGameType.TexasHoldem => new HoldemCombinationsEvaluator(),
            _ => throw new NotSupportedException($"Unsupported game type: {gameType}")
        };
    }
}
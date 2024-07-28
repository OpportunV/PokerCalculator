using System;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;
using PokerCalculator.Services;


namespace PokerCalculator.Factories;

public class OutsCalculatorFactory : IOutsCalculatorFactory
{
    public IOutsCalculator GetOutsCalculator(IGame game)
    {
        return game switch
        {
            HoldemGame => new HoldemOutsCalculator(game),
            _ => throw new NotSupportedException($"Unsupported game type: {game.GetType()}")
        };
    }
}
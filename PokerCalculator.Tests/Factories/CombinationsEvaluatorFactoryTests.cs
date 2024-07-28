using System;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Tests.Factories;

public class CombinationsEvaluatorFactoryTests
{
    [Fact]
    public void GameFactory_CreateGame_ReturnsHoldemGameForTexasHoldem()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();

        var evaluator = factory.GetCombinationsEvaluator(PokerGameType.TexasHoldem);

        Assert.IsType<HoldemCombinationsEvaluator>(evaluator);
    }

    [Fact]
    public void GameFactory_CreateGame_ThrowsExceptionOnUnsupportedType()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();

        Assert.Throws<NotSupportedException>(() => factory.GetCombinationsEvaluator((PokerGameType) 90));
    }
}
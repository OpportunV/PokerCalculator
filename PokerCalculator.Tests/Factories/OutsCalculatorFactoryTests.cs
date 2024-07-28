using System;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Tests.Factories;

public class OutsCalculatorFactoryTests
{
    [Fact]
    public void GameFactory_CreateGame_ReturnsHoldemGameForTexasHoldem()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        var evaluator = factory.GetOutsCalculator(PokerGameType.TexasHoldem);

        Assert.IsType<HoldemOutsCalculator>(evaluator);
    }

    [Fact]
    public void GameFactory_CreateGame_ThrowsExceptionOnUnsupportedType()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        Assert.Throws<NotSupportedException>(() => factory.GetOutsCalculator((PokerGameType) 90));
    }
}
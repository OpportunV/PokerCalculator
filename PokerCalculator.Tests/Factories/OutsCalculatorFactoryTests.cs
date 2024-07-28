using System;
using System.Collections.Generic;
using Moq;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;
using PokerCalculator.Services;


namespace PokerCalculator.Tests.Factories;

public class OutsCalculatorFactoryTests
{
    [Fact]
    public void OutsCalculatorFactory_GetOutsCalculator_ReturnsHoldemOutsCalculatorForTexasHoldem()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        var holdemGame = new HoldemGame(new Board(), new List<IHand>{ Hand.Parse("AsJs") });
        var evaluator = factory.GetOutsCalculator(holdemGame);

        Assert.IsType<HoldemOutsCalculator>(evaluator);
    }

    [Fact]
    public void OutsCalculatorFactory_GetOutsCalculator_ThrowsExceptionOnUnsupportedType()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        var gameMock = new Mock<IGame>();

        Assert.Throws<NotSupportedException>(() => factory.GetOutsCalculator(gameMock.Object));
    }
}
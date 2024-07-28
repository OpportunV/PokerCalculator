using System;
using System.Collections.Generic;
using Moq;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Services;


namespace PokerCalculator.Tests.Factories;

public class OutsCalculatorFactoryTests
{
    [Fact]
    public void OutsCalculatorFactory_GetOutsCalculator_ReturnsHoldemOutsCalculatorForTexasHoldem()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        var gameMock = new Mock<IGame>();

        var evaluator = factory.GetOutsCalculator(PokerGameType.TexasHoldem, gameMock.Object);

        Assert.IsType<HoldemOutsCalculator>(evaluator);
    }

    [Fact]
    public void OutsCalculatorFactory_GetOutsCalculator_ThrowsExceptionOnUnsupportedType()
    {
        IOutsCalculatorFactory factory = new OutsCalculatorFactory();

        var gameMock = new Mock<IGame>();
        var playerMock = new Mock<IPlayer>();
        gameMock.Setup(game => game.Players)
            .Returns(() => new List<IPlayer> { playerMock.Object });

        Assert.Throws<NotSupportedException>(() => factory.GetOutsCalculator((PokerGameType) 90, gameMock.Object));
    }
}
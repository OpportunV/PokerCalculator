using System;
using System.Collections.Generic;
using Moq;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;
using PokerCalculator.Services;

namespace PokerCalculator.Tests.Factories;

public class CombinationsEvaluatorFactoryTests
{
    [Fact]
    public void CombinationsEvaluatorFactory_GetCombinationsEvaluator_ReturnsHoldemForTexasHoldemEnum()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();

        var evaluator = factory.GetCombinationsEvaluator(PokerGameType.TexasHoldem);

        Assert.IsType<HoldemCombinationsEvaluator>(evaluator);
    }

    [Fact]
    public void CombinationsEvaluatorFactory_GetCombinationsEvaluator_ReturnsHoldemForHoldemGame()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();
        
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("AsKd")),
        };

        var board = Board.Parse("8s9cAc7s");
        var game = new HoldemGame(board, players);

        var evaluator = factory.GetCombinationsEvaluator(game);

        Assert.IsType<HoldemCombinationsEvaluator>(evaluator);
    }

    [Fact]
    public void CombinationsEvaluatorFactory_GetCombinationsEvaluator_ThrowsExceptionOnUnsupportedType()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();
    
        Assert.Throws<NotSupportedException>(() => factory.GetCombinationsEvaluator((PokerGameType)90));
    }
    
    [Fact]
    public void CombinationsEvaluatorFactory_GetCombinationsEvaluator_ThrowsExceptionOnUnsupportedGame()
    {
        ICombinationsEvaluatorFactory factory = new CombinationsEvaluatorFactory();
    
        var game = new Mock<IGame>();
    
        Assert.Throws<NotSupportedException>(() => factory.GetCombinationsEvaluator(game.Object));
    }
}
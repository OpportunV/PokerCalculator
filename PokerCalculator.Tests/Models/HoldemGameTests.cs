using System.Collections.Generic;
using PokerCalculator.Exceptions;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class HoldemGameTests
{
    [Fact]
    public void HoldemGame_InitializationValidation_ThrowsOnDuplicate()
    {
        var board = Board.Parse("AdKdQd");
        var playerHands = new List<IHand>
        {
            Hand.Parse("JdKd")
        };

        Assert.Throws<PokerGameException>(() => new HoldemGame(board, playerHands));
    }

    [Fact]
    public void HoldemGame_InitializationValidation_ThrowsOnWrongHandsSize()
    {
        var board = Board.Parse("AdKdQd");
        var playerHands = new List<IHand>
        {
            Hand.Parse("Jd")
        };

        Assert.Throws<PokerHandException>(() => new HoldemGame(board, playerHands));
    }

    [Fact]
    public void HoldemGame_InitializationValidation_NoExceptionOnValidInput()
    {
        var board = Board.Parse("AdKdQd");
        var playerHands = new List<IHand>
        {
            Hand.Parse("JdQc")
        };

        var game = new HoldemGame(board, playerHands);
    }

    [Fact]
    public void HoldemGame_FilterDeck_RemovesPlayerHandCardsFromDeck()
    {
        var board = new Board();
        var playerHands = new List<IHand>
        {
            Hand.Parse("JdQc")
        };
        var game = new HoldemGame(board, playerHands);

        Assert.Equal(50, game.Deck.Count);
    }

    [Fact]
    public void HoldemGame_FilterDeck_RemovesBoardCardsFromDeck()
    {
        var board = Board.Parse("AdKdQd");
        var playerHands = new List<IHand>
        {
            Hand.Parse("JdQc")
        };
        var game = new HoldemGame(board, playerHands);

        Assert.Equal(47, game.Deck.Count);
    }
}
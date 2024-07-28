using System;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Factories;

public class GameFactoryTests
{
    [Fact]
    public void GameFactory_CreateGame_ReturnsHoldemGameForTexasHoldem()
    {
        IGameFactory factory = new GameFactory();
        IBoard board = new Board();
        var hands = new List<IHand>
        {
            new Hand(new Card(Rank.Ace, Suit.Hearts), new Card(Rank.Ace, Suit.Diamonds))
        };

        var game = factory.CreateGame(PokerGameType.TexasHoldem, board, hands);

        Assert.IsType<HoldemGame>(game);
        Assert.Equal(board, game.Board);
        Assert.Equal(hands, game.Players.Select(player => player.Hand));
    }

    [Fact]
    public void GameFactory_CreateGame_ThrowsExceptionOnUnsupportedType()
    {
        IGameFactory factory = new GameFactory();
        IBoard board = new Board();
        var hands = new List<IHand>
        {
            new Hand(new Card(Rank.Ace, Suit.Hearts), new Card(Rank.Ace, Suit.Diamonds))
        };

        Assert.Throws<NotSupportedException>(() => factory.CreateGame((PokerGameType) 90, board, hands));
    }
}
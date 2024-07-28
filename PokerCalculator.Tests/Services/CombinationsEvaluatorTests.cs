using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;
using PokerCalculator.Services;


namespace PokerCalculator.Tests.Services;

public class CombinationsEvaluatorTests
{
    private readonly ICombinationsEvaluator _combinationsEvaluator = new HoldemCombinationsEvaluator();

    [Fact]
    public void FillsCombinationProperty_ForThreePlayers()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Spades),
                              new Card(Rank.Six, Suit.Diamonds));

        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Nine, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades))),
            new Player(new Hand(new Card(Rank.Two, Suit.Clubs), new Card(Rank.King, Suit.Hearts)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.NotNull(players[2].Combination);
    }

    [Fact]
    public void FillsCombinationProperty_ForFullBoard()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Spades),
                              new Card(Rank.Ace, Suit.Spades), new Card(Rank.King, Suit.Hearts),
                              new Card(Rank.Ten, Suit.Clubs));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Nine, Suit.Hearts))),
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
    }

    [Fact]
    public void FillsCombinationProperty_ForEmptyBoard()
    {
        var board = new Board();
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Nine, Suit.Hearts))),
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
    }

    [Fact]
    public void FillsCombinationProperty_ForHighCard()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Spades),
                              new Card(Rank.Six, Suit.Diamonds));

        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Nine, Suit.Hearts))),
        };

        var expectedCards = new ICard[]
        {
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.Nine, Suit.Hearts),
            new Card(Rank.Six, Suit.Diamonds),
            new Card(Rank.Four, Suit.Spades),
            new Card(Rank.Two, Suit.Hearts)
        };

        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.Equal(CombinationType.HighCard, players[0].Combination!.Type);
        Assert.Equal(expectedCards, players[0].Combination);
    }

    [Fact]
    public void FillsCombinationProperty_ForPair()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Spades),
                              new Card(Rank.Six, Suit.Diamonds));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Two, Suit.Clubs), new Card(Rank.Queen, Suit.Hearts))),
        };
        
        var expectedCards = new ICard[]
        {
            new Card(Rank.Two, Suit.Hearts),
            new Card(Rank.Two, Suit.Clubs),
            new Card(Rank.Queen, Suit.Hearts),
            new Card(Rank.Six, Suit.Diamonds),
            new Card(Rank.Four, Suit.Spades)
        };
        
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.Equal(CombinationType.Pair, players[0].Combination!.Type);
        Assert.Equal(expectedCards, players[0].Combination);
    }

    [Fact]
    public void FillsCombinationProperty_ForTwoPair()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Spades),
                              new Card(Rank.Four, Suit.Diamonds));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Two, Suit.Clubs), new Card(Rank.Queen, Suit.Hearts))),
        };
        
        var expectedCards = new ICard[]
        {
            new Card(Rank.Four, Suit.Diamonds),
            new Card(Rank.Four, Suit.Spades),
            new Card(Rank.Two, Suit.Hearts),
            new Card(Rank.Two, Suit.Clubs),
            new Card(Rank.Queen, Suit.Hearts)
        };
        
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.Equal(CombinationType.TwoPairs, players[0].Combination!.Type);
        Assert.Equal(expectedCards, players[0].Combination!);
    }

    [Fact]
    public void FillsCombinationProperty_ForThreeOfAKind()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Two, Suit.Spades),
                              new Card(Rank.Two, Suit.Diamonds));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Nine, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.ThreeOfAKind, players[0].Combination!.Type);
        Assert.Equal(CombinationType.ThreeOfAKind, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForStraight()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Three, Suit.Spades),
                              new Card(Rank.Four, Suit.Diamonds));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Five, Suit.Clubs), new Card(Rank.Six, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Ace, Suit.Clubs), new Card(Rank.Five, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.Straight, players[0].Combination!.Type);
        Assert.Equal(CombinationType.Straight, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForFlush()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Four, Suit.Hearts),
                              new Card(Rank.Six, Suit.Hearts));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Queen, Suit.Hearts), new Card(Rank.Nine, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.Flush, players[0].Combination!.Type);
        Assert.Equal(CombinationType.HighCard, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForFullHouse()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Two, Suit.Spades),
                              new Card(Rank.Three, Suit.Hearts));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Two, Suit.Diamonds), new Card(Rank.Three, Suit.Diamonds))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.FullHouse, players[0].Combination!.Type);
        Assert.Equal(CombinationType.TwoPairs, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForFourOfAKind()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Two, Suit.Spades),
                              new Card(Rank.Two, Suit.Diamonds));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Two, Suit.Clubs), new Card(Rank.Queen, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.FourOfAKind, players[0].Combination!.Type);
        Assert.Equal(CombinationType.ThreeOfAKind, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForStraightFlush()
    {
        var board = new Board(new Card(Rank.Two, Suit.Hearts), new Card(Rank.Three, Suit.Hearts),
                              new Card(Rank.Four, Suit.Hearts));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.Five, Suit.Hearts), new Card(Rank.Six, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.StraightFlush, players[0].Combination!.Type);
        Assert.Equal(CombinationType.Pair, players[1].Combination!.Type);
    }

    [Fact]
    public void FillsCombinationProperty_ForRoyalFlush()
    {
        var board = new Board(new Card(Rank.Ten, Suit.Hearts), new Card(Rank.Jack, Suit.Hearts),
                              new Card(Rank.Queen, Suit.Hearts));
        var players = new List<IPlayer>
        {
            new Player(new Hand(new Card(Rank.King, Suit.Hearts), new Card(Rank.Ace, Suit.Hearts))),
            new Player(new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Three, Suit.Spades)))
        };
        var game = new HoldemGame(board, players);

        _combinationsEvaluator.Evaluate(game);

        Assert.NotNull(players[0].Combination);
        Assert.NotNull(players[1].Combination);
        Assert.Equal(CombinationType.RoyalFlush, players[0].Combination!.Type);
        Assert.Equal(CombinationType.HighCard, players[1].Combination!.Type);
    }
    
    [Fact]
    public void RealLifeExample()
    {
        var board = Board.Parse("9hAh6hQc");
        
        // Ordered by combination strength for this board.
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("TdAc")),
            new Player(Hand.Parse("QhKs")),
            new Player(Hand.Parse("2cQd")),
            new Player(Hand.Parse("JsJc")),
            new Player(Hand.Parse("4h4c")),
            new Player(Hand.Parse("5cJd")),
            new Player(Hand.Parse("5s8h")),
            new Player(Hand.Parse("7c2s")),
        };

        var game = new HoldemGame(board, players);
        
        _combinationsEvaluator.Evaluate(game);

        var results = game.Players.OrderByDescending(player => player.Combination)
            .Select(player => player.Hand);
        Assert.Equal(players.Select(player => player.Hand), results);
    }
}
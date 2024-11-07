using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Services;

public class OutsCalculatorTests
{
    private readonly OutsCalculatorFactory _outsCalculatorFactory = new();

    [Fact]
    public void RealLifeExample1()
    {
        var board = Board.Parse("9hAh6hQc");

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
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(game);
        outsCalculator.Calculate();

        Assert.Equal(15, players.SelectMany(player => player.Outs)
                         .Distinct()
                         .Count());
    }

    [Fact]
    public void RealLifeExample2()
    {
        var board = Board.Parse("Ah9sQd5s");

        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("JcQh")),
            new Player(Hand.Parse("KsTh")),
            new Player(Hand.Parse("Kd6d")),
            new Player(Hand.Parse("4h4d")),
            new Player(Hand.Parse("6s7s")),
            new Player(Hand.Parse("4s9c")),
        };

        var game = new HoldemGame(board, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(game);
        outsCalculator.Calculate();

        Assert.Equal(17, players.Sum(player => player.Outs.Count));
    }

    [Fact]
    public void RealLifeExample3()
    {
        var board = Board.Parse("7hAh2c5d");

        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("AdKd")),
            new Player(Hand.Parse("TcJs")),
            new Player(Hand.Parse("Ac5c")),
            new Player(Hand.Parse("9dQs")),
            new Player(Hand.Parse("3sTs")),
            new Player(Hand.Parse("Kh5s")),
            new Player(Hand.Parse("7dQd")),
            new Player(Hand.Parse("AsTh")),
            new Player(Hand.Parse("2h9s")),
        };

        var game = new HoldemGame(board, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(game);
        outsCalculator.Calculate();

        Assert.Equal(11, players.Sum(player => player.Outs.Count));
    }

    [Fact]
    public void RealLifeExample4()
    {
        var newHand1 = new Hand(new Card(Rank.Three, Suit.Clubs), new Card(Rank.Three, Suit.Diamonds));
        var newHand2 = new Hand(new Card(Rank.Four, Suit.Clubs), new Card(Rank.Jack, Suit.Clubs));
        var newHand3 = new Hand(new Card(Rank.Seven, Suit.Clubs), new Card(Rank.Six, Suit.Diamonds));

        var newHand4 = new Hand(new Card(Rank.Queen, Suit.Hearts), new Card(Rank.Seven, Suit.Diamonds));
        var newHand5 = new Hand(new Card(Rank.Four, Suit.Hearts), new Card(Rank.Two, Suit.Diamonds));
        var newHand6 = new Hand(new Card(Rank.Eight, Suit.Hearts), new Card(Rank.Ace, Suit.Hearts));
        var newHand7 = new Hand(new Card(Rank.Seven, Suit.Hearts), new Card(Rank.Five, Suit.Diamonds));

        var players = new List<IPlayer>
        {
            new Player(newHand1),
            new Player(newHand2),
            new Player(newHand3),
            new Player(newHand4),
            new Player(newHand5),
            new Player(newHand6),
            new Player(newHand7)
        };

        var boardTurn = new Board(new Card(Rank.Two, Suit.Clubs), new Card(Rank.Ten, Suit.Spades),
                                  new Card(Rank.Jack, Suit.Spades), new Card(Rank.King, Suit.Clubs));

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();

        Assert.Equal(11, players.Sum(player => player.Outs.Count));
    }

    [Fact]
    public void RealLifeExample5()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("JcQd")),
            new Player(Hand.Parse("2s3c")),
            new Player(Hand.Parse("7cQc")),
            new Player(Hand.Parse("3h5c")),
            new Player(Hand.Parse("6c4d")),
            new Player(Hand.Parse("KcTs")),
            new Player(Hand.Parse("AsKd")),
        };

        var boardTurn = Board.Parse("8s9cAc7s");

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(19, totalOuts);
    }

    [Fact]
    public void RealLifeExample6()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("KcJh")),
            new Player(Hand.Parse("4c7c")),
            new Player(Hand.Parse("Ad9d")),
            new Player(Hand.Parse("Kh2d")),
            new Player(Hand.Parse("Td6d")),
            new Player(Hand.Parse("TcJd")),
            new Player(Hand.Parse("3dKd")),
            new Player(Hand.Parse("Js5d")),
            new Player(Hand.Parse("Qd4h")),
        };

        var boardTurn = Board.Parse("Th6h8cKs");

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(19, totalOuts);
    }

    [Fact]
    public void RealLifeExample7()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("Jc3c")),
            new Player(Hand.Parse("9s5d")),
            new Player(Hand.Parse("Ac7h")),
            new Player(Hand.Parse("Ks9d")),
            new Player(Hand.Parse("7sKh")),
            new Player(Hand.Parse("2hAs")),
        };

        var boardTurn = Board.Parse("9c7d2sTh");

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(15, totalOuts);
    }

    [Fact]
    public void RealLifeExample8()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("QdQh")),
            new Player(Hand.Parse("AhTs")),
            new Player(Hand.Parse("2h7d")),
            new Player(Hand.Parse("8dJc")),
            new Player(Hand.Parse("Kh3d")),
            new Player(Hand.Parse("4cKd")),
            new Player(Hand.Parse("5c3c")),
        };

        var boardTurn = Board.Parse("8h3h9s6d");

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(20, totalOuts);
    }

    [Fact]
    public void RealLifeExample9()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("4sAs")),
            new Player(Hand.Parse("5s9s")),
            new Player(Hand.Parse("4hKc")),
            new Player(Hand.Parse("TcAc")),
            new Player(Hand.Parse("5h7s")),
            new Player(Hand.Parse("7c3s")),
            new Player(Hand.Parse("TsJd")),
            new Player(Hand.Parse("Ad8h")),
        };

        var boardTurn = Board.Parse("7dKhJc3c");

        var gameOuts = new HoldemGame(boardTurn, players);
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(18, totalOuts);
    }
    
    [Fact]
    public void RealLifeExample10()
    {
        var players = new List<IPlayer>
        {
            new Player(Hand.Parse("3dAs")),
            new Player(Hand.Parse("JcAc")),
            new Player(Hand.Parse("3sQd")),
            new Player(Hand.Parse("6h8h")),
            new Player(Hand.Parse("9c7d")),
            new Player(Hand.Parse("Th8d")),
            new Player(Hand.Parse("4c7c")),
        };

        var boardTurn = Board.Parse("3cQs6c2s");

        var gameOuts = new HoldemGame(boardTurn, players);
        
        var outsCalculator = _outsCalculatorFactory.GetOutsCalculator(gameOuts);
        outsCalculator.Calculate();
        var totalOuts = players.Sum(player => player.Outs.Count);

        Assert.Equal(12, totalOuts);
    }
}
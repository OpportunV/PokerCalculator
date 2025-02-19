﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class BoardTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithGivenCards()
    {
        ICard card1 = new Card(Rank.Ace, Suit.Spades);
        ICard card2 = new Card(Rank.King, Suit.Hearts);
        IBoard board = new Board(card1, card2);

        Assert.Equal(2, board.Count);
        Assert.Contains(card1, board);
        Assert.Contains(card2, board);
    }

    [Fact]
    public void Parse_ShouldCreateBoardFromString()
    {
        IBoard board = Board.Parse("AsKh");

        Assert.Equal(2, board.Count);
        Assert.Contains(new Card(Rank.Ace, Suit.Spades), board);
        Assert.Contains(new Card(Rank.King, Suit.Hearts), board);
    }

    [Fact]
    public void Parse_ShouldThrowArgumentNullException_WhenInputIsEmpty()
    {
        Assert.Throws<ArgumentNullException>(() => Board.Parse(string.Empty));
    }

    [Fact]
    public void Parse_ShouldThrowArgumentException_WhenInputLengthIsOdd()
    {
        Assert.Throws<ArgumentException>(() => Board.Parse("AsK"));
    }

    [Fact]
    public void AddCard_ShouldAddCardToBoard()
    {
        IBoard board = new Board();
        ICard card = new Card(Rank.Queen, Suit.Clubs);
        board.AddCard(card);

        Assert.Single(board);
        Assert.Contains(card, board);
    }

    [Fact]
    public void GetEnumerator_ShouldEnumerateOverCards()
    {
        ICard card1 = new Card(Rank.Ten, Suit.Spades);
        ICard card2 = new Card(Rank.Nine, Suit.Hearts);
        IBoard board = new Board(card1, card2);

        var cards = board.ToList();

        Assert.Equal(2, cards.Count);
        Assert.Contains(card1, cards);
        Assert.Contains(card2, cards);
    }
    
    [Fact]
    public void IEnumerable_GetEnumerator_ReturnsAllCards()
    {
        IBoard board = new Board();
        var expectedCards = board.ToList();

        IEnumerable enumerable = board;
        var enumerator = enumerable.GetEnumerator();
        var actualCards = new List<ICard>();

        while (enumerator.MoveNext())
        {
            actualCards.Add((ICard) enumerator.Current!);
        }

        Assert.Equal(expectedCards.Count, actualCards.Count);
        Assert.Equivalent(expectedCards, actualCards);
    }
}
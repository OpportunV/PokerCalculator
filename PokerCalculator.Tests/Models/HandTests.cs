using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class HandTests
{
    [Fact]
    public void Hand_ConstructorWithParams_ShouldInitializeWithCards()
    {
        var card1 = new Card(Rank.Ace, Suit.Hearts);
        var card2 = new Card(Rank.King, Suit.Diamonds);

        var hand = new Hand(card1, card2);

        Assert.Equal(2, hand.Count());
        Assert.Contains(card1, hand);
        Assert.Contains(card2, hand);
    }

    [Fact]
    public void Parse_ShouldThrowArgumentNullException_WhenInputIsEmpty()
    {
        Assert.Throws<ArgumentNullException>(() => Hand.Parse(string.Empty));
    }

    [Fact]
    public void Parse_ShouldThrowArgumentException_WhenInputLengthIsOdd()
    {
        Assert.Throws<ArgumentException>(() => Hand.Parse("AsK"));
    }

    [Fact]
    public void IEnumerable_GetEnumerator_ReturnsAllCards()
    {
        var board = new Hand(new Card(Rank.Ace, Suit.Diamonds));
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
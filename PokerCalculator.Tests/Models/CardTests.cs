using System;
using PokerCalculator.Enums;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class CardTests
{
    [Theory]
    [InlineData("Ah", Suit.Hearts, Rank.Ace)]
    [InlineData("2d", Suit.Diamonds, Rank.Two)]
    [InlineData("Js", Suit.Spades, Rank.Jack)]
    public void Parse_ShouldParseValidCardStrings(string cardString, Suit expectedSuit, Rank expectedRank)
    {
        var card = Card.Parse(cardString);
        Assert.Equal(expectedSuit, card.Suit);
        Assert.Equal(expectedRank, card.Rank);
    }

    [Theory]
    [InlineData("1h")]
    [InlineData("T2")]
    [InlineData("B")]
    public void Parse_ShouldThrowExceptionForInvalidCardStrings(string invalidCardString)
    {
        Assert.Throws<ArgumentException>(() => Card.Parse(invalidCardString));
    }

    [Theory]
    [InlineData(Suit.Hearts, Rank.Ace, "Ah")]
    [InlineData(Suit.Diamonds, Rank.Two, "2d")]
    [InlineData(Suit.Spades, Rank.Jack, "Js")]
    public void ToString_ShouldReturnCorrectStringRepresentation(Suit suit, Rank rank, string expectedString)
    {
        var card = new Card(rank, suit);
        Assert.Equal(expectedString, card.ToString());
    }
}
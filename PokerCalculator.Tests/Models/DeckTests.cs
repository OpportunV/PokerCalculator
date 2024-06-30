using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class DeckTests
{
    [Fact]
    public void Deck_Initialization_ShouldHave52Cards()
    {
        IDeck deck = new Deck();

        var count = deck.Count;

        Assert.Equal(52, count);
    }

    [Fact]
    public void Deck_RemoveCard_ShouldRemoveFromDeck()
    {
        IDeck deck = new Deck();
        ICard cardToRemove = new Card(Rank.Ace, Suit.Hearts);

        deck.RemoveCard(cardToRemove);

        Assert.DoesNotContain(cardToRemove, deck);
    }

    [Fact]
    public void IEnumerable_GetEnumerator_ReturnsAllCards()
    {
        IDeck deck = new Deck();
        var expectedCards = deck.ToList();

        IEnumerable enumerable = deck;
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
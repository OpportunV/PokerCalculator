using System;
using System.Collections;
using System.Collections.Generic;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class Hand : IHand
{
    private readonly IList<ICard> _cards;

    public Hand(params ICard[] cards)
    {
        _cards = cards;
    }
    
    private Hand()
    {
        _cards = new List<ICard>();
    }

    public IEnumerator<ICard> GetEnumerator()
    {
        return _cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Hand Parse(string handCards)
    {
        if (string.IsNullOrWhiteSpace(handCards))
        {
            throw new ArgumentNullException(nameof(handCards));
        }

        if (handCards.Length % 2 != 0)
        {
            throw new ArgumentException("Invalid board card string length.");
        }

        var hand = new Hand();
        for (var i = 0; i < handCards.Length; i += 2)
        {
            hand._cards.Add(Card.Parse(handCards[i..(i + 2)]));
        }

        return hand;
    }

    public override string ToString()
    {
        var cards = string.Join("", _cards);
        return $"[{cards}]";
    }
}
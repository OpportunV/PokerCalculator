using System;
using System.Collections;
using System.Collections.Generic;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class Deck : IDeck
{
    public int Count => _cards.Count;

    private readonly IList<ICard> _cards;

    public Deck()
    {
        _cards = new List<ICard>();
        InitializeDefaultDeck();
    }

    public void RemoveCard(ICard card)
    {
        _cards.Remove(card);
    }

    public IEnumerator<ICard> GetEnumerator()
    {
        return _cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void InitializeDefaultDeck()
    {
        _cards.Clear();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Add(new Card(rank, suit));
            }
        }
    }
}
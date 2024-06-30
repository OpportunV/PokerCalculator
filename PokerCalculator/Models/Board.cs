using System;
using System.Collections;
using System.Collections.Generic;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class Board : IBoard
{
    public int Count => _cards.Count;

    private readonly IList<ICard> _cards;

    public Board(params ICard[] cards)
    {
        _cards = new List<ICard>(cards);
    }

    public static Board Parse(string boardCards)
    {
        if (string.IsNullOrWhiteSpace(boardCards))
        {
            throw new ArgumentNullException(nameof(boardCards));
        }

        if (boardCards.Length % 2 != 0)
        {
            throw new ArgumentException("Invalid board card string length.");
        }

        var board = new Board();
        for (var i = 0; i < boardCards.Length; i += 2)
        {
            board._cards.Add(Card.Parse(boardCards[i..(i + 2)]));
        }

        return board;
    }

    public void AddCard(ICard card)
    {
        _cards.Add(card);
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
}
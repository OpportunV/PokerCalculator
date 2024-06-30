using System;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class Card : ICard
{
    public Rank Rank { get; }

    public Suit Suit { get; }

    private static readonly Dictionary<char, Rank> _rankMap = new()
    {
        { '2', Rank.Two },
        { '3', Rank.Three },
        { '4', Rank.Four },
        { '5', Rank.Five },
        { '6', Rank.Six },
        { '7', Rank.Seven },
        { '8', Rank.Eight },
        { '9', Rank.Nine },
        { 't', Rank.Ten },
        { 'j', Rank.Jack },
        { 'q', Rank.Queen },
        { 'k', Rank.King },
        { 'a', Rank.Ace }
    };

    private static readonly Dictionary<char, Suit> _suitMap = new()
    {
        { 'h', Suit.Hearts },
        { 'd', Suit.Diamonds },
        { 'c', Suit.Clubs },
        { 's', Suit.Spades }
    };

    public Card(Rank rank, Suit suit)
    {
        Suit = suit;
        Rank = rank;
    }

    public static Card Parse(string cardString)
    {
        if (cardString.Length != 2)
        {
            throw new ArgumentException("Invalid card string length.");
        }

        var rankChar = char.ToLower(cardString[0]);
        var suitChar = char.ToLower(cardString[1]);

        if (!_rankMap.TryGetValue(rankChar, out var rank))
        {
            throw new ArgumentException("Invalid rank character.");
        }

        if (!_suitMap.TryGetValue(suitChar, out var suit))
        {
            throw new ArgumentException("Invalid suit character.");
        }

        return new Card(rank, suit);
    }

    public override string ToString()
    {
        return $"{RankChar()}{SuitChar()}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Card card && Suit == card.Suit && Rank == card.Rank;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }

    private char RankChar()
    {
        var rank = _rankMap.First(pair => pair.Value == Rank);
        return char.ToUpper(rank.Key);
    }

    private char SuitChar()
    {
        var suit = _suitMap.First(pair => pair.Value == Suit);
        return suit.Key;
    }
}
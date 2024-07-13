using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class PokerCombination : IPokerCombination
{
    public CombinationType Type { get; }

    public IList<ICard> MainCards { get; }

    public IList<ICard> Kickers { get; }

    public PokerCombination(CombinationType type, IEnumerable<ICard> combinationCards, IEnumerable<ICard> kickers)
    {
        Type = type;
        MainCards = combinationCards.OrderByDescending(card => card.Rank)
            .ToList();
        Kickers = kickers.OrderByDescending(card => card.Rank)
            .ToList();
    }

    public int CompareTo(IPokerCombination? other)
    {
        if (other == null)
        {
            return 1;
        }

        var typeComparison = CompareCombinationType(other);
        if (typeComparison != 0)
        {
            return typeComparison;
        }

        var combinationComparison = CompareCombinationCards(other);
        return combinationComparison != 0 ? combinationComparison : CompareKickers(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is IPokerCombination other)
        {
            return CompareTo(other) == 0;
        }

        return false;
    }

    public override int GetHashCode()
    {
        var hashCode = (int) Type;
        hashCode = MainCards.Aggregate(hashCode, (current, card) => (current * 397) ^ card.GetHashCode());
        return Kickers.Aggregate(hashCode, (current, card) => (current * 397) ^ card.GetHashCode());
    }

    public static bool operator >(PokerCombination left, PokerCombination right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <(PokerCombination left, PokerCombination right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >=(PokerCombination left, PokerCombination right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator <=(PokerCombination left, PokerCombination right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator ==(PokerCombination left, PokerCombination right)
    {
        return left.CompareTo(right) == 0;
    }

    public static bool operator !=(PokerCombination left, PokerCombination right)
    {
        return !(left == right);
    }

    public IEnumerator<ICard> GetEnumerator()
    {
        foreach (var card in MainCards)
        {
            yield return card;
        }

        foreach (var card in Kickers)
        {
            yield return card;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private int CompareCombinationType(IPokerCombination other)
    {
        return Type.CompareTo(other.Type);
    }

    private int CompareCombinationCards(IPokerCombination other)
    {
        return MainCards.Select((t, i) => t.Rank.CompareTo(other.MainCards[i].Rank))
            .FirstOrDefault(comparison => comparison != 0);
    }

    private int CompareKickers(IPokerCombination other)
    {
        return Kickers.Select((t, i) => t.Rank.CompareTo(other.Kickers[i].Rank))
            .FirstOrDefault(comparison => comparison != 0);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class PokerCombination : IPokerCombination
{
    public CombinationType Type { get; }

    private readonly IList<ICard> _mainCards;

    private readonly IList<ICard> _kickers;

    public PokerCombination(CombinationType type, IEnumerable<ICard> combinationCards, IEnumerable<ICard> kickers)
    {
        Type = type;
        _mainCards = combinationCards.OrderByDescending(card => card.Rank)
            .ToList();
        _kickers = kickers.OrderByDescending(card => card.Rank)
            .ToList();
    }

    public int CompareTo(IPokerCombination? other)
    {
        if (other == null)
        {
            return 1;
        }

        var typeComparison = CompareCombinationType(other);
        return typeComparison != 0 ? typeComparison : CompareCards(other);
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
        return this.Aggregate(hashCode, (current, card) => (current * 397) ^ card.GetHashCode());
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
        foreach (var card in _mainCards)
        {
            yield return card;
        }

        foreach (var card in _kickers)
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

    private int CompareCards(IPokerCombination other)
    {
        foreach (var (first, second) in this.Zip(other))
        {
            var comparison = first.Rank.CompareTo(second.Rank);
            if (comparison != 0)
            {
                return comparison;
            }
        }

        return 0;
    }
}
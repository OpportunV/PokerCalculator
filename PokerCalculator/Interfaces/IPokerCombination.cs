using System;
using System.Collections.Generic;
using PokerCalculator.Enums;


namespace PokerCalculator.Interfaces;

public interface IPokerCombination : IComparable<IPokerCombination>, IEnumerable<ICard>
{
    public CombinationType Type { get; }

    public IList<ICard> MainCards { get; }

    public IList<ICard> Kickers { get; }
}
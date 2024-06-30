using System.Collections.Generic;


namespace PokerCalculator.Interfaces;

public interface IPlayer
{
    public IHand Hand { get; }

    public IList<ICard> Outs { get; }

    public decimal Equity { get; set; }
}
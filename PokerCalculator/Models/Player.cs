using System.Collections.Generic;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class Player : IPlayer
{
    public IHand Hand { get; }

    public IList<ICard> Outs { get; }

    public decimal Equity { get; set; }

    public IPokerCombination? Combination { get; set; }

    public Player(IHand hand)
    {
        Hand = hand;
        Outs = new List<ICard>();
        Equity = -1;
    }
}
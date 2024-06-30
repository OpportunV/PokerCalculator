using System.Collections.Generic;


namespace PokerCalculator.Interfaces;

public interface IBoard : IEnumerable<ICard>
{
    public int Count { get; }

    public void AddCard(ICard card);

    public void RemoveCard(ICard card);
}
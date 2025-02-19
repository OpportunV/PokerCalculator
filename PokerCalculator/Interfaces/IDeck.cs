﻿using System.Collections.Generic;


namespace PokerCalculator.Interfaces;

public interface IDeck : IEnumerable<ICard>
{
    public int Count { get; }

    public void RemoveCard(ICard card);
}
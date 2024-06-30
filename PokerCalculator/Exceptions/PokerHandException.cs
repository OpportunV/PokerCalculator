using System;


namespace PokerCalculator.Exceptions;

public class PokerHandException : Exception
{
    public PokerHandException(string? message) : base(message)
    {
    }
}
using System;


namespace PokerCalculator.Exceptions;

public class PokerGameException : Exception
{
    public PokerGameException(string? message) : base(message)
    {
    }
}
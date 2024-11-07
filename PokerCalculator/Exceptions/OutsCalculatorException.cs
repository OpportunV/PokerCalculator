using System;


namespace PokerCalculator.Exceptions;

public class OutsCalculatorException : Exception
{
    public OutsCalculatorException(string? message) : base(message)
    {
    }
}
using PokerCalculator.Enums;


namespace PokerCalculator.Interfaces;

public interface IOutsCalculatorFactory
{
    public IOutsCalculator GetOutsCalculator(PokerGameType gameType, IGame game);
}
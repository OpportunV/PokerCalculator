using PokerCalculator.Enums;

namespace PokerCalculator.Interfaces;

public interface ICombinationsEvaluatorFactory
{
    public ICombinationsEvaluator GetCombinationsEvaluator(PokerGameType gameType);

    public ICombinationsEvaluator GetCombinationsEvaluator(IGame game);
}
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Services;

public class HoldemOutsCalculator : IOutsCalculator
{
    private readonly ICombinationsEvaluator _combinationsEvaluator;
    private readonly IGame _game;
    private const int MaxBoardCards = 5;

    public HoldemOutsCalculator(IGame game)
    {
        _game = game;
        var factory = new CombinationsEvaluatorFactory();
        _combinationsEvaluator = factory.GetCombinationsEvaluator(PokerGameType.TexasHoldem);
    }
    
    public void Calculate(IGame game)
    {
    }

    private void ValidateGame(IGame game)
    {
        if (game.Players.Any(player => player.Combination == null))
        {
            _combinationsEvaluator.Evaluate(game);
        }
    }
}
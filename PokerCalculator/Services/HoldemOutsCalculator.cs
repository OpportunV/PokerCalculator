using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Exceptions;
using PokerCalculator.Factories;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Services;

public class HoldemOutsCalculator : IOutsCalculator
{
    private readonly ICombinationsEvaluator _combinationsEvaluator;
    private readonly IGame _game;

    public HoldemOutsCalculator(IGame game)
    {
        _game = game;
        var factory = new CombinationsEvaluatorFactory();
        _combinationsEvaluator = factory.GetCombinationsEvaluator(PokerGameType.TexasHoldem);
    }
    
    public void Calculate()
    {
        if (_game.Board.Count != 4)
        {
            throw new OutsCalculatorException("Non turn scenarios are not supported yet.");
        }

        EvaluateGame();
        var favoritePlayer = GetFavoritePlayer();
        SimulateTurn(favoritePlayer);
    }

    private void SimulateTurn(IPlayer favoritePlayer)
    {
        foreach (var card in _game.Deck)
        {
            var newGame = GetNewGame(card);
            _combinationsEvaluator.Evaluate(newGame);
            var newFavorites =
                newGame.Players.Where(player => player.Combination!.CompareTo(favoritePlayer.Combination) > 0)
                    .GroupBy(player => player.Combination).FirstOrDefault();

            if (newFavorites == null)
            {
                continue;
            }

            foreach (var newFavorite in newFavorites)
            {
                _game.Players.First(player => player.Hand == newFavorite.Hand)
                    .Outs.Add(card);
            }
        }
    }

    private IGame GetNewGame(ICard card)
    {
        var newBoardCards = new List<ICard>(_game.Board) { card };
        var board = new Board(newBoardCards.ToArray());
        var newPlayers = new List<IPlayer>(_game.Players);
        var newGame = new HoldemGame(board, newPlayers);
        return newGame;
    }

    private IPlayer GetFavoritePlayer()
    {
        var favoritePlayer = _game.Players.MaxBy(player => player.Combination)!;

        if (_game.Players.Count(player => Equals(player.Combination, favoritePlayer.Combination)) > 1)
        {
            throw new OutsCalculatorException("Multiple favorites is not supported yet.");
        }

        return favoritePlayer;
    }

    private void EvaluateGame()
    {
        if (_game.Players.Any(player => player.Combination == null))
        {
            _combinationsEvaluator.Evaluate(_game);
        }
    }
}
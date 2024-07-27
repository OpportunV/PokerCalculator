using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Exceptions;
using PokerCalculator.Interfaces;


namespace PokerCalculator.Models;

public class HoldemGame : IGame
{
    public IBoard Board { get; }

    public IDeck Deck { get; }

    public IList<IPlayer> Players { get; }

    public IEnumerable<ICard> PlayerCards => Players.SelectMany(player => player.Hand);

    public HoldemGame(IBoard board, IList<IHand> playerHands) : this(board, playerHands.Select(hand => new Player(hand))
                                                                         .Cast<IPlayer>()
                                                                         .ToList())
    {
    }

    public HoldemGame(IBoard board, IList<IPlayer> players)
    {
        Board = board;
        Players = players;
        Deck = new Deck();
        Validate();
        FilterDeck();
    }

    private void Validate()
    {
        foreach (var player in Players)
        {
            if (player.Hand.Count() != 2)
            {
                throw new PokerHandException(
                    $"Given hand {player.Hand} has incorrect amount of cards for {nameof(HoldemGame)}.");
            }
        }

        var allCards = PlayerCards.ToList();
        allCards.AddRange(Board);
        if (allCards.Count == new HashSet<ICard>(allCards).Count)
        {
            return;
        }

        var cardsString = string.Join(" ", allCards.Select(card => card.ToString()));
        throw new PokerGameException($"There are duplicate cards in use {cardsString}.");
    }

    private void FilterDeck()
    {
        FilterDeckByBoard();
        FilterDeckByHands();
    }

    private void FilterDeckByBoard()
    {
        foreach (var card in Board)
        {
            Deck.RemoveCard(card);
        }
    }

    private void FilterDeckByHands()
    {
        foreach (var card in PlayerCards)
        {
            Deck.RemoveCard(card);
        }
    }
}
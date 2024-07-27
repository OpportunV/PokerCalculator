using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Services;

public class CombinationsEvaluator : ICombinationsEvaluator
{
    private static readonly HashSet<Rank> _lowerStraightRanks = new()
    {
        Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five
    };

    private static readonly HashSet<Rank> _royalRanks = new() { Rank.Ace, Rank.King, Rank.Queen, Rank.Jack, Rank.Ten };

    public void Evaluate(IGame game)
    {
        foreach (var player in game.Players)
        {
            EvaluatePlayer(player, game.Board);
        }
    }

    private void EvaluatePlayer(IPlayer player, IBoard board)
    {
        var cards = new List<ICard>();
        cards.AddRange(player.Hand);
        cards.AddRange(board);
        var combination = GetCombination(cards.AsQueryable());
        player.Combination = combination;
    }

    private IPokerCombination GetCombination(IQueryable<ICard> cards)
    {
        var kickers = new List<ICard>();

        if (IsRoyalFlush(cards, out var combinationCards))
        {
            return new PokerCombination(CombinationType.RoyalFlush, combinationCards, kickers);
        }

        if (IsStraightFlush(cards, out combinationCards))
        {
            return new PokerCombination(CombinationType.StraightFlush, combinationCards, kickers);
        }

        if (IsFourOfAKind(cards, out combinationCards, out kickers))
        {
            return new PokerCombination(CombinationType.FourOfAKind, combinationCards, kickers);
        }

        if (IsFullHouse(cards, out combinationCards))
        {
            return new PokerCombination(CombinationType.FullHouse, combinationCards, kickers);
        }

        if (IsFlush(cards, out combinationCards))
        {
            return new PokerCombination(CombinationType.Flush, combinationCards, kickers);
        }

        if (IsStraight(cards, out combinationCards))
        {
            return new PokerCombination(CombinationType.Straight, combinationCards, kickers);
        }

        if (IsThreeOfAKind(cards, out combinationCards, out kickers))
        {
            return new PokerCombination(CombinationType.ThreeOfAKind, combinationCards, kickers);
        }

        if (IsTwoPair(cards, out combinationCards, out kickers))
        {
            return new PokerCombination(CombinationType.TwoPairs, combinationCards, kickers);
        }

        if (IsPair(cards, out combinationCards, out kickers))
        {
            return new PokerCombination(CombinationType.Pair, combinationCards, kickers);
        }
        
        var kickerCards = cards.OrderByDescending(c => c.Rank).Take(5).ToList();
        return new PokerCombination(CombinationType.HighCard, combinationCards, kickerCards);
    }

    private bool IsRoyalFlush(IQueryable<ICard> cards, out List<ICard> combinationCards)
    {
        combinationCards = new List<ICard>();
        return IsStraightFlush(cards, out combinationCards) && _royalRanks.IsSubsetOf(cards.Select(card => card.Rank));
    }

    private bool IsStraightFlush(IQueryable<ICard> cards, out List<ICard> combinationCards)
    {
        combinationCards = new List<ICard>();
        var suitedGroup = GetSuitedGroup(cards);

        if (suitedGroup == null)
        {
            return false;
        }

        return IsStraight(suitedGroup.AsQueryable(), out combinationCards);
    }

    private bool IsFourOfAKind(IQueryable<ICard> cards, out List<ICard> combinationCards, out List<ICard> kickerCards)
    {
        combinationCards = new List<ICard>();
        kickerCards = new List<ICard>();

        var rankGroup = cards.GroupBy(card => card.Rank)
            .FirstOrDefault(grouping => grouping.Count() == 4);

        if (rankGroup == null)
        {
            return false;
        }

        combinationCards = rankGroup.ToList();
        kickerCards = cards.Except(combinationCards)
            .OrderByDescending(card => card.Rank)
            .Take(1)
            .ToList();

        return true;
    }

    private bool IsFullHouse(IQueryable<ICard> cards, out List<ICard> combinationCards)
    {
        combinationCards = new List<ICard>();

        var rankGroups = cards.GroupBy(card => card.Rank)
            .OrderByDescending(grouping => grouping.Key)
            .ToList();
        var threeOfAKindGroup = rankGroups.FirstOrDefault(grouping => grouping.Count() == 3);
        var pairGroup = rankGroups.FirstOrDefault(grouping => grouping.Count() == 2);

        if (threeOfAKindGroup == null || pairGroup == null)
        {
            return false;
        }

        combinationCards = threeOfAKindGroup.ToList();
        combinationCards.AddRange(pairGroup);

        return true;
    }

    private bool IsFlush(IQueryable<ICard> cards, out List<ICard> combinationCards)
    {
        combinationCards = new List<ICard>();
        var suitedGroup = GetSuitedGroup(cards);

        if (suitedGroup == null)
        {
            return false;
        }

        combinationCards = suitedGroup.OrderByDescending(card => card.Rank)
            .Take(5)
            .ToList();
        return true;
    }

    private bool IsStraight(IQueryable<ICard> cards, out List<ICard> combinationCards)
    {
        combinationCards = new List<ICard>();
        var orderedCards = cards.OrderBy(card => card.Rank)
            .ToList();
        var distinctRanks = orderedCards.Select(card => card.Rank)
            .Distinct()
            .ToList();

        for (int i = 0; i <= distinctRanks.Count - 5; i++)
        {
            if (distinctRanks[i + 4] - distinctRanks[i] == 4)
            {
                combinationCards = orderedCards.Where(card => distinctRanks.GetRange(i, 5)
                                                          .Contains(card.Rank))
                    .ToList();
                return true;
            }
        }

        if (_lowerStraightRanks.IsSubsetOf(distinctRanks))
        {
            combinationCards = orderedCards.Where(card => _lowerStraightRanks.Contains(card.Rank))
                .ToList();
            return true;
        }

        return false;
    }

    private bool IsThreeOfAKind(IQueryable<ICard> cards, out List<ICard> combinationCards, out List<ICard> kickerCards)
    {
        combinationCards = new List<ICard>();
        kickerCards = new List<ICard>();

        var rankGroup = cards.GroupBy(card => card.Rank)
            .OrderByDescending(grouping => grouping.Key)
            .FirstOrDefault(grouping => grouping.Count() == 3);

        if (rankGroup == null)
        {
            return false;
        }

        combinationCards = rankGroup.ToList();
        kickerCards = cards.Except(combinationCards)
            .OrderByDescending(card => card.Rank)
            .Take(2)
            .ToList();

        return true;
    }

    private bool IsTwoPair(IQueryable<ICard> cards, out List<ICard> combinationCards, out List<ICard> kickerCards)
    {
        combinationCards = new List<ICard>();
        kickerCards = new List<ICard>();

        var rankGroups = cards.GroupBy(card => card.Rank)
            .Where(grouping => grouping.Count() == 2)
            .ToList();

        if (rankGroups.Count < 2)
        {
            return false;
        }

        var topTwoPairs = rankGroups.OrderByDescending(grouping => grouping.Key)
            .Take(2)
            .SelectMany(grouping => grouping)
            .ToList();
        combinationCards = topTwoPairs;
        kickerCards = cards.Except(topTwoPairs)
            .OrderByDescending(card => card.Rank)
            .Take(1)
            .ToList();

        return true;
    }

    private bool IsPair(IQueryable<ICard> cards, out List<ICard> combinationCards, out List<ICard> kickerCards)
    {
        combinationCards = new List<ICard>();
        kickerCards = new List<ICard>();

        var rankGroup = cards.GroupBy(card => card.Rank)
            .OrderByDescending(grouping => grouping.Key)
            .FirstOrDefault(grouping => grouping.Count() == 2);

        if (rankGroup == null)
        {
            return false;
        }

        combinationCards = rankGroup.ToList();
        kickerCards = cards.Except(combinationCards)
            .OrderByDescending(card => card.Rank)
            .Take(3)
            .ToList();

        return true;
    }

    private static IGrouping<Suit, ICard>? GetSuitedGroup(IQueryable<ICard> cards)
    {
        return cards.GroupBy(card => card.Suit)
            .FirstOrDefault(grouping => grouping.Count() >= 5);
    }
}
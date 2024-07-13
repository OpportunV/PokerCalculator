using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Enums;
using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class PokerCombinationTests
{
    [Fact]
    public void CompareTo_DifferentCombination()
    {
        var highCard = new PokerCombination(CombinationType.HighCard, new List<ICard>(), new List<ICard>());
        var pair = new PokerCombination(CombinationType.Pair, new List<ICard>(), new List<ICard>());
        Assert.True(pair > highCard);
    }

    [Fact]
    public void GetHashCode_ForSameObject()
    {
        var combinationCards = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var kickers = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard = new PokerCombination(CombinationType.HighCard, combinationCards, kickers);
        var hashCode1 = highCard.GetHashCode();
        var hashCode2 = highCard.GetHashCode();
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void GetHashCode_ForEqualsObjects()
    {
        var combinationCards = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var kickers = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard1 = new PokerCombination(CombinationType.HighCard, combinationCards, kickers);
        var highCard2 = new PokerCombination(CombinationType.HighCard, combinationCards, kickers);
        Assert.True(highCard1.Equals(highCard2));
        Assert.Equal(highCard1.GetHashCode(), highCard2.GetHashCode());
    }
    
    [Fact]
    public void GetHashCode_ForDifferentObjects()
    {
        var combinationCards1 = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var combinationCards2 = new List<ICard> { new Card(Rank.Ace, Suit.Diamonds) };
        var kickers = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard1 = new PokerCombination(CombinationType.HighCard, combinationCards1, kickers);
        var highCard2 = new PokerCombination(CombinationType.HighCard, combinationCards2, kickers);
        Assert.NotEqual(highCard1.GetHashCode(), highCard2.GetHashCode());
    }

    [Fact]
    public void CompareTo_Null()
    {
        var combinationCards = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var kickers = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard = new PokerCombination(CombinationType.HighCard, combinationCards, kickers);

        Assert.Equal(1, highCard.CompareTo(null));
        Assert.False(highCard.Equals(null));
    }

    [Fact]
    public void CanIterateOverAllCards()
    {
        var mainCardsPair = new List<ICard> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.Ace, Suit.Diamonds) };
        var kickersPair = new List<ICard> { new Card(Rank.King, Suit.Clubs) };
        var pair = new PokerCombination(CombinationType.Pair, mainCardsPair, kickersPair);

        var expectedCards = new List<ICard>();
        expectedCards.AddRange(mainCardsPair);
        expectedCards.AddRange(kickersPair);
        Assert.Equal(expectedCards, pair.ToList());
    }

    [Fact]
    public void IEnumerable_Returns()
    {
        var mainCardsPair = new List<ICard> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.Ace, Suit.Diamonds) };
        var kickersPair = new List<ICard> { new Card(Rank.King, Suit.Clubs) };
        var pair = new PokerCombination(CombinationType.Pair, mainCardsPair, kickersPair);

        var expectedCards = new List<ICard>();
        expectedCards.AddRange(mainCardsPair);
        expectedCards.AddRange(kickersPair);
        IEnumerable enumerable = pair;
        var enumerator = enumerable.GetEnumerator();
        var cards = new List<ICard>();
        while (enumerator.MoveNext())
        {
            cards.Add((ICard) enumerator.Current!);
        }

        Assert.Equal(cards, expectedCards);
    }

    [Fact]
    public void InequalityOperator()
    {
        var combinationCards1 = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var kickers1 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard1 = new PokerCombination(CombinationType.HighCard, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard> { new Card(Rank.King, Suit.Hearts) };
        var kickers2 = new List<ICard> { new Card(Rank.Jack, Suit.Diamonds) };
        var highCard2 = new PokerCombination(CombinationType.HighCard, combinationCards2, kickers2);

        Assert.True(highCard1 != highCard2);
    }

    [Fact]
    public void CompareTo_HighCardVsHighCard_DifferentHighCards()
    {
        var combinationCards1 = new List<ICard> { new Card(Rank.Ace, Suit.Clubs) };
        var kickers1 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard1 = new PokerCombination(CombinationType.HighCard, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard> { new Card(Rank.King, Suit.Hearts) };
        var kickers2 = new List<ICard> { new Card(Rank.Jack, Suit.Diamonds) };
        var highCard2 = new PokerCombination(CombinationType.HighCard, combinationCards2, kickers2);

        Assert.True(highCard1 > highCard2);
    }

    [Fact]
    public void CompareTo_PairVsPair_SamePairDifferentKickers()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ace, Suit.Diamonds)
        };
        var kickers1 = new List<ICard>
        {
            new Card(Rank.Two, Suit.Spades), new Card(Rank.King, Suit.Clubs),
            new Card(Rank.Queen, Suit.Spades)
        };
        var pair1 = new PokerCombination(CombinationType.Pair, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Spades),
            new Card(Rank.Ace, Suit.Clubs)
        };
        var kickers2 = new List<ICard>
        {
            new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Three, Suit.Diamonds),
            new Card(Rank.Ten, Suit.Diamonds)
        };
        var pair2 = new PokerCombination(CombinationType.Pair, combinationCards2, kickers2);

        Assert.True(pair1 > pair2);
    }

    [Fact]
    public void CompareTo_TwoPairsVsTwoPairs()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ace, Suit.Diamonds),
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.King, Suit.Diamonds)
        };
        var kickers1 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var twoPairs1 = new PokerCombination(CombinationType.TwoPairs, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Spades),
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.King, Suit.Spades),
            new Card(Rank.King, Suit.Clubs)
        };
        var kickers2 = new List<ICard> { new Card(Rank.Jack, Suit.Clubs) };
        var twoPairs2 = new PokerCombination(CombinationType.TwoPairs, combinationCards2, kickers2);

        Assert.True(twoPairs1 > twoPairs2);
    }

    [Fact]
    public void CompareTo_ThreeOfAKindVsThreeOfAKind()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ace, Suit.Diamonds),
            new Card(Rank.Ace, Suit.Clubs)
        };
        var kickers1 = new List<ICard> { new Card(Rank.King, Suit.Clubs), new Card(Rank.Queen, Suit.Hearts) };
        var threeOfAKind1 = new PokerCombination(CombinationType.ThreeOfAKind, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.King, Suit.Clubs)
        };
        var kickers2 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs), new Card(Rank.Jack, Suit.Hearts) };
        var threeOfAKind2 = new PokerCombination(CombinationType.ThreeOfAKind, combinationCards2, kickers2);

        Assert.True(threeOfAKind1 > threeOfAKind2);
    }

    [Fact]
    public void CompareTo_StraightVsStraight()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ten, Suit.Clubs),
            new Card(Rank.Jack, Suit.Clubs),
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.Ace, Suit.Clubs)
        };
        var straight1 = new PokerCombination(CombinationType.Straight, combinationCards1, new List<ICard>());

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Nine, Suit.Diamonds),
            new Card(Rank.Ten, Suit.Diamonds),
            new Card(Rank.Jack, Suit.Diamonds),
            new Card(Rank.Queen, Suit.Diamonds),
            new Card(Rank.King, Suit.Diamonds)
        };
        var straight2 = new PokerCombination(CombinationType.Straight, combinationCards2, new List<ICard>());

        Assert.True(straight1 > straight2);
    }

    [Fact]
    public void CompareTo_FlushVsFlush()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.Jack, Suit.Clubs),
            new Card(Rank.Ten, Suit.Clubs)
        };
        var flush1 = new PokerCombination(CombinationType.Flush, combinationCards1, new List<ICard>());

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Queen, Suit.Diamonds),
            new Card(Rank.Jack, Suit.Diamonds),
            new Card(Rank.Ten, Suit.Diamonds),
            new Card(Rank.Nine, Suit.Diamonds)
        };
        var flush2 = new PokerCombination(CombinationType.Flush, combinationCards2, new List<ICard>());

        Assert.True(flush1 > flush2);
    }

    [Fact]
    public void CompareTo_FullHouseVsFullHouse()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.Ace, Suit.Diamonds),
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.King, Suit.Diamonds)
        };
        var fullHouse1 = new PokerCombination(CombinationType.FullHouse, combinationCards1, new List<ICard>());

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.King, Suit.Spades),
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.Queen, Suit.Diamonds)
        };
        var fullHouse2 = new PokerCombination(CombinationType.FullHouse, combinationCards2, new List<ICard>());

        Assert.True(fullHouse1 > fullHouse2);
    }

    [Fact]
    public void CompareTo_FourOfAKindVsFourOfAKind()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.Ace, Suit.Diamonds),
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ace, Suit.Spades)
        };
        var kickers1 = new List<ICard> { new Card(Rank.King, Suit.Clubs) };
        var fourOfAKind1 = new PokerCombination(CombinationType.FourOfAKind, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.King, Suit.Spades)
        };
        var kickers2 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var fourOfAKind2 = new PokerCombination(CombinationType.FourOfAKind, combinationCards2, kickers2);

        Assert.True(fourOfAKind1 > fourOfAKind2);
    }

    [Fact]
    public void CompareTo_StraightFlushVsStraightFlush()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ten, Suit.Clubs),
            new Card(Rank.Jack, Suit.Clubs),
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.Ace, Suit.Clubs)
        };
        var royalFlush = new PokerCombination(CombinationType.RoyalFlush, combinationCards1, new List<ICard>());

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Nine, Suit.Diamonds),
            new Card(Rank.Ten, Suit.Diamonds),
            new Card(Rank.Jack, Suit.Diamonds),
            new Card(Rank.Queen, Suit.Diamonds),
            new Card(Rank.King, Suit.Diamonds)
        };
        var straightFlush = new PokerCombination(CombinationType.StraightFlush, combinationCards2, new List<ICard>());

        Assert.True(royalFlush > straightFlush);
    }

    [Fact]
    public void CompareTo_HighCardVsHighCard_SameCards()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Clubs)
        };
        var kickers1 = new List<ICard> { new Card(Rank.Queen, Suit.Clubs) };
        var highCard1 = new PokerCombination(CombinationType.HighCard, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Hearts)
        };
        var kickers2 = new List<ICard> { new Card(Rank.Queen, Suit.Hearts) };
        var highCard2 = new PokerCombination(CombinationType.HighCard, combinationCards2, kickers2);

        Assert.False(highCard1 > highCard2);
        Assert.False(highCard2 > highCard1);
    }

    [Fact]
    public void CompareTo_PairVsPair_SamePairSameKickers()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ace, Suit.Diamonds)
        };
        var kickers1 = new List<ICard> { new Card(Rank.King, Suit.Clubs) };
        var pair1 = new PokerCombination(CombinationType.Pair, combinationCards1, kickers1);

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Spades),
            new Card(Rank.Ace, Suit.Clubs)
        };
        var kickers2 = new List<ICard> { new Card(Rank.King, Suit.Hearts) };
        var pair2 = new PokerCombination(CombinationType.Pair, combinationCards2, kickers2);

        Assert.False(pair1 > pair2);
        Assert.False(pair2 > pair1);
    }

    [Fact]
    public void CompareTo_FlushVsFlush_SameCards()
    {
        var combinationCards1 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.King, Suit.Clubs),
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.Jack, Suit.Clubs),
            new Card(Rank.Ten, Suit.Clubs)
        };
        var flush1 = new PokerCombination(CombinationType.Flush, combinationCards1, new List<ICard>());

        var combinationCards2 = new List<ICard>
        {
            new Card(Rank.Ace, Suit.Spades),
            new Card(Rank.King, Suit.Spades),
            new Card(Rank.Queen, Suit.Spades),
            new Card(Rank.Jack, Suit.Spades),
            new Card(Rank.Ten, Suit.Spades)
        };
        var flush2 = new PokerCombination(CombinationType.Flush, combinationCards2, new List<ICard>());

        Assert.False(flush1 > flush2);
        Assert.True(flush1 >= flush2);
        Assert.False(flush1 < flush2);
        Assert.True(flush1 <= flush2);
        Assert.True(flush1 == flush2);
    }
}
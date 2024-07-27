using PokerCalculator.Interfaces;
using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void Player_InitializesHand()
    {
        IHand hand = Hand.Parse("AsAd");
        IPlayer player = new Player(hand);
        Assert.NotNull(player.Hand);
        Assert.Equal(hand, player.Hand);
    }

    [Fact]
    public void Player_InitializesOuts()
    {
        IPlayer player = new Player(new Hand());
        Assert.NotNull(player.Outs);
        Assert.Empty(player.Outs);
    }
    
    [Fact]
    public void Player_InitializesEquity()
    {
        IPlayer player = new Player(new Hand());
        Assert.Equal(-1, player.Equity);
    }
    
    [Fact]
    public void Player_InitializesCombination()
    {
        IPlayer player = new Player(new Hand());
        Assert.Null(player.Combination);
    }
}
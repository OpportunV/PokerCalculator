using PokerCalculator.Models;


namespace PokerCalculator.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void Player_InitializesHand()
    {
        var hand = Hand.Parse("AsAd");
        var player = new Player(hand);
        Assert.NotNull(player.Hand);
        Assert.Equal(hand, player.Hand);
    }

    [Fact]
    public void Player_InitializesOuts()
    {
        var player = new Player(new Hand());
        Assert.NotNull(player.Outs);
        Assert.Empty(player.Outs);
    }
    
    [Fact]
    public void Player_InitializesEquity()
    {
        var player = new Player(new Hand());
        Assert.Equal(-1, player.Equity);
    }
}
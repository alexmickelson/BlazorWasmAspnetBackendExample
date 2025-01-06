using FluentAssertions;
using GameLogic;

namespace GameTest;

public class GameLifecycle
{
    [Fact]
    public void PlayersCanJoinGame()
    {
        var game = new Game();
        game.Status.Should().Be(GameStatus.NotStarted);

        
    }
}
